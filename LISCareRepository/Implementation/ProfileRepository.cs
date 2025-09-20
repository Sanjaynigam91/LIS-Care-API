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
        /// <summary>
        /// Delete profile by profile code and partnerId
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="profileCode"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<APIResponseModel<string>> DeleteProfile(string partnerId, string profileCode)
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
                if (!string.IsNullOrEmpty(profileCode) && !string.IsNullOrEmpty(partnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspDeleteProfileDetails;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProfileCode, profileCode));

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
        /// used to get all profiles details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
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
