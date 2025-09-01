using LISCareDataAccess.LISCareDbContext;
using LISCareDTO.GlobalRoleAccess;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;
using LISCareDTO;
using System.Net;
using LISCareDTO.LISRoles;
using Microsoft.IdentityModel.Tokens;

namespace LISCareRepository.Implementation
{
    public class GlobalRoleAccessRepository : IGlobalRoleAccessRepository
    {
        private readonly IConfiguration _configuration;
        private readonly LISCareDbContext _dbContext;
        public GlobalRoleAccessRepository(IConfiguration configuration, LISCareDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public APIResponseModel<object> DeleteLisPageById(string pageId)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(pageId))
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.Usp_DeletePageDetails;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPageId, pageId));

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
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value),
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = ConstantResource.PageAccessMsg;
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

        public List<LisCareCriteriaModel> GetAllCriteria()
        {
            List<LisCareCriteriaModel> response = new List<LisCareCriteriaModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetAllCriteria;
                cmd.CommandType = CommandType.StoredProcedure;


                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LisCareCriteriaModel lisCareCriteria = new LisCareCriteriaModel();
                    lisCareCriteria.CriteriaId = Convert.ToInt32(reader[ConstantResource.CriteriaId]);
                    lisCareCriteria.Criteria = Convert.ToString(reader[ConstantResource.Criteria]);
                    response.Add(lisCareCriteria);
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

        public List<LabRolesResponse> GetAllLabRole()
        {
            List<LabRolesResponse> response = new List<LabRolesResponse>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetLabRoles;
                cmd.CommandType = CommandType.StoredProcedure;

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LabRolesResponse labRoles = new LabRolesResponse();
                    labRoles.RoleId = Convert.ToInt32(reader[ConstantResource.UserRoleId]);
                    labRoles.RoleName = Convert.ToString(reader[ConstantResource.RoleName]) ?? string.Empty;
                    response.Add(labRoles);
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

        public List<LisPageResponseModel> GetAllLisPages(string partnerId)
        {
            List<LisPageResponseModel> response = new List<LisPageResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetAllLisPages;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, SqlDbType.VarChar) { Value = partnerId });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LisPageResponseModel lisPage = new LisPageResponseModel();
                    lisPage.NavigationId = Convert.ToString(reader[ConstantResource.NavigationId]);
                    lisPage.PageName = Convert.ToString(reader[ConstantResource.PageName]);
                    lisPage.PageEntity = Convert.ToString(reader[ConstantResource.PageEntity]);
                    lisPage.Criteria = Convert.ToString(reader[ConstantResource.Criteria]);
                    lisPage.PartnerId = Convert.ToString(reader[ConstantResource.PartnerId]);
                    if (Convert.ToBoolean(reader[ConstantResource.IsActive]))
                    {
                        lisPage.Status = "Active";
                    }
                    else
                    {
                        lisPage.Status = "InActive";
                    }

                    response.Add(lisPage);
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

        public List<LisPageModel> GetAllPage(string partnerId, string pageEntity)
        {
            List<LisPageModel> response = new List<LisPageModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetLisPages;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, SqlDbType.VarChar) { Value = partnerId });
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamPageEntity, SqlDbType.VarChar) { Value = pageEntity });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LisPageModel lisPage = new LisPageModel();
                    lisPage.PageId = Convert.ToInt32(reader[ConstantResource.LisPageId]);
                    lisPage.PageName = Convert.ToString(reader[ConstantResource.PageName]);
                    response.Add(lisPage);
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

        public List<PageEntityModel> GetAllPageEntity(string partnerId)
        {
            List<PageEntityModel> response = new List<PageEntityModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetPageEntity;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, SqlDbType.VarChar) { Value = partnerId });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PageEntityModel lisPageEntity = new PageEntityModel();
                    lisPageEntity.PageEntity = Convert.ToString(reader[ConstantResource.PageEntity]);
                    lisPageEntity.PartnerId = Convert.ToString(reader[ConstantResource.PartnerId]);
                    response.Add(lisPageEntity);
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
        /// This method is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<PageHeaderResponseModel></returns>
        public List<PageHeaderResponseModel> GetAllPageHeaders(int roleId, string partnerId)
        {
            List<PageHeaderResponseModel> response = new List<PageHeaderResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetAllPageHeaders;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamRoleId, SqlDbType.Int) { Value = roleId });
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, SqlDbType.NVarChar) { Value = partnerId });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PageHeaderResponseModel pageHeader = new PageHeaderResponseModel();
                    pageHeader.NavigationId = Convert.ToString(reader[ConstantResource.NavigationId]);
                    pageHeader.UrlLabel = Convert.ToString(reader[ConstantResource.UrlLabel]);
                    pageHeader.MessageHeader = Convert.ToString(reader[ConstantResource.MessageHeader]);
                    pageHeader.MenuId = Convert.ToString(reader[ConstantResource.MenuId]);
                    pageHeader.RoleId = Convert.ToString(reader[ConstantResource.UserRoleId]);
                    if (DBNull.Value.Equals(reader[ConstantResource.Visibility]))
                    {
                        pageHeader.Visibility = false;
                    }
                    else
                    {
                        pageHeader.Visibility = Convert.ToBoolean(reader[ConstantResource.Visibility]);
                    }
                    if (DBNull.Value.Equals(reader[ConstantResource.IsReadEnabled]))
                    {
                        pageHeader.IsReadEnabled = false;
                    }
                    else
                    {
                        pageHeader.IsReadEnabled = Convert.ToBoolean(reader[ConstantResource.IsReadEnabled]);
                    }
                    if (DBNull.Value.Equals(reader[ConstantResource.IsWriteEnabled]))
                    {
                        pageHeader.IsWriteEnabled = false;
                    }
                    else
                    {
                        pageHeader.IsWriteEnabled = Convert.ToBoolean(reader[ConstantResource.IsWriteEnabled]);
                    }
                    if (DBNull.Value.Equals(reader[ConstantResource.IsApproveEnabled]))
                    {
                        pageHeader.IsApproveEnabled = false;
                    }
                    else
                    {
                        pageHeader.IsApproveEnabled = Convert.ToBoolean(reader[ConstantResource.IsApproveEnabled]);
                    }
                    if (DBNull.Value.Equals(reader[ConstantResource.IsSpecialPermssion]))
                    {
                        pageHeader.IsSpecialPermssion = false;
                    }
                    else
                    {
                        pageHeader.IsSpecialPermssion = Convert.ToBoolean(reader[ConstantResource.IsSpecialPermssion]);
                    }
                    
                    if (reader.IsDBNull(ConstantResource.PartnerId))
                    {
                        pageHeader.PartnerId = string.Empty;
                    }
                    else
                    {
                        pageHeader.PartnerId = Convert.ToString(reader[ConstantResource.PartnerId]);
                    }

                    response.Add(pageHeader);
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
        /// This method is used to get all Role Type
        /// </summary>
        /// <returns>List<LISRoleTypeResponseModel></returns>
        public List<RoleTypeResponseModel> GetAllRoleType()
        {
            List<RoleTypeResponseModel> response = new List<RoleTypeResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetLISRoleType;
                cmd.CommandType = CommandType.StoredProcedure;

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RoleTypeResponseModel lISRoleType = new RoleTypeResponseModel();
                    lISRoleType.RoletypeId = Convert.ToInt32(reader[ConstantResource.RoletypeId]);
                    lISRoleType.RoleType = Convert.ToString(reader[ConstantResource.RoleType]) ?? string.Empty;
                    response.Add(lISRoleType);  
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

        public LisPageResponseModel GetPageDetailsById(string pageId, string partnerId)
        {
            LisPageResponseModel response = new LisPageResponseModel();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetPagesByPageId;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamPageId, SqlDbType.NVarChar) { Value = pageId });
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, SqlDbType.NVarChar) { Value = partnerId });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    response.NavigationId = Convert.ToString(reader[ConstantResource.NavigationId]);
                    response.PageName = Convert.ToString(reader[ConstantResource.PageName]);
                    response.PageEntity = Convert.ToString(reader[ConstantResource.PageEntity]);
                    response.Criteria = Convert.ToString(reader[ConstantResource.Criteria]);
                    response.PartnerId = Convert.ToString(reader[ConstantResource.PartnerId]);
                    if (Convert.ToBoolean(reader[ConstantResource.IsActive]))
                    {
                        response.Status = "Active";
                    }
                    else
                    {
                        response.Status = "InActive";
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
        /// This method is used to get roles by role type
        /// </summary>
        /// <returns>List<LISRoleResponseModel></returns>
        public List<RoleResponseModel> GetRolesByRoleType(string roleType)
        {
            List<RoleResponseModel> response = new List<RoleResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetRolesByRoleType;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamRoleType, SqlDbType.VarChar) { Value = roleType });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RoleResponseModel lISRole = new RoleResponseModel();
                    lISRole.RoleId = Convert.ToInt32(reader[ConstantResource.UserRoleId]);
                    lISRole.RoleName = Convert.ToString(reader[ConstantResource.RoleName]) ?? string.Empty;
                    response.Add(lISRole);
                }
            }catch
            {
                throw;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }

        public APIResponseModel<object> SaveAllLisPageAccess(LisPageRequestModel lisPageRequest)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(lisPageRequest.PageName))
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspSaveLisPageNavigations;
                    command.CommandType = CommandType.StoredProcedure;
                    Guid navigationId = Guid.NewGuid();
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamNavigationId, navigationId.ToString()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPageName, lisPageRequest.PageName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPageEntity, lisPageRequest.PageEntity.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCriteria, lisPageRequest.Criteria.Trim()));
                    Boolean status = false;
                    if (lisPageRequest.IsActive == "Active")
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsActive, status));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, lisPageRequest.PartnerId.ToString()));

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
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value),
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = ConstantResource.PageSuccess;
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
        /// This method is used to Save All Page Access
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> SaveAllPageAccess(RolePermissionRequestModel rolePermission)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (rolePermission.RoleId > 0 && rolePermission.PartnerId!=null)
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspSaveAllRoleAceess;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamVisibility, Convert.ToBoolean(rolePermission.Visibility)));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRead, Convert.ToBoolean(rolePermission.IsReadEnabled)));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamWrite, Convert.ToBoolean(rolePermission.IsWriteEnabled)));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamApprove, Convert.ToBoolean(rolePermission.IsApproveEnabled)));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSpermission, Convert.ToBoolean(rolePermission.IsSpecialPermssion)));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamMenuid, Convert.ToString(rolePermission.MenuId.Trim())));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRoleId, rolePermission.RoleId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, rolePermission.PartnerId));

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
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value),
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = ConstantResource.SaveAccessMsg;
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

        public List<LisPageResponseModel> SearchLisPages(LisPageSearchRequestModel lisPageSearch)
        {
            List<LisPageResponseModel> response = new List<LisPageResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspSearchLisPages;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, SqlDbType.VarChar) { Value = lisPageSearch.PartnerId });
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCriteria, SqlDbType.VarChar) { Value = lisPageSearch.Criteria });
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamPageEntity, SqlDbType.VarChar) { Value = lisPageSearch.PageEntity });
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamPageName, SqlDbType.VarChar) { Value = lisPageSearch.PageName });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LisPageResponseModel lisPage = new LisPageResponseModel();
                    lisPage.NavigationId = Convert.ToString(reader[ConstantResource.NavigationId]);
                    lisPage.PageName = Convert.ToString(reader[ConstantResource.PageName]);
                    lisPage.PageEntity = Convert.ToString(reader[ConstantResource.PageEntity]);
                    lisPage.Criteria = Convert.ToString(reader[ConstantResource.Criteria]);
                    lisPage.PartnerId = Convert.ToString(reader[ConstantResource.PartnerId]);
                    if (Convert.ToBoolean(reader[ConstantResource.IsActive]))
                    {
                        lisPage.Status = "Active";
                    }
                    else
                    {
                        lisPage.Status = "InActive";
                    }

                    response.Add(lisPage);
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
        /// This method is used to update role permission
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> UpdatePageAccess(RolePermissionRequestModel rolePermission)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (rolePermission.RoleId > 0 && rolePermission.PartnerId != null)
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspUpdateData;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamVisibility, Convert.ToBoolean(rolePermission.Visibility)));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRead, Convert.ToBoolean(rolePermission.IsReadEnabled)));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamWrite, Convert.ToBoolean(rolePermission.IsWriteEnabled)));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamApprove, Convert.ToBoolean(rolePermission.IsApproveEnabled)));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSpermission, Convert.ToBoolean(rolePermission.IsSpecialPermssion)));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamMenuid, Convert.ToString(rolePermission.MenuId.Trim())));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRoleId, rolePermission.RoleId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, rolePermission.PartnerId));

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
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value),
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = ConstantResource.PageAccessMsg;
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

        public APIResponseModel<object> UpdatePageDetails(LisPageUpdateRequestModel lisPageUpdate)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(lisPageUpdate.PageId))
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspUpdatePageDetails;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPageId, lisPageUpdate.PageId.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPageName, lisPageUpdate.PageName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPageEntity, lisPageUpdate.PageEntity.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCriteria, lisPageUpdate.Criteria.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, lisPageUpdate.PartnerId.Trim()));
                    Boolean status = false;
                    if (lisPageUpdate.IsActive == "Active")
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsActive, status));

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
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value),
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = ConstantResource.PageAccessMsg;
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
    }
}
