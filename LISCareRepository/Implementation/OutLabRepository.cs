using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.ClientMaster;
using LISCareDTO.OutLab;
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
    public class OutLabRepository(LISCareDbContext dbContext, ILogger<OutLabRepository> logger) : IOutLabRepository
    {
        private readonly LISCareDbContext _dbContext = dbContext;
        private readonly ILogger<OutLabRepository> _logger = logger;

        /// <summary>
        /// used to create or add new out lab
        /// </summary>
        /// <param name="outLabRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> AddNewOutLab(OutLabRequest outLabRequest)
        {
            _logger.LogInformation($"AddNewOutLab method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(outLabRequest.LabName))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspAddNewOutLab execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspAddNewOutLab;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, outLabRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabName, outLabRequest.LabName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamContactPerson, outLabRequest.ContactPerson));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterMobileNumber, outLabRequest.MobileNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmailId, outLabRequest.Email));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIntroducedBy, outLabRequest.IntroducedBy));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabStatus, outLabRequest.LabStatus));

                    _logger.LogInformation($"UspAddNewOutLab execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspAddNewOutLab execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspAddNewOutLab execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspAddNewOutLab execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspAddNewOutLab execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"AddNewOutLab method execution completed at :{DateTime.Now}");
            return response;
        }

        /// <summary>
        /// used to delete out lab details
        /// </summary>
        /// <param name="labCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> DeleteOutLabDetails(string labCode, string partnerId)
        {
            _logger.LogInformation($"DeleteOutLabDetails method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(labCode))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspDeleteOutLab execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspDeleteOutLab;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabCode, labCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId));

                    _logger.LogInformation($"UspDeleteOutLab execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspDeleteOutLab execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspDeleteOutLab execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspDeleteOutLab execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspDeleteOutLab execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"DeleteOutLabDetails method execution completed at :{DateTime.Now}");
            return response;
        }

        /// <summary>
        /// used to get all out labs
        /// </summary>
        /// <param name="labStatus"></param>
        /// <param name="labname"></param>
        /// <param name="labCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<OutLabResponse>>> GetAllOutLabs(bool? labStatus, string? labname, string? labCode, string partnerId)
        {
            _logger.LogInformation($"GetAllOutLabs, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<OutLabResponse>>
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
                    _logger.LogInformation($"USPGetOutLabDetails, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.USPGetOutLabDetails;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamLabStatus, labStatus));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamLabName, labname));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamLabCode, labCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"USPGetOutLabDetails, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new OutLabResponse
                        {
                            LabCode = reader[ConstantResource.LabCode] as string ?? string.Empty,
                            LabName = reader[ConstantResource.LabName] as string ?? string.Empty,
                            City = reader[ConstantResource.City] as string ?? string.Empty,
                            MobileNumber = reader[ConstantResource.PhoneNumber] as string ?? string.Empty,
                            Email = reader[ConstantResource.OutLabEmail] as string ?? string.Empty,
                            ContactPerson = reader[ConstantResource.ContactPerson] as string ?? string.Empty,
                            LabStatus = reader[ConstantResource.LabStatus] != DBNull.Value
                            ? Convert.ToBoolean(reader[ConstantResource.LabStatus]) : false,
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "All out lab details retrieved successfully.";
                        _logger.LogInformation($"All out lab details retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"GetAllOutLabs method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"GetAllOutLabs method execution completed at :{DateTime.Now}");
            return response;
        }
        /// <summary>
        /// used to get out lab by lab code
        /// </summary>
        /// <param name="labCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<OutLabResponse>>> GetOutLabByLabCode(string labCode, string partnerId)
        {
            _logger.LogInformation($"GetOutLabByLabCode, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<OutLabResponse>>
            {
                Data = []
            };

            try
            {
                if (string.IsNullOrWhiteSpace(labCode))
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = "labCode cannot be null or empty.";
                }
                else
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await _dbContext.Database.OpenConnectionAsync();

                    using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"USPGetOutLabByLabCode, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.USPGetOutLabByLabCode;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamLabCode, labCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"USPGetOutLabByLabCode, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new OutLabResponse
                        {
                            LabCode = reader[ConstantResource.LabCode] as string ?? string.Empty,
                            LabName = reader[ConstantResource.LabName] as string ?? string.Empty,
                            City = reader[ConstantResource.City] as string ?? string.Empty,
                            MobileNumber = reader[ConstantResource.PhoneNumber] as string ?? string.Empty,
                            Email = reader[ConstantResource.OutLabEmail] as string ?? string.Empty,
                            ContactPerson = reader[ConstantResource.ContactPerson] as string ?? string.Empty,
                            LabStatus = reader[ConstantResource.LabStatus] != DBNull.Value
                            ? Convert.ToBoolean(reader[ConstantResource.LabStatus]) : false,
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                            IntroducedBy= reader[ConstantResource.IntroducedBy] as string ?? string.Empty
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Out lab details retrieved successfully.";
                        _logger.LogInformation($"Out lab details retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"GetOutLabByLabCode method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"GetOutLabByLabCode method execution completed at :{DateTime.Now}");
            return response;
        }
        /// <summary>
        /// used to update out lab details
        /// </summary>
        /// <param name="outLabRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateOutLabDetails(OutLabRequest outLabRequest)
        {
            _logger.LogInformation($"UpdateOutLabDetails method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(outLabRequest.LabName))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspUpdateOutLabDetails execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspUpdateOutLabDetails;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabCode, outLabRequest.LabCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, outLabRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabName, outLabRequest.LabName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamContactPerson, outLabRequest.ContactPerson));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterMobileNumber, outLabRequest.MobileNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmailId, outLabRequest.Email));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIntroducedBy, outLabRequest.IntroducedBy));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabStatus, outLabRequest.LabStatus));

                    _logger.LogInformation($"UspUpdateOutLabDetails execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspUpdateOutLabDetails execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspUpdateOutLabDetails execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspUpdateOutLabDetails execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspUpdateOutLabDetails execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"UpdateOutLabDetails method execution completed at :{DateTime.Now}");
            return response;
        }
    }
}
