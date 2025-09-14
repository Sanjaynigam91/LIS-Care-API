using Azure;
using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.ProfileMaster;
using LISCareDTO.TestMaster;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Implementation
{
    public class ProfileRepository(IConfiguration configuration, LISCareDbContext dbContext) : IProfileRepository
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly LISCareDbContext _dbContext = dbContext;

        public async Task<APIResponseModel<List<ProfileResponse>>> GetAllProfileDetails(string partnerId)
        {
            var response = new APIResponseModel<List<ProfileResponse>>
            {
                Data = new List<ProfileResponse>()
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
                    cmd.CommandText = ConstantResource.UspRetrieveProfileDetails;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new ProfileResponse
                        {
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                            ProfileCode = reader[ConstantResource.ProfileCode] as string ?? string.Empty,
                            ProfileName = reader[ConstantResource.ProfileName] as string ?? string.Empty,
                            MRP = reader[ConstantResource.MRP] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.MRP]) : 0,
                            ProfileStatus = reader[ConstantResource.ProfileStatus] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.ProfileStatus]),
                            B2CRates = reader[ConstantResource.ProfileB2CRates] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.ProfileB2CRates]) : 0,
                            SampleTypes = reader[ConstantResource.SampleTypes] as string ?? string.Empty,
                            Labrates = reader[ConstantResource.ProfileLabRates] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.ProfileLabRates]) : 0,
                            TatHrs = reader[ConstantResource.TatHrs] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.TatHrs]) : 0,
                            CptCodes = reader[ConstantResource.CptCodes] as string ?? string.Empty,
                            PrintSequence = reader[ConstantResource.PrintSequence] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.PrintSequence]) : 0,
                            IsRestricted = reader[ConstantResource.IsRestricted] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.IsRestricted]),
                            SubProfilesCount = reader[ConstantResource.SubProfilesCount] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.SubProfilesCount]) : 0,
                            RecordId = reader[ConstantResource.RecordId] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.RecordId]) : 0,
                            NormalRangeFooter = reader[ConstantResource.ProfileNormalRangeFooter] as string ?? string.Empty,
                            TestShortName = reader[ConstantResource.TestShortName] as string ?? string.Empty,
                            ProfileProfitRate = reader[ConstantResource.ProfileProfitRate] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.ProfileProfitRate]) : 0,
                            LabTestCodes = reader[ConstantResource.ProfileLabTestCode] as string ?? string.Empty,
                            IsProfileOutLab = reader[ConstantResource.IsProfileOutLab] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.IsProfileOutLab]),
                            TestApplicable = reader[ConstantResource.TestApplicable] as string ?? string.Empty,
                            IsLMP = reader[ConstantResource.IsLMP] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.IsLMP]),
                            IsNABLApplicable = reader[ConstantResource.IsNABLApplicable] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.IsNABLApplicable])
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
