using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.ClientMaster;
using LISCareDTO.MetaData;
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
    public class EmployeeRepository(LISCareDbContext dbContext, ILogger<EmployeeRepository> logger) : IEmployeeRepository
    {
        private readonly LISCareDbContext _dbContext = dbContext;
        private readonly ILogger<EmployeeRepository> _logger = logger;

      /// <summary>
      /// used to get all department
      /// </summary>
      /// <param name="category"></param>
      /// <param name="partnerId"></param>
      /// <returns></returns>
        public async Task<APIResponseModel<List<MetaDataTagsResponseModel>>> GetEmployeeDepartments(string? category, string partnerId)
        {
            _logger.LogInformation($"GetEmployeeDepartments, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<MetaDataTagsResponseModel>>
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
                    _logger.LogInformation($"UspGetEmployeeDepartments, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetEmployeeDepartments;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCategory, category));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
          
                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"UspGetEmployeeDepartments, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new MetaDataTagsResponseModel
                        {
                            ItemType = reader[ConstantResource.ItemType] as string ?? string.Empty,
                            ItemDescription = reader[ConstantResource.ItemDescription] as string ?? string.Empty,
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Get Employee Departments retrieved successfully.";
                        _logger.LogInformation($"All employee departments retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"GetEmployeeDepartments, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"GetEmployeeDepartments,method execution completed at :{DateTime.Now}");
            return response;
        }
    }
}
