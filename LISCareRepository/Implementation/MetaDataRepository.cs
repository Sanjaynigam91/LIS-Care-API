using LISCareDataAccess.LISCareDbContext;
using LISCareDTO.MetaData;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;
using LISCareDTO;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace LISCareRepository.Implementation
{
    public class MetaDataRepository : IMetaDataRepository
    {
        private IConfiguration _configuration;
        private LISCareDbContext _dbContext;
        public MetaDataRepository(IConfiguration configuration, LISCareDbContext _DbContext)
        {
            _configuration = configuration;
            _dbContext = _DbContext;
        }
        /// <summary>
        /// This method is used to create new master list
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> CreateNewMasterList(MasterListRequestModel masterList)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(masterList.ToString()))
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspCreateNewMasterList;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCategory, masterList.CategoryCode.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamItemType, masterList.ItemType.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamItemDescription, masterList.ItemDescription.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, masterList.PartnerId));

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
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
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
        /// <summary>
        /// This method is used to create new meta data tag
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> CreateNewMetaDataTag(MetaDataTagRequestModel metaDataTag)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(metaDataTag.ToString()))
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspCreateNewTag;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTagCode, metaDataTag.TagCode.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTagDescription, metaDataTag.TagDescription.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTagStatus, metaDataTag.TagStatus.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, metaDataTag.PartnerId));

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
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
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
        /// <summary>
        /// This method is used to delete master list
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> DeleteMasterList(int recordId)
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
                    command.CommandText = ConstantResource.UspDeleteMasterList;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRecordId, SqlDbType.Int) { Value = recordId });
                    //command.Parameters.Add(new SqlParameter(ConstantResource.ParamOwnerId, SqlDbType.Int) { Value = ownerId });

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
        /// <summary>
        /// This Method is used to get Meta Data By OwnerId
        /// </summary>
        /// <returns>List<MetaDataResponseModel></returns>
        public List<MetaDataResponseModel> GetAllMetaData(string partnerId)
        {
            List<MetaDataResponseModel> response = new List<MetaDataResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetAllTags;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.ToString()));

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(partnerId))
                    {
                        MetaDataResponseModel metaData = new MetaDataResponseModel();
                        metaData.TagId = Convert.ToInt32(reader[ConstantResource.TagId]);
                        metaData.TagCode = Convert.ToString(reader[ConstantResource.TagCode]) ?? string.Empty;
                        metaData.PartnerId = Convert.ToString(reader[ConstantResource.PartnerId]) ?? string.Empty;
                        metaData.TagDescription = Convert.ToString(reader[ConstantResource.TagDescription]) ?? string.Empty;
                        metaData.MetaStatus = Convert.ToString(reader[ConstantResource.TagStatus]) ?? string.Empty;
                        response.Add(metaData);
                    }
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

        public MetaDataResponseModel GetMetaDataTag(int tagId)
        {
            MetaDataResponseModel response = new MetaDataResponseModel();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetTagsByTagId;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamTagId, tagId));

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    response.TagId = Convert.ToInt32(reader[ConstantResource.TagId]);
                    response.TagCode = Convert.ToString(reader[ConstantResource.TagCode]) ?? string.Empty;
                    response.TagDescription = Convert.ToString(reader[ConstantResource.TagDescription]) ?? string.Empty;
                    response.MetaStatus = Convert.ToString(reader[ConstantResource.TagStatus]) ?? string.Empty;
                    response.PartnerId = Convert.ToString(reader[ConstantResource.PartnerId]) ?? string.Empty;
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

        /// <summary>
        /// This method is used to get Meta Data By Category
        /// </summary>
        /// <returns>List<MetaDataTagsResponseModel></returns>
        public List<MetaDataTagsResponseModel> GetMetaDataTagsByCategory(string category, string partnerId)
        {
            List<MetaDataTagsResponseModel> response = new List<MetaDataTagsResponseModel>();
            try
            {
                if (!string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(partnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = ConstantResource.UspGetAllTagsByCategory;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCategory, SqlDbType.VarChar) { Value = category });
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.ToString()));

                    DbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MetaDataTagsResponseModel metaDataTags = new MetaDataTagsResponseModel();
                        metaDataTags.RecordId = Convert.ToInt32(reader[ConstantResource.RecordId]);
                        metaDataTags.PartnerId = Convert.ToString(reader[ConstantResource.PartnerId]) ?? string.Empty;
                        metaDataTags.ItemType = Convert.ToString(reader[ConstantResource.ItemType]); ;
                        metaDataTags.ItemDescription = Convert.ToString(reader[ConstantResource.ItemDescription]);
                        metaDataTags.Category = Convert.ToString(reader[ConstantResource.Category]);
                        response.Add(metaDataTags);
                    }
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
        /// <summary>
        /// This method is used to Get Reporting Style
        /// </summary>
        /// <returns>List<MetaDataTagsResponseModel></returns>
        public List<MetaTagResponse> GetReportingStyle(string partnerId)
        {
            List<MetaTagResponse> response = new List<MetaTagResponse>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetReportingStyle;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.ToString()));

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(partnerId))
                    {
                        MetaTagResponse templates = new MetaTagResponse();
                        templates.itemType = Convert.ToString(reader[ConstantResource.ItemType]);
                        templates.itemDescription = Convert.ToString(reader[ConstantResource.ItemDescription]);
                        response.Add(templates);
                    }
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

        /// <summary>
        /// This method is used to Get Report Templates
        /// </summary>
        /// <returns>List<MetaDataTagsResponseModel></returns>
        public List<MetaTagResponse> GetReportTemplates(string partnerId)
        {
            List<MetaTagResponse> response = new List<MetaTagResponse>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetReportTemplates;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.ToString()));

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(partnerId))
                    {
                        MetaTagResponse templates = new MetaTagResponse();
                        templates.itemType = Convert.ToString(reader[ConstantResource.ItemType]);
                        templates.itemDescription = Convert.ToString(reader[ConstantResource.ItemDescription]);
                        response.Add(templates);
                    }
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
        /// <summary>
        /// This method is used to Get Specimen Type
        /// </summary>
        /// <returns>List<MetaDataTagsResponseModel></returns>
        public List<MetaTagResponse> GetSpecimenType(string partnerId)
        {
            List<MetaTagResponse> response = new List<MetaTagResponse>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetSpecimenType;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.ToString()));

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(partnerId))
                    {
                        MetaTagResponse templates = new MetaTagResponse();
                        templates.itemType = Convert.ToString(reader[ConstantResource.ItemType]);
                        templates.itemDescription = Convert.ToString(reader[ConstantResource.ItemDescription]);
                        response.Add(templates);
                    }
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
        public List<MetaTagResponse> GetSubTestDepartments(string partnerId)
        {
            List<MetaTagResponse> response = new List<MetaTagResponse>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetTestSubdepartments;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.ToString()));

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(partnerId))
                    {
                        MetaTagResponse templates = new MetaTagResponse();
                        templates.itemType = Convert.ToString(reader[ConstantResource.ItemType]);
                        templates.itemDescription = Convert.ToString(reader[ConstantResource.ItemDescription]);
                        response.Add(templates);
                    }
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

        /// <summary>
        /// This method is used to Get Test Departments
        /// </summary>
        /// <returns>List<MetaDataTagsResponseModel></returns>
        public List<MetaTagResponse> GetTestDepartments(string partnerId)
        {
            List<MetaTagResponse> response = new List<MetaTagResponse>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetTestDepartments;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.ToString()));

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(partnerId))
                    {
                        MetaTagResponse templates = new MetaTagResponse();
                        templates.itemType = Convert.ToString(reader[ConstantResource.ItemType]) ?? string.Empty;
                        templates.itemDescription = Convert.ToString(reader[ConstantResource.ItemDescription]) ?? string.Empty;
                        response.Add(templates);
                    }
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

        /// <summary>
        /// This method is used to Update master list
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> UpdateMasterList(MasterListRequestModel masterList)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(masterList.ToString()))
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspUpdateMasterList;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCategory, masterList.CategoryCode.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamItemType, masterList.ItemType.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamItemDescription, masterList.ItemDescription.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, masterList.PartnerId));

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
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = ConstantResource.Success;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = false;
                        response.ResponseMessage = ConstantResource.Failed;

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
        /// <summary>
        /// This method is used to update meta data tag
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> UpdateTagInfo(MetaTagModel metaDataTag)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (metaDataTag.TagId > 0 && !string.IsNullOrEmpty(metaDataTag.PartnerId))
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspUpdateTag;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTagId, metaDataTag.TagId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.TagDescription, metaDataTag.TagDescription.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.TagStatus, metaDataTag.TagStatus.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, metaDataTag.PartnerId));

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
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
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
