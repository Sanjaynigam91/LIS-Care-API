using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.AnalyzerMaster;
using LISCareDTO.CenterMaster;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Implementation
{
    public class CenterRepository(LISCareDbContext dbContext) : ICenterRepository
    {
        private readonly LISCareDbContext _dbContext = dbContext;

        public async Task<APIResponseModel<List<CenterResponse>>> GetAllCenterDetails(string? partnerId, string? centerStatus = "", string? searchBy = "")
        {
            var response = new APIResponseModel<List<CenterResponse>>
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
                    cmd.CommandText = ConstantResource.UspGetAllCenters;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterStatus, centerStatus));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamSearchBy, searchBy));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new CenterResponse
                        {
                           
                            CenterCode = reader[ConstantResource.CenterCode] as string ?? string.Empty,
                            CenterName = reader[ConstantResource.CenterName] as string ?? string.Empty,
                            CenterInchargeName = reader[ConstantResource.CenterInchargeName] as string ?? string.Empty,
                            SalesIncharge = reader[ConstantResource.SalesIncharge] as string ?? string.Empty,
                            CenterAddress = reader[ConstantResource.CenterAddress] as string ?? string.Empty,
                            Pincode = reader[ConstantResource.PinCode] as string ?? string.Empty,
                            MobileNumber = reader[ConstantResource.CenterMobileNumber] as string ?? string.Empty,
                            EmailId = reader[ConstantResource.EmailId] as string ?? string.Empty,
                            AlternateContactNo = reader[ConstantResource.AlternateContactNo] as string ?? string.Empty,
                            RateType = reader[ConstantResource.RateType] as string ?? string.Empty,
                            CenterStatus = reader[ConstantResource.CenterStatus] != DBNull.Value
                            && Convert.ToBoolean(reader[ConstantResource.CenterStatus]),
                            IntroducedBy = reader[ConstantResource.IntroducedBy] as string ?? string.Empty,
                            CreditLimit = reader[ConstantResource.CreditLimit] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.CreditLimit]): 0,
                            IsAutoBarcode = reader[ConstantResource.IsAutoBarcode] != DBNull.Value
                            && Convert.ToBoolean(reader[ConstantResource.IsAutoBarcode]),
                            City = reader[ConstantResource.City] as string ?? string.Empty,
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Cetner details retrieved successfully.";
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
