using Azure.Core;
using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.CenterMaster;
using LISCareDTO.ClinicMaster;
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
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Implementation
{
    public class ClinicRepository(LISCareDbContext dbContext, ILogger<ClinicRepository> logger) : IClinicRepository
    {
        private readonly LISCareDbContext _dbContext = dbContext;
        private readonly ILogger<ClinicRepository> _logger = logger;

        public async Task<APIResponseModel<string>> CreateNewClinic(ClinicRequest request)
        {
            _logger.LogInformation($"Create New Clinic method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(request.PartnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspAddNewClinicDetail execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspAddNewClinicDetail;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicName, request.ClinicName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicIncharge, request.ClinicIncharge));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmailId, request.EmailId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterMobileNumber, request.MobileNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAlternateContactNo, request.AlterContactNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicDoctorCode, request.ClinicDoctorCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, request.CenterCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicAddress, request.ClinicAddress));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRateType, request.RateType));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicStatus, request.ClinicStatus));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, request.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCreatedBy, request.CreatedBy));
                    _logger.LogInformation($"UspAddNewClinicDetail execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspAddNewClinicDetail execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspAddNewClinicDetail execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspAddNewClinicDetail execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspAddNewClinicDetail execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"Create New Clinic method execution completed at :{DateTime.Now}");
            return response;
        }
        /// <summary>
        /// used to delete clinic details by Id
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> DeleteClinic(int clinicId, string partnerId)
        {
            _logger.LogInformation($"Delete Clinic method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                _logger.LogInformation($"Checking partnerId and clinicId");
                if (!string.IsNullOrEmpty(partnerId) && clinicId > 0)
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspDeleteClinicById, execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspDeleteClinicById;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicId,clinicId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId));
                    _logger.LogInformation($"UspDeleteClinicById, execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspDeleteClinicById, execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspDeleteClinicById, execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspDeleteClinicById, execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspDeleteClinicById, execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"Delete Clinic method execution completed at :{DateTime.Now}");
            return response;
        }

        public async Task<APIResponseModel<List<ClinicResponse>>> GetAllClinicDetails(string? partnerId, string? centerCode, string? clinicStatus, string? searchBy = "")
        {
            _logger.LogInformation($"Get All Clinic Details method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<ClinicResponse>>
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
                    _logger.LogInformation($"UspGetClinicDetails, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetClinicDetails;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCentreCode, centerCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicStatus, clinicStatus));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamSearchBy, searchBy));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"UspGetClinicDetails, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new ClinicResponse
                        {
                            ClinicId = reader[ConstantResource.ClinicId] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.ClinicId]) : 0,
                            ClinicCode = reader[ConstantResource.ClinicCode] as string ?? string.Empty,
                            ClinicName = reader[ConstantResource.ClinicName] as string ?? string.Empty,
                            ClinicIncharge = reader[ConstantResource.ClinicIncharge] as string ?? string.Empty,
                            MobileNumber = reader[ConstantResource.CenterMobileNumber] as string ?? string.Empty,
                            EmailId = reader[ConstantResource.EmailId] as string ?? string.Empty,
                            AlternateContactNo = reader[ConstantResource.AlternateContactNo] as string ?? string.Empty,
                            RateType = reader[ConstantResource.RateType] as string ?? string.Empty,
                            ClinicDoctorCode = reader[ConstantResource.ClinicDoctorCode] as string ?? string.Empty,
                            CenterCode = reader[ConstantResource.CenterCode] as string ?? string.Empty,
                            ClinicAddress = reader[ConstantResource.ClinicAddress] as string ?? string.Empty,
                            ClinicStatus = reader[ConstantResource.ClinicStatus] != DBNull.Value
                            && Convert.ToBoolean(reader[ConstantResource.ClinicStatus]),
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                            CreatedBy = reader[ConstantResource.CreatedBy] as string ?? string.Empty,
                            CreatedOn = reader[ConstantResource.CreatedOn] != DBNull.Value
                            ? Convert.ToDateTime(reader[ConstantResource.CreatedOn])
                            : DateTime.MinValue,
                            UpdatedOn = reader[ConstantResource.CreatedOn] != DBNull.Value
                            ? Convert.ToDateTime(reader[ConstantResource.CreatedOn])
                            : DateTime.MinValue,
                            UpdatedBy = reader[ConstantResource.UpdateBy] as string ?? string.Empty,

                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "All Clinic details retrieved successfully.";
                        _logger.LogInformation($"All Clinic details retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"Get All Clinic Details method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"Get All Clinic Details method execution completed at :{DateTime.Now}");
            return response;
        }

        public async Task<APIResponseModel<List<ClinicResponse>>> GetClinicDeatilsById(int clinicId, string partnerId)
        {
            _logger.LogInformation($"GetClinicDeatilsById, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<ClinicResponse>>
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
                    _logger.LogInformation($"UspGetClinicDetailsById, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetClinicDetailsById;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicId, clinicId));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"UspGetClinicDetailsById, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new ClinicResponse
                        {
                            ClinicId = reader[ConstantResource.ClinicId] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.ClinicId]) : 0,
                            ClinicCode = reader[ConstantResource.ClinicCode] as string ?? string.Empty,
                            ClinicName = reader[ConstantResource.ClinicName] as string ?? string.Empty,
                            ClinicIncharge = reader[ConstantResource.ClinicIncharge] as string ?? string.Empty,
                            MobileNumber = reader[ConstantResource.CenterMobileNumber] as string ?? string.Empty,
                            EmailId = reader[ConstantResource.EmailId] as string ?? string.Empty,
                            AlternateContactNo = reader[ConstantResource.AlternateContactNo] as string ?? string.Empty,
                            RateType = reader[ConstantResource.RateType] as string ?? string.Empty,
                            ClinicDoctorCode = reader[ConstantResource.ClinicDoctorCode] as string ?? string.Empty,
                            CenterCode = reader[ConstantResource.CenterCode] as string ?? string.Empty,
                            ClinicAddress = reader[ConstantResource.ClinicAddress] as string ?? string.Empty,
                            ClinicStatus = reader[ConstantResource.ClinicStatus] != DBNull.Value
                            && Convert.ToBoolean(reader[ConstantResource.ClinicStatus]),
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                            CreatedBy = reader[ConstantResource.CreatedBy] as string ?? string.Empty,
                            CreatedOn = reader[ConstantResource.CreatedOn] != DBNull.Value
                            ? Convert.ToDateTime(reader[ConstantResource.CreatedOn])
                            : DateTime.MinValue,
                            UpdatedOn = reader[ConstantResource.CreatedOn] != DBNull.Value
                            ? Convert.ToDateTime(reader[ConstantResource.CreatedOn])
                            : DateTime.MinValue,
                            UpdatedBy = reader[ConstantResource.UpdateBy] as string ?? string.Empty,

                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Clinic details retrieved successfully.";
                        _logger.LogInformation($"Clinic details retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"GetClinicDeatilsById, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"GetClinicDeatilsById method execution completed at :{DateTime.Now}");
            return response;
        }

        public async Task<APIResponseModel<string>> UpdateClinic(ClinicRequest request)
        {
            _logger.LogInformation($"Update Clinic method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(request.PartnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspUpdateClinicDetail execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspUpdateClinicDetail;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicId, request.ClinicId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicName, request.ClinicName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicIncharge, request.ClinicIncharge));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmailId, request.EmailId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterMobileNumber, request.MobileNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAlternateContactNo, request.AlterContactNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicDoctorCode, request.ClinicDoctorCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, request.CenterCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicAddress, request.ClinicAddress));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRateType, request.RateType));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicStatus, request.ClinicStatus));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, request.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamUpdatedBy, request.UpdatedBy));
                    _logger.LogInformation($"UspUpdateClinicDetail execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspUpdateClinicDetail execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspUpdateClinicDetail execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspUpdateClinicDetail execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspUpdateClinicDetail execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"Update Clinic method execution completed at :{DateTime.Now}");
            return response;
        }
    }
}
