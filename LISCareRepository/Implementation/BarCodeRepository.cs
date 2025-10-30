using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.Barcode;
using LISCareDTO.Employee;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Implementation
{
    public class BarCodeRepository(LISCareDbContext dbContext, ILogger<BarCodeRepository> logger): IBarcodeRepository
    {
        private readonly LISCareDbContext _dbContext=dbContext;
        private readonly ILogger<BarCodeRepository> _logger = logger;

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
    }
}
