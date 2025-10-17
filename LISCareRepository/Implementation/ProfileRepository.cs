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
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LISCareRepository.Implementation
{
    public class ProfileRepository(IConfiguration configuration, LISCareDbContext dbContext) : IProfileRepository
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly LISCareDbContext _dbContext = dbContext;
        /// <summary>
        /// used to delete tests mapping by mapping Id and partnerId
        /// </summary>
        /// <param name="mappingId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> DeleteMappingTests(string mappingId, string partnerId)
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
                if (!string.IsNullOrEmpty(mappingId) && !string.IsNullOrEmpty(partnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspDeleteMappingDetails;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamMappingId, mappingId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId));

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
        /// used to get all mapped test into profile's
        /// </summary>
        /// <param name="profileCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<APIResponseModel<List<ProfileMappingResponse>>> GetAllMappedTests(string profileCode, string partnerId)
        {
            var response = new APIResponseModel<List<ProfileMappingResponse>>
            {
                Data = new List<ProfileMappingResponse>()
            };

            try
            {
                if (string.IsNullOrWhiteSpace(partnerId) && string.IsNullOrEmpty(profileCode))
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = ConstantResource.ProfileCodeEmpty;
                }
                else
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await _dbContext.Database.OpenConnectionAsync();

                    using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = ConstantResource.UspProfileMappingRetrieve;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamProfileCode, profileCode.Trim()));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new ProfileMappingResponse
                        {
                            TestsMappingId = reader[ConstantResource.TestsMappingId] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestsMappingId]) ?? string.Empty : string.Empty,
                            ProfileCode = reader[ConstantResource.ProfileCode] as string ?? string.Empty,
                            ProfileName = reader[ConstantResource.ProfileName] as string ?? string.Empty,
                            TestCode = reader[ConstantResource.TestCode] as string ?? string.Empty,
                            TestName = reader[ConstantResource.MappedTestName] as string ?? string.Empty,
                            PrintOrder = reader[ConstantResource.PrintOrder] != DBNull.Value
                         ? Convert.ToInt32(reader[ConstantResource.PrintOrder])
                         : 0,
                            SectionName = reader[ConstantResource.SectionName] as string ?? string.Empty,
                            GroupHeader = reader[ConstantResource.GroupHeader] as string ?? string.Empty,
                            ReportTemplateName = reader[ConstantResource.ProfileTemplateName] as string ?? string.Empty,
                            CanPrintSeparately = reader[ConstantResource.CanPrintSeparately] != DBNull.Value
                         ? Convert.ToBoolean(reader[ConstantResource.CanPrintSeparately])
                         : false
                        });

                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Profile's mapped tests retrieved successfully.";
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
        /// <summary>
        /// used to get profiles by profile code
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="profileCode"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<APIResponseModel<ProfileResponse>> GetProfilesByProfileCode(string partnerId, string profileCode)
        {
            var response = new APIResponseModel<ProfileResponse>
            {
                Data = new ProfileResponse()
            };

            try
            {
                if (string.IsNullOrWhiteSpace(partnerId) && string.IsNullOrEmpty(profileCode))
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = ConstantResource.ProfileCodeEmpty;
                }
                else
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await _dbContext.Database.OpenConnectionAsync();

                    using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = ConstantResource.UspGetProfileDetailsByProfileCode;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamProfileCode, profileCode.Trim()));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    ProfileResponse profile = new ProfileResponse();
                    while (await reader.ReadAsync())
                    {
                        profile.PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty;
                        profile.ProfileCode = reader[ConstantResource.ProfileCode] as string ?? string.Empty;
                        profile.ProfileName = reader[ConstantResource.ProfileName] as string ?? string.Empty;
                        profile.MRP = reader[ConstantResource.MRP] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.MRP]) : 0;
                        profile.ProfileStatus = reader[ConstantResource.ProfileStatus] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.ProfileStatus]);
                        profile.B2CRates = reader[ConstantResource.ProfileB2CRates] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.ProfileB2CRates]) : 0;
                        profile.SampleTypes = reader[ConstantResource.SampleTypes] as string ?? string.Empty;
                        profile.Labrates = reader[ConstantResource.ProfileLabRates] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.ProfileLabRates]) : 0;
                        profile.TatHrs = reader[ConstantResource.TatHrs] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.TatHrs]) : 0;
                        profile.CptCodes = reader[ConstantResource.CptCodes] as string ?? string.Empty;
                        profile.PrintSequence = reader[ConstantResource.PrintSequence] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.PrintSequence]) : 0;
                        profile.IsRestricted = reader[ConstantResource.IsRestricted] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.IsRestricted]);
                        profile.SubProfilesCount = reader[ConstantResource.SubProfilesCount] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.SubProfilesCount]) : 0;
                        profile.RecordId = reader[ConstantResource.RecordId] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.RecordId]) : 0;
                        profile.NormalRangeFooter = reader[ConstantResource.ProfileNormalRangeFooter] as string ?? string.Empty;
                        profile.TestShortName = reader[ConstantResource.TestShortName] as string ?? string.Empty;
                        profile.ProfileProfitRate = reader[ConstantResource.ProfileProfitRate] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.ProfileProfitRate]) : 0;
                        profile.LabTestCodes = reader[ConstantResource.ProfileLabTestCode] as string ?? string.Empty;
                        profile.IsProfileOutLab = reader[ConstantResource.IsProfileOutLab] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.IsProfileOutLab]);
                        profile.TestApplicable = reader[ConstantResource.TestApplicable] as string ?? string.Empty;
                        profile.IsLMP = reader[ConstantResource.IsLMP] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.IsLMP]);
                        profile.IsNABLApplicable = reader[ConstantResource.IsNABLApplicable] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.IsNABLApplicable]);
                        profile.IsAvailableForAll = reader[ConstantResource.IsAvailableForAll] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.IsAvailableForAll]);

                        response.Data = profile;
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
        /// <summary>
        /// used to save profile details
        /// </summary>
        /// <param name="profileRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<APIResponseModel<string>> SaveProfileDetails(ProfileRequest profileRequest)
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
                if (!string.IsNullOrEmpty(profileRequest.ProfileName) && !string.IsNullOrEmpty(profileRequest.PartnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.USPSaveProfileDetails;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProfileCode, profileRequest.ProfileCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ProfileName, profileRequest.ProfileName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.PartnerId, profileRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientRate, profileRequest.PatientRate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClientRate, profileRequest.ClientRate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabRate, profileRequest.LabRate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProfileStatus, profileRequest.ProfileStatus));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestShortName, profileRequest.TestShortName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPrintSequence, profileRequest.PrintSequence));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSampleTypes, profileRequest.SampleTypes));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsAvailableForAll, profileRequest.IsAvailableForAll));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabTestCode, profileRequest.LabTestCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsProfileOutLab, profileRequest.IsProfileOutLab));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestApplicable, profileRequest.TestApplicable));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsLMP, profileRequest.IsLMP));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsNABApplicable, profileRequest.IsNABApplicable));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProfileFooter, profileRequest.ProfileFooter));

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
        /// used to save test mapping details
        /// </summary>
        /// <param name="mappingRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<APIResponseModel<string>> SaveTestMappingDeatils(TestMappingRequest mappingRequest)
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
                if (!string.IsNullOrEmpty(mappingRequest.ProfileCode) && !string.IsNullOrEmpty(mappingRequest.TestCode))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspTestMappingForProfile;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProfileCode, mappingRequest.ProfileCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.PartnerId, mappingRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode, mappingRequest.TestCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSectionName, mappingRequest.SectionName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPrintOrder, mappingRequest.PrintOrder));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReportTemplateName, mappingRequest.ReportTemplateName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamGroupHeader, mappingRequest.GroupHeader));

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
        /// used to update all mappings.
        /// </summary>
        /// <param name="mappingRequests"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateAllMapping(List<TestMappingRequest> mappingRequests)
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
                if (mappingRequests.Count > 0)
                {
                    foreach (var allMppingItem in mappingRequests)
                    {
                        if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                            _dbContext.Database.OpenConnection();
                        var command = _dbContext.Database.GetDbConnection().CreateCommand();
                        command.CommandText = ConstantResource.UspUpdateTestMapping;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter(ConstantResource.ParamProfileCode, allMppingItem.ProfileCode));
                        command.Parameters.Add(new SqlParameter(ConstantResource.PartnerId, allMppingItem.PartnerId));
                        command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode, allMppingItem.TestCode));
                        command.Parameters.Add(new SqlParameter(ConstantResource.ParamSectionName, allMppingItem.SectionName));
                        command.Parameters.Add(new SqlParameter(ConstantResource.ParamPrintOrder, allMppingItem.PrintOrder));
                        command.Parameters.Add(new SqlParameter(ConstantResource.ParamReportTemplateName, allMppingItem.ReportTemplateName));
                        command.Parameters.Add(new SqlParameter(ConstantResource.ParamGroupHeader, allMppingItem.GroupHeader));

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
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.InvaidMappingRequest;
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
        /// used to update profile details
        /// </summary>
        /// <param name="profileRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateProfileDetails(ProfileRequest profileRequest)
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
                if (!string.IsNullOrEmpty(profileRequest.ProfileName) && !string.IsNullOrEmpty(profileRequest.PartnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.USPUpdateProfileDetails;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProfileCode, profileRequest.ProfileCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ProfileName, profileRequest.ProfileName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.PartnerId, profileRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientRate, profileRequest.PatientRate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClientRate, profileRequest.ClientRate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabRate, profileRequest.LabRate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProfileStatus, profileRequest.ProfileStatus));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestShortName, profileRequest.TestShortName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPrintSequence, profileRequest.PrintSequence));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSampleTypes, profileRequest.SampleTypes));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsAvailableForAll, profileRequest.IsAvailableForAll));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabTestCode, profileRequest.LabTestCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsProfileOutLab, profileRequest.IsProfileOutLab));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestApplicable, profileRequest.TestApplicable));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsLMP, profileRequest.IsLMP));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsNABApplicable, profileRequest.IsNABApplicable));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProfileFooter, profileRequest.ProfileFooter));

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
    }

}
