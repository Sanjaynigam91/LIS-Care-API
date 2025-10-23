using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
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
    }
}
