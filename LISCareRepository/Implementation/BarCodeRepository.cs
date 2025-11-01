using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.Barcode;
using LISCareDTO.Employee;
using LISCareReporting.LISBarcode;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Implementation
{
    public class BarCodeRepository : IBarcodeRepository
    {
        private readonly LISCareDbContext _dbContext;
        private readonly ILogger<BarCodeRepository> _logger;
        private readonly BulkBarcodeGenerator _bulkBarcode;

        public BarCodeRepository(LISCareDbContext dbContext, ILogger<BarCodeRepository> logger, BulkBarcodeGenerator bulkBarcodeGenerator)
        {
            _dbContext = dbContext;
            _logger = logger;
            _bulkBarcode = bulkBarcodeGenerator;
        }

        /// <summary>
        /// used to generate barcodes in bulk
        /// </summary>
        /// <param name="SequenceStart"></param>
        /// <param name="SequenceEnd"></param>
        /// <returns></returns>
        public Task<byte[]> GenerateBarcodes(int sequenceStart, int sequenceEnd)
        {
            return Task.FromResult(BulkBarcodeGenerator.GenerateBulkBarcodes(sequenceStart, sequenceEnd));
        }

        /// <summary>
        /// used to get all barcode details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<BarcodeResponse>>> GetAllBarcodeDetails(string partnerId)
        {
            _logger.LogInformation($"GetAllBarcodeDetails, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<BarcodeResponse>>
            {
                Data = []
            };

            try
            {
                if (string.IsNullOrWhiteSpace(partnerId))
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = "PartnerId cannot be null or empty.";
                }
                else
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await _dbContext.Database.OpenConnectionAsync();

                    using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspGetBarcodeDetailsByPartnerId, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetBarcodeDetailsByPartnerId;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"UspGetBarcodeDetailsByPartnerId, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new BarcodeResponse
                        {

                            GeneratedOn = reader[ConstantResource.GeneratedOn] as string ?? string.Empty,
                            SequenceStart = reader[ConstantResource.SequenceStart] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.SequenceStart])
                            : 0,

                            SequenceEnd = reader[ConstantResource.SequenceEnd] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.SequenceEnd])
                            : 0,

                            CreatedBy = reader[ConstantResource.CreatedBy] as string ?? string.Empty,
                            GenerateId = reader[ConstantResource.GenerateId] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.GenerateId])
                            : 0,

                        });

                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Barcode deatils retrieved successfully.";
                        _logger.LogInformation($"All barcode details retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"GetAllBarcodeDetails, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"GetAllBarcodeDetails,method execution completed at :{DateTime.Now}");
            return response;
        }

        /// <summary>
        /// used to get last printed barcode
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<int>> GetLastPrintedBarcode(string partnerId)
        {
            _logger.LogInformation($"GetLastPrintedBarcode, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<int>
            {
                Data = 0
            };

            try
            {
                if (string.IsNullOrWhiteSpace(partnerId))
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = "PartnerId cannot be null or empty.";
                }
                else
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await _dbContext.Database.OpenConnectionAsync();

                    using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspGetLastPrintedBarcode, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetLastPrintedBarcode;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"UspGetLastPrintedBarcode, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {

                        response.Data = reader[ConstantResource.SequenceEnd] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.SequenceEnd])
                            : 0;
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Last printed barcode retrieved successfully.";
                        _logger.LogInformation($"Last printed barcode retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"GetLastPrintedBarcode, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"GetLastPrintedBarcode,method execution completed at :{DateTime.Now}");
            return response;
        }

        /// <summary>
        /// used to save printed barcodes
        /// </summary>
        /// <param name="barcodeRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> SavePrintedBarcodes(BarcodeRequest barcodeRequest)
        {

            var response = new APIResponseModel<string>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (barcodeRequest.SequenceStart > 0)
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspSaveBarcodePrintDetails;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSequenceStart, barcodeRequest.SequenceStart));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSequenceEnd, barcodeRequest.SequenceEnd));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, barcodeRequest.PartnerId.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCreatedBy, barcodeRequest.CreatedBy.Trim()));

                    // output parameters
                    SqlParameter outputBitParm = new SqlParameter(ConstantResource.IsSuccess, SqlDbType.Bit)
                    {

                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorParm = new SqlParameter(ConstantResource.IsError, SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorMessageParm = new SqlParameter(ConstantResource.ErrorMsg, SqlDbType.NVarChar, 404)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputBitParm);
                    command.Parameters.Add(outputErrorParm);
                    command.Parameters.Add(outputErrorMessageParm);
                    _dbContext.Database.GetDbConnection().Open();
                    command.ExecuteScalar();
                    OutputParameterModel parameterModel = new OutputParameterModel
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value) ?? string.Empty,
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.Status = parameterModel.IsSuccess;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;

                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status =false;
                response.ResponseMessage = ex.Message;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }
    }
}
