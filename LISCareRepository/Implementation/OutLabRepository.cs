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
    }
}
