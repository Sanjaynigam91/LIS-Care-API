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
    public class AnalyzerRepository(IConfiguration configuration, LISCareDbContext dbContext): IAnalyzerRepository
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly LISCareDbContext _dbContext = dbContext;

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
                            WarrantyEndDate = reader[ConstantResource.WarrantyEndDate] != DBNull.Value ? Convert.ToDateTime(reader[ConstantResource.WarrantyEndDate]) : DateTime.Now,
                            EngineerContactNo = reader[ConstantResource.EngineerContactNo] as string ?? string.Empty,
                            AssetCode = reader[ConstantResource.AssetCode] as string ?? string.Empty
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Profiles retrieved successfully.";
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
    }
}
