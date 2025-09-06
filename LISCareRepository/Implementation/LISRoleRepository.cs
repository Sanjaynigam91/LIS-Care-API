using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.LISRoles;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data;
using System.Net;
using Microsoft.Data.SqlClient;
using LISCareDTO.MetaData;

namespace LISCareRepository.Implementation
{
    public class LISRoleRepository : ILISRoleRepository
    {
        private IConfiguration _configuration;
        private LISCareDbContext _dbContext;
        public LISRoleRepository(IConfiguration configuration, LISCareDbContext _DbContext)
        {
            _configuration = configuration;
            _dbContext = _DbContext;
        }
        /// <summary>
        /// This method is used to add new LIS role
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> AddNewLISRole(LISRoleRequestModel lISRoleRequest)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(lISRoleRequest.ToString()))
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspAddNewLISRole;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRoleCode, lISRoleRequest.RoleCode.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRoleName, lISRoleRequest.RoleName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.RoleStatus, lISRoleRequest.RoleStatus.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRoleType, lISRoleRequest.RoleType.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamDepartment, (lISRoleRequest.Department ?? string.Empty).Trim()));
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
        /// This method is used to Update LIS role
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> UpdateLISRole(LISRoleUpdateRequestModel lISRoleUpdate)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (lISRoleUpdate.RoleId > 0)
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspUpdateLISRole;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRoleId, lISRoleUpdate.RoleId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRoleCode, (lISRoleUpdate.RoleCode ?? string.Empty).Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRoleName, (lISRoleUpdate.RoleName ?? string.Empty).Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRoleStatus, lISRoleUpdate.RoleStatus.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRoleType, (lISRoleUpdate.RoleType ?? string.Empty).Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamDepartment, (lISRoleUpdate.Department ?? string.Empty).Trim()));

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
        /// This method is used to get All LIS Roles
        /// </summary>
        /// <returns>List<LISRoleResponseModel></returns>
        public List<LISRoleResponseModel> GetAllLISRoles()
        {
            List<LISRoleResponseModel> response = new List<LISRoleResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetAllLISRoles;
                cmd.CommandType = CommandType.StoredProcedure;


                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LISRoleResponseModel lISRole = new LISRoleResponseModel();
                    lISRole.RoleId = Convert.ToInt32(reader[ConstantResource.UserRoleId]);
                    lISRole.RoleName = reader[ConstantResource.RoleName] as string ?? string.Empty;
                    lISRole.RoleCode = reader[ConstantResource.RoleCode] as string ?? string.Empty;
                    lISRole.RoleType = reader[ConstantResource.RoleType] as string ?? string.Empty;
                    lISRole.Department = reader[ConstantResource.Department] as string ?? string.Empty;
                    lISRole.RoleStatus = reader[ConstantResource.RoleStatus] as string ?? string.Empty;
                    response.Add(lISRole);
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
        /// This method is used to get all LIS Role Type
        /// </summary>
        /// <returns>List<LISRoleTypeResponseModel></returns>
        public List<LISRoleTypeResponseModel> GetLISRoleType()
        {
            List<LISRoleTypeResponseModel> response = new List<LISRoleTypeResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetRoleType;
                cmd.CommandType = CommandType.StoredProcedure;

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LISRoleTypeResponseModel lISRoleType = new LISRoleTypeResponseModel();
                    lISRoleType.RoleType = Convert.ToString(reader[ConstantResource.ItemType]);
                    lISRoleType.RoleDescription = Convert.ToString(reader[ConstantResource.ItemDescription]);
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
        /// <summary>
        /// This method is used to get LIS role by roleId
        /// </summary>
        /// <returns>List<LISRoleResponseModel></returns>
        public LISRoleResponseModel GetLISRoleByRoleId(int roleId)
        {
            LISRoleResponseModel lISRole = new LISRoleResponseModel();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetLISRoleByRoleId;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamRoleId, SqlDbType.Int) { Value = roleId });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lISRole.RoleId = Convert.ToInt32(reader[ConstantResource.UserRoleId]);
                    lISRole.RoleName = Convert.ToString(reader[ConstantResource.RoleName]) ?? string.Empty;
                    lISRole.RoleCode = Convert.ToString(reader[ConstantResource.RoleCode]) ?? string.Empty;
                    lISRole.RoleType = Convert.ToString(reader[ConstantResource.RoleType]) ?? string.Empty;
                    lISRole.Department = Convert.ToString(reader[ConstantResource.Department]) ?? string.Empty;
                    lISRole.RoleStatus = Convert.ToString(reader[ConstantResource.RoleStatus]) ?? string.Empty;
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
            return lISRole;
        }

        public APIResponseModel<object> RoleDeletedById(int roleId)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (roleId > 0)
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspDeleteRolebyId;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter(ConstantResource.RoleId, roleId));

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
