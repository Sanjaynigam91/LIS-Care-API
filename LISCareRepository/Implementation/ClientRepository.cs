using Azure.Core;
using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.CenterMaster;
using LISCareDTO.ClientMaster;
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
    public class ClientRepository(LISCareDbContext dbContext, ILogger<ClientRepository> logger) :IClientRepository
    {
        private readonly LISCareDbContext _dbContext = dbContext;
        private readonly ILogger<ClientRepository> _logger = logger;

        public async Task<APIResponseModel<string>> DeleteClient(string clientId, string partnerId)
        {
            _logger.LogInformation($"DeleteClient method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(clientId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspDeleteClientById execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspDeleteClientById;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClientId, clientId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId));

                    _logger.LogInformation($"UspDeleteClientById execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspDeleteClientById execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspDeleteClientById execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspDeleteClientById execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspDeleteClientById execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"DeleteClient method execution completed at :{DateTime.Now}");
            return response;
        }

        /// <summary>
        /// used to get all clients
        /// </summary>
        /// <param name="clientStatus"></param>
        /// <param name="searchBy"></param>
        /// <param name="partnerId"></param>
        /// <param name="centerCode"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<ClientResponse>>> GetAllClients(string? clientStatus, string? searchBy, string partnerId, string? centerCode)
        {
            _logger.LogInformation($"Get All Clients Details method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<ClientResponse>>
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
                    _logger.LogInformation($"UspGetAllClients, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetAllClients;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamClientStatus, Convert.ToBoolean(clientStatus)));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamSearchBy, searchBy));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, centerCode));

                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"UspGetAllClients, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new ClientResponse
                        {
                            ClientId = reader[ConstantResource.ClientId] != DBNull.Value
                           ? (Guid)reader[ConstantResource.ClientId]
                           : Guid.Empty,
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                            ClientCode = reader[ConstantResource.ClientCode] as string ?? string.Empty,
                            ClientName = reader[ConstantResource.ClientName] as string ?? string.Empty,
                            CenterName = reader[ConstantResource.CenterName] as string ?? string.Empty,
                            MobileNumber = reader[ConstantResource.CenterMobileNumber] as string ?? string.Empty,
                            City = reader[ConstantResource.City] as string ?? string.Empty,
                            IsAccessEnabled = reader[ConstantResource.IsAccessEnabled] != DBNull.Value
                            && Convert.ToBoolean(reader[ConstantResource.IsAccessEnabled]),
                            UserId = reader[ConstantResource.UserId] as string ?? string.Empty,
                            ClientStatus = reader[ConstantResource.ClientStatus] != DBNull.Value
                            && Convert.ToBoolean(reader[ConstantResource.ClientStatus]),
                            LicenseNumber = reader[ConstantResource.LicenseNumber] as string ?? string.Empty,
                            DiscountPercentage = reader[ConstantResource.DiscountPercentage] != DBNull.Value
                            ? Convert.ToDecimal(reader[ConstantResource.DiscountPercentage]) : 0,
                            Speciality = reader[ConstantResource.Speciality] as string ?? string.Empty,
                            ClientType = reader[ConstantResource.ClientType] as string ?? string.Empty,
                            BillingType = reader[ConstantResource.BillingType] as string ?? string.Empty, 
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "All clients details retrieved successfully.";
                        _logger.LogInformation($"All clients details retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"Get All Clients Details method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"Get All Clients Details method execution completed at :{DateTime.Now}");
            return response;
        }
        /// <summary>
        /// used to get client details by Id
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<ClientResponse>>> GetClientById(string clientId, string partnerId)
        {
            _logger.LogInformation($"Get Client Details by Id method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<ClientResponse>>
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
                    _logger.LogInformation($"UspGetClientById, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetClientById;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamClientId, clientId));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"UspGetClientById, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new ClientResponse
                        {
                            ClientId = reader[ConstantResource.ClientId] != DBNull.Value
                           ? (Guid)reader[ConstantResource.ClientId]
                           : Guid.Empty,
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                            ClientCode = reader[ConstantResource.ClientCode] as string ?? string.Empty,
                            ClientName = reader[ConstantResource.ClientName] as string ?? string.Empty,
                            Speciality = reader[ConstantResource.Speciality] as string ?? string.Empty,
                            LicenseNumber = reader[ConstantResource.LicenseNumber] as string ?? string.Empty,
                            ClientType = reader[ConstantResource.ClientType] as string ?? string.Empty,
                            EmailId = reader[ConstantResource.EmailId] as string ?? string.Empty,
                            MobileNumber = reader[ConstantResource.CenterMobileNumber] as string ?? string.Empty,
                            Address = reader[ConstantResource.AddressInfo] as string ?? string.Empty,
                            ClientStatus = reader[ConstantResource.ClientStatus] != DBNull.Value
                            && Convert.ToBoolean(reader[ConstantResource.ClientStatus]),
                            CenterCode = reader[ConstantResource.CentreCode] as string ?? string.Empty,
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Client details retrieved successfully.";
                        _logger.LogInformation($"Client details retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"Get Client Details by Id method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"Get Client Details by Id method execution completed at :{DateTime.Now}");
            return response;
        }
        /// <summary>
        /// used to get all clients rates
        /// </summary>
        /// <param name="opType"></param>
        /// <param name="clientCode"></param>
        /// <param name="partnerId"></param>
        /// <param name="testCode"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<ClientCustomResponse>>> GetClientCustomRates(string? opType, string? clientCode, string? partnerId, string? testCode)
        {
            _logger.LogInformation($"GetClientCustomRates, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<ClientCustomResponse>>
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
                    _logger.LogInformation($"UspGetClientCustomRates, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetClientCustomRates;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamOpType,
                    string.IsNullOrEmpty(opType) ? (object)DBNull.Value : opType));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamClientCode,
                    string.IsNullOrEmpty(clientCode) ? (object)DBNull.Value : clientCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId,
                    string.IsNullOrEmpty(partnerId) ? (object)DBNull.Value : partnerId));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode,
                    string.IsNullOrEmpty(testCode) ? (object)DBNull.Value : testCode));

                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"UspGetClientCustomRates, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new ClientCustomResponse
                        {
                            MappingId = reader[ConstantResource.MappingId] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.MappingId]) : 0,
                            ClientCode = reader[ConstantResource.ClientCode] as string ?? string.Empty,
                            ClientName = reader[ConstantResource.ClientName] as string ?? string.Empty,
                            TestCode = reader[ConstantResource.TestCode] as string ?? string.Empty,
                            TestName = reader[ConstantResource.CentreTestName] as string ?? string.Empty,
                            CustomRate = reader[ConstantResource.AgreedRate] != DBNull.Value
                            ? Convert.ToDecimal(reader[ConstantResource.AgreedRate]) : 0,
                            Mrp = reader[ConstantResource.MRP] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.MRP]) : 0,
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Custom rates retrieved successfully.";
                        _logger.LogInformation($"Custom rates retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspGetClientCustomRates, execution falied at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"GetClientCustomRates, method execution completed at :{DateTime.Now}");
            return response;
        }

        /// <summary>
        /// used to save client details
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> SaveClient(ClientRequest clientRequest)
        {
            _logger.LogInformation($"SaveClient method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(clientRequest.PartnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspAddNewClient execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspAddNewClient;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, clientRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClientName, clientRequest.ClientName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSpeciality, clientRequest.Speciality));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLicenseNumber, clientRequest.LicenseNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClientType, clientRequest.ClientType));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmailId, clientRequest.EmailId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.MobileNumber, clientRequest.MobileNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAddressInfo, clientRequest.Address));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, clientRequest.CentreCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClientStatus, clientRequest.ClientStatus));
                   
                    _logger.LogInformation($"UspAddNewClient execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspAddNewClient execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspAddNewClient execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspAddNewClient execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspAddNewClient execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"SaveClient method execution completed at :{DateTime.Now}");
            return response;
        }
        /// <summary>
        /// used to update client details
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateClient(ClientRequest clientRequest)
        {
            _logger.LogInformation($"UpdateClient method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(clientRequest.PartnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspAddNewClient execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspUpdateClient;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClientId, clientRequest.ClientId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, clientRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClientName, clientRequest.ClientName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSpeciality, clientRequest.Speciality));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLicenseNumber, clientRequest.LicenseNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClientType, clientRequest.ClientType));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmailId, clientRequest.EmailId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.MobileNumber, clientRequest.MobileNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAddressInfo, clientRequest.Address));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, clientRequest.CentreCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClientStatus, clientRequest.ClientStatus));

                    _logger.LogInformation($"UspAddNewClient execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspAddNewClient execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspAddNewClient execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspAddNewClient execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspAddNewClient execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"UpdateClient method execution completed at :{DateTime.Now}");
            return response;
        }
    }
}
