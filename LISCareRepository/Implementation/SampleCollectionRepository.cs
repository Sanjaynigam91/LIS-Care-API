using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.SampleCollectionPlace;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Net;


namespace LISCareRepository.Implementation
{
    public class SampleCollectionRepository : ISampleCollectionRepository
    {
        private IConfiguration _configuration;
        private LISCareDbContext _dbContext;

        public SampleCollectionRepository(IConfiguration configuration, LISCareDbContext _DbContext)
        {
            _configuration = configuration;
            _dbContext = _DbContext;
        }
        public List<SampleCollectedAtResponse> GetSampleCollectedPlace(string partnerId)
        {
            List<SampleCollectedAtResponse> response = new List<SampleCollectedAtResponse>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetAllSampleCollectedPlaces;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SampleCollectedAtResponse sample = new SampleCollectedAtResponse();
                    sample.PartnerId = Convert.ToString(reader[ConstantResource.PartnerId]) ?? string.Empty;
                    sample.SampleCollectedPlaceName = Convert.ToString(reader[ConstantResource.SampleCollectedAt]) ?? string.Empty;
                    sample.RecordId = Convert.ToInt32(reader[ConstantResource.RecordId]);
                    response.Add(sample);
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }

        public APIResponseModel<object> AddSampleCollectedPlaces(SampleCollectedRequest sampleCollected)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = (int)(HttpStatusCode.BadRequest),
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(sampleCollected.ToString()))
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspAddSampleCollectedPlaces;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, sampleCollected.PartnerId.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSamplePlace, sampleCollected.SampleColletedPlace.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamUpdatedBy, sampleCollected.UpdatedBy.Trim()));

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
                    _dbContext.Database.GetDbConnection().Open();
                    command.ExecuteScalar();
                    OutputParameterModel parameterModel = new OutputParameterModel
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value) ?? string.Empty,
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };
                    if (parameterModel.IsSuccess)
                    {
                        response.Status = parameterModel.IsSuccess;
                        response.StatusCode = (int)HttpStatusCode.OK;
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
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }

        public APIResponseModel<object> RemoveSamplePlace(int recordId, string partnerId)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (recordId > 0)
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspRemoveSamplePlace;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter(ConstantResource.RecordId, recordId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
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
                    _dbContext.Database.GetDbConnection().Open();
                    command.ExecuteScalar();
                    OutputParameterModel parameterModel = new OutputParameterModel
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value) ?? string.Empty,
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };
                    if (parameterModel.IsSuccess)
                    {
                        response.Status = parameterModel.IsSuccess;
                        response.StatusCode = (int)HttpStatusCode.OK;
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
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }
    }
}
