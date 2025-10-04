using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.AnalyzerMaster;
using LISCareDTO.ProfileMaster;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Implementation
{
    public class AnalyzerRepository(IConfiguration configuration, LISCareDbContext dbContext) : IAnalyzerRepository
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly LISCareDbContext _dbContext = dbContext;

        public async Task<APIResponseModel<string>> DeleteAnalyzerDetails(int analyzerId, string partnerId)
        {
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (analyzerId > 0 && !string.IsNullOrEmpty(partnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspDeleteAnalyzerById;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAnalyzerId, analyzerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId));


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

                    await command.ExecuteScalarAsync();
                    OutputParameterModel parameterModel = new OutputParameterModel
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value) ?? string.Empty,
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Status = parameterModel.IsSuccess;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.ProfileCodeEmpty;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            return response;
        }

        /// <summary>
        /// used to get all analyzer details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="AnalyzerNameOrShortCode"></param>
        /// <param name="AnalyzerStatus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<APIResponseModel<List<AnalyzerResponse>>> GetAllAnalyzerDetails(string partnerId, string? AnalyzerNameOrShortCode = "", string? AnalyzerStatus = "")
        {
            var response = new APIResponseModel<List<AnalyzerResponse>>
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
                    cmd.CommandText = ConstantResource.USPGetAllAnalyzers;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamAnalyzerNameOrShortCode, AnalyzerNameOrShortCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamStatus, AnalyzerStatus));

                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new AnalyzerResponse
                        {
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                            AnalyzerId = reader[ConstantResource.AnalyzerId] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.AnalyzerId]) : 0,
                            AnalyzerName = reader[ConstantResource.AnalyzerNames] as string ?? string.Empty,
                            AnalyzerCode = reader[ConstantResource.AnalyzerShortCode] as string ?? string.Empty,
                            AnalyzerStatus = reader[ConstantResource.Status] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.Status]),
                            SupplierCode = reader[ConstantResource.SupplierCode] as string ?? string.Empty,
                            PurchaseValue = reader[ConstantResource.PurchasedValue] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.PurchasedValue]) : 0,
                            WarrantyEndDate = reader[ConstantResource.WarrantyEndDate] != DBNull.Value
                            ? Convert.ToDateTime(reader[ConstantResource.WarrantyEndDate]).ToString("yyyyMMdd")
                            : DateTime.Now.ToString("yyyyMMdd"),
                            EngineerContactNo = reader[ConstantResource.EngineerContactNo] as string ?? string.Empty,
                            AssetCode = reader[ConstantResource.AssetCode] as string ?? string.Empty,
                            SupplierName= reader[ConstantResource.SupplierName] as string ?? string.Empty
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Analyzers retrieved successfully.";
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                // Optionally log the exception here
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }

            return response;
        }
        /// <summary>
        /// used to get all suppliers
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<SupplierResponse>>> GetAllSuppliers(string partnerId)
        {
            var response = new APIResponseModel<List<SupplierResponse>>
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
                    cmd.CommandText = ConstantResource.USPGetAllSuppliers;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new SupplierResponse
                        {
                            SupplierCode = reader[ConstantResource.SupplierCode] as string ?? string.Empty,
                            SupplierName = reader[ConstantResource.SupplierName] as string ?? string.Empty

                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Supplier details retrieved successfully.";
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                // Optionally log the exception here
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }

            return response;
        }
        /// <summary>
        /// used to get analyzer details by analyzerId
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="analyzerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<AnalyzerResponse>>> GetAnalyzerDetails(string partnerId, int analyzerId)
        {
            var response = new APIResponseModel<List<AnalyzerResponse>>
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
                    cmd.CommandText = ConstantResource.USPGetAnalyzerDetailsById;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamAnalyzerId, analyzerId));


                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new AnalyzerResponse
                        {
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                            AnalyzerId = reader[ConstantResource.AnalyzerId] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.AnalyzerId]) : 0,
                            AnalyzerName = reader[ConstantResource.AnalyzerNames] as string ?? string.Empty,
                            AnalyzerCode = reader[ConstantResource.AnalyzerShortCode] as string ?? string.Empty,
                            AnalyzerStatus = reader[ConstantResource.Status] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.Status]),
                            SupplierCode = reader[ConstantResource.SupplierCode] as string ?? string.Empty,
                            PurchaseValue = reader[ConstantResource.PurchasedValue] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.PurchasedValue]) : 0,
                            WarrantyEndDate = reader[ConstantResource.WarrantyEndDate] != DBNull.Value
                            ? Convert.ToDateTime(reader[ConstantResource.WarrantyEndDate]).ToString("yyyyMMdd")
                            : DateTime.Now.ToString("yyyyMMdd"),
                            EngineerContactNo = reader[ConstantResource.EngineerContactNo] as string ?? string.Empty,
                            AssetCode = reader[ConstantResource.AssetCode] as string ?? string.Empty
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Analyzers retrieved successfully.";
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                // Optionally log the exception here
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }

            return response;
        }

        public async Task<APIResponseModel<List<AnalyzerMappingResponse>>> GetAnalyzerTestMappings(string partnerId, int analyzerId)
        {
            var response = new APIResponseModel<List<AnalyzerMappingResponse>>
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
                    cmd.CommandText = ConstantResource.UspGetAnalyzerTestMapping;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamAnalyzerId, analyzerId));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new AnalyzerMappingResponse
                        {
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                            AnalyzerId = reader[ConstantResource.AnalyzerId] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.AnalyzerId]) : 0,
                            MappingId = reader[ConstantResource.MappingId] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.MappingId]) : 0,
                            AnalyzerTestCode = reader[ConstantResource.AnalyzerTestCode] as string ?? string.Empty,
                            LabTestCode = reader[ConstantResource.AnalyzerLabTestCode] as string ?? string.Empty,
                            Status = reader[ConstantResource.Status] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.Status]),
                            IsProfileCode = reader[ConstantResource.IsProfileCode] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.IsProfileCode]),
                            SampleType = reader[ConstantResource.SampleType] as string ?? string.Empty,
                            TestName = reader[ConstantResource.TestName] as string ?? string.Empty
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Analyzer's test mappings retrieved successfully.";
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                // Optionally log the exception here
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }

            return response;
        }
        /// <summary>
        /// used to save analyzer details
        /// </summary>
        /// <param name="analyzerRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> SaveAnalyzerDetails(AnalyzerRequest analyzerRequest)
        {
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(analyzerRequest.AnalyzerName) && !string.IsNullOrEmpty(analyzerRequest.PartnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspAddNewLISAnalyzer;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAnalyzerName, analyzerRequest.AnalyzerName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAnalyzerShortCode, analyzerRequest.AnalyzerShortCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamStatus, analyzerRequest.Status));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSupplierCode, analyzerRequest.SupplierCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPurchasedValue, analyzerRequest.PurchasedValue));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamWarrantyEndDate, analyzerRequest.WarrantyEndDate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEngineerContactNo, analyzerRequest.EngineerContactNo));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAssetCode, analyzerRequest.AssetCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, analyzerRequest.PartnerId));


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

                    await command.ExecuteScalarAsync();
                    OutputParameterModel parameterModel = new OutputParameterModel
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value) ?? string.Empty,
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Status = parameterModel.IsSuccess;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.ProfileCodeEmpty;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            return response;
        }
        /// <summary>
        /// used to update analyzer details
        /// </summary>
        /// <param name="analyzerRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateAnalyzerDetails(AnalyzerRequest analyzerRequest)
        {
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (analyzerRequest.AnalyzerId > 0)
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspUpdateLISAnalyzerDetails;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAnalyzerId, analyzerRequest.AnalyzerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAnalyzerName, analyzerRequest.AnalyzerName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAnalyzerShortCode, analyzerRequest.AnalyzerShortCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamStatus, analyzerRequest.Status));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSupplierCode, analyzerRequest.SupplierCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPurchasedValue, analyzerRequest.PurchasedValue));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamWarrantyEndDate, analyzerRequest.WarrantyEndDate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEngineerContactNo, analyzerRequest.EngineerContactNo));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAssetCode, analyzerRequest.AssetCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, analyzerRequest.PartnerId));


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

                    await command.ExecuteScalarAsync();
                    OutputParameterModel parameterModel = new OutputParameterModel
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value) ?? string.Empty,
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Status = parameterModel.IsSuccess;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.ProfileCodeEmpty;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            return response;
        }
    }
}
