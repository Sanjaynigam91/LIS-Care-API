using Azure;
using Azure.Core;
using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.MetaData;
using LISCareReposotiory.Interface;
using LISCareUtility;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LISCareReposotiory.Implementation
{
    public class UserRepository : IUserRepository
    {
        private IConfiguration _configuration;
        private LISCareDbContext _dbContext;
        private readonly UploadImagePath _uploadImagePath;

        public UserRepository(IConfiguration configuration, LISCareDbContext _DbContext, IOptions<UploadImagePath> upldImagePath)
        {
            _configuration = configuration;
            _dbContext = _DbContext;
            _uploadImagePath = upldImagePath.Value;
        }
        /// <summary>
        /// This method is used to User Sign Up
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> SignUp(SignUpRequestModel signUpRequest)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(signUpRequest.ToString()))
                {

                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspSaveUserData;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.FirstName, signUpRequest.FirstName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.LastName, signUpRequest.LastName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.Email, signUpRequest.Email.Trim()));
                    var password = Common.Base64Encode(signUpRequest.Password);
                    command.Parameters.Add(new SqlParameter(ConstantResource.Password, password.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.MobileNumber, signUpRequest.MobileNumber.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.RoleId, signUpRequest.RoleId));
                    var partnerId = Common.GeneratePartnerId(); // Generate a 06-character long partner ID
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
        /// <summary>
        /// This method is used Get User Details
        /// </summary>
        /// <returns>List<LoginResponseModel></returns>
        public LoginResponseModel GetUserDetails(LoginRequsetModel loginRequset)
        {
            LoginResponseModel response = new LoginResponseModel();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetUserDetails;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.UserName, SqlDbType.VarChar) { Value = loginRequset.UserName });
                var password = Common.Base64Encode(loginRequset.Password);
                cmd.Parameters.Add(new SqlParameter(ConstantResource.Password, SqlDbType.VarChar) { Value = password });
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    response.UserId = Convert.ToInt32(reader[ConstantResource.UserId]);
                    response.FullName = Convert.ToString(reader[ConstantResource.FullName]) ?? string.Empty;
                    var userPassword = Common.Base64Decode(Convert.ToString(reader[ConstantResource.UserPassword]) ?? string.Empty);
                    response.Password = userPassword;
                    response.RoleId = Convert.ToInt32(reader[ConstantResource.UserRoleId]);
                    response.Email = Convert.ToString(reader[ConstantResource.UserEmail]) ?? string.Empty;
                    response.MobileNumber = Convert.ToString(reader[ConstantResource.PhoneNumber]) ?? string.Empty;
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
        /// This method is used Get All User Information
        /// </summary>
        /// <returns>List<LoginResponseModel></returns>
        public List<LoginResponseModel> GetAllUserInfo(string partnerId)
        {
            List<LoginResponseModel> response = new List<LoginResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetAllUserDetails;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                cmd.CommandType = CommandType.StoredProcedure;

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LoginResponseModel loginResponse = new LoginResponseModel();
                    loginResponse.UserId = Convert.ToInt32(reader[ConstantResource.UserId]);
                    loginResponse.FullName = Convert.ToString(reader[ConstantResource.FullName]) ?? string.Empty;
                    var userPassword = Common.Base64Decode(Convert.ToString(reader[ConstantResource.UserPassword]) ?? string.Empty);
                    loginResponse.Password = userPassword;
                    loginResponse.RoleId = Convert.ToInt32(reader[ConstantResource.UserRoleId]);
                    loginResponse.Email = Convert.ToString(reader[ConstantResource.UserEmail]) ?? string.Empty;
                    loginResponse.RoleType = Convert.ToString(reader[ConstantResource.RoleType]) ?? string.Empty;
                    loginResponse.RoleName = Convert.ToString(reader[ConstantResource.RoleName]) ?? string.Empty;
                    loginResponse.IsMobileVerified = Convert.ToBoolean(reader[ConstantResource.IsMobileVerified]);
                    loginResponse.IsEmailVerified = Convert.ToBoolean(reader[ConstantResource.IsEmailVerified]);
                    //  response.UserLogo = Configuration[ConstantResources.AppSettingsbaseUrl] + Convert.ToString(reader["UserLogo"]);
                    loginResponse.MobileNumber = Convert.ToString(reader[ConstantResource.PhoneNumber]) ?? string.Empty;
                    loginResponse.UserStatus = Convert.ToString(reader[ConstantResource.UserStatus]) ?? string.Empty;
                    loginResponse.PartnerId = Convert.ToString(reader[ConstantResource.PartnerId]) ?? string.Empty;
                    response.Add(loginResponse);
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
        /// This method is used Get User Details By UserId
        /// </summary>
        /// <returns>List<LoginResponseModel></returns>
        public LoginResponseModel GetUserDetailsByUserId(int userId)
        {
            LoginResponseModel response = new LoginResponseModel();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetUserDetailsByUserId;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.UserUniqueId, SqlDbType.Int) { Value = userId });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    response.UserId = Convert.ToInt32(reader[ConstantResource.UserId]);
                    response.FullName = Convert.ToString(reader[ConstantResource.FullName]) ?? string.Empty;
                    response.FirstName = Convert.ToString(reader[ConstantResource.LISUserFirstName]) ?? string.Empty;
                    response.LastName = Convert.ToString(reader[ConstantResource.LISUserLastName]) ?? string.Empty;
                    var userPassword = Common.Base64Decode(Convert.ToString(reader[ConstantResource.UserPassword]) ?? string.Empty);
                    response.Password = userPassword;
                    response.RoleId = Convert.ToInt32(reader[ConstantResource.UserRoleId]);
                    response.Email = Convert.ToString(reader[ConstantResource.UserEmail]) ?? string.Empty;
                    response.RoleType = Convert.ToString(reader[ConstantResource.RoleName]) ?? string.Empty;
                    response.IsMobileVerified = Convert.ToBoolean(reader[ConstantResource.IsMobileVerified]);
                    response.IsEmailVerified = Convert.ToBoolean(reader[ConstantResource.IsEmailVerified]);
                    //  response.UserLogo = Configuration[ConstantResources.AppSettingsbaseUrl] + Convert.ToString(reader["UserLogo"]);
                    response.MobileNumber = Convert.ToString(reader[ConstantResource.PhoneNumber]) ?? string.Empty;
                    response.DepartmentId = Convert.ToInt32(reader[ConstantResource.DepartmentId]);
                    response.UserStatus = Convert.ToString(reader[ConstantResource.UserStatus]) ?? string.Empty;

                    if (Convert.ToString(reader[ConstantResource.UserLogoPrefix]) != "")
                    {
                        var imgPath = _uploadImagePath.FolderPath + Convert.ToString(reader[ConstantResource.UserLogo]);
                        var profileImage = Common.ConvertImageToBase64(imgPath);
                        var finalUserImage = reader[ConstantResource.UserLogoPrefix] + profileImage;
                        response.UserLogo = finalUserImage;
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
        /// This method is used for user login
        /// </summary>
        /// <returns>List<LoginResponseModel></returns>
        public LoginResponseModel UserLogin(LoginRequsetModel loginRequset)
        {
            LoginResponseModel loginResponse = new LoginResponseModel();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspValidateUserLogin;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.UserName, SqlDbType.VarChar) { Value = loginRequset.UserName });
                var password = Common.Base64Encode(loginRequset.Password);
                cmd.Parameters.Add(new SqlParameter(ConstantResource.Password, SqlDbType.VarChar) { Value = password });
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    loginResponse.UserId = Convert.ToInt32(reader[ConstantResource.UserId]);
                    loginResponse.FullName = Convert.ToString(reader[ConstantResource.FullName]) ?? string.Empty;
                    var userPassword = Common.Base64Decode(Convert.ToString(reader[ConstantResource.UserPassword]) ?? string.Empty);
                    loginResponse.Password = userPassword;
                    loginResponse.RoleId = Convert.ToInt32(reader[ConstantResource.UserRoleId]);
                    loginResponse.Email = Convert.ToString(reader[ConstantResource.UserEmail]) ?? string.Empty;
                    loginResponse.RoleType = Convert.ToString(reader[ConstantResource.RoleType]) ?? string.Empty;
                    loginResponse.RoleName = Convert.ToString(reader[ConstantResource.RoleName]) ?? string.Empty;
                    loginResponse.MobileNumber = Convert.ToString(reader[ConstantResource.PhoneNumber]) ?? string.Empty;
                    loginResponse.UserStatus = Convert.ToString(reader[ConstantResource.UserStatus]) ?? string.Empty;
                    loginResponse.PartnerId = Convert.ToString(reader[ConstantResource.PartnerId]) ?? string.Empty;
                    string token = Common.GenerateToken(loginResponse.Email, loginResponse.RoleType, this._configuration);
                    loginResponse.Token = token;
                    loginResponse.Expires_in = DateTime.Now.AddMinutes(Convert.ToDouble(ConstantResource.tokenExpirationTime));
                    if (Convert.ToString(reader[ConstantResource.UserLogoPrefix]) != "")
                    {
                        var imgPath = _uploadImagePath.FolderPath + Convert.ToString(reader[ConstantResource.UserLogo]);
                        var profileImage = Common.ConvertImageToBase64(imgPath);
                        var finalUserImage = reader[ConstantResource.UserLogoPrefix] + profileImage;
                        loginResponse.UserLogo = finalUserImage;
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
            return loginResponse;
        }
        /// <summary>
        /// This method is used to get All LIS User information
        /// </summary>
        /// <returns>List<LISUserResponseModel></returns>
        public List<LISUserResponseModel> GetALlLISUserInformation()
        {
            List<LISUserResponseModel> response = new List<LISUserResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetAllLISUsers;
                cmd.CommandType = CommandType.StoredProcedure;

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LISUserResponseModel lISUesr = new LISUserResponseModel();
                    lISUesr.UserCode = Convert.ToString(reader[ConstantResource.UserCode]);
                    lISUesr.UserName = Convert.ToString(reader[ConstantResource.LISUserName]);
                    lISUesr.CreatedOn = Convert.ToDateTime(reader[ConstantResource.CreatedOn]);
                    lISUesr.CreatedBy = Convert.ToString(reader[ConstantResource.CreatedBy]);
                    lISUesr.UserType = Convert.ToString(reader[ConstantResource.UserType]);
                    lISUesr.AccountStatus = Convert.ToString(reader[ConstantResource.AccountStatus]);
                    //  var userPassword = Common.Base64Decode(Convert.ToString(reader[ConstantResource.UserPassword]));
                    lISUesr.Password = Convert.ToString(reader[ConstantResource.UserPassword]);
                    lISUesr.Default_Url = Convert.ToString(reader[ConstantResource.Default_Url]);
                    lISUesr.AssignedRole = Convert.ToString(reader[ConstantResource.AssignedRole]);
                    lISUesr.LastLogintime = Convert.ToDateTime(reader[ConstantResource.LastLogintime]);
                    lISUesr.CurrentLogintime = Convert.ToDateTime(reader[ConstantResource.CurrentLogintime]);
                    lISUesr.AccountId = Convert.ToInt32(reader[ConstantResource.AccountId]);
                    lISUesr.LastName = Convert.ToString(reader[ConstantResource.LISUserLastName]);
                    lISUesr.UserClassificationId = Convert.ToString(reader[ConstantResource.UserClassificationId]);
                    lISUesr.AuthenticationMode = Convert.ToString(reader[ConstantResource.AuthenticationMode]);
                    lISUesr.Department = Convert.ToString(reader[ConstantResource.Department]);
                    lISUesr.IsBlocked = Convert.ToBoolean(reader[ConstantResource.IsBlocked]);
                    lISUesr.RoleId = Convert.ToInt32(reader[ConstantResource.LISUserRoleId]);
                    lISUesr.RoleName = Convert.ToString(reader[ConstantResource.RoleName]);

                    response.Add(lISUesr);
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
        /// This method is used to GeT User Type Information
        /// </summary>
        /// <returns>List<UserTypeResponseModel></returns>
        public List<UserTypeResponseModel> GetUserTypeInformation(int ownerId)
        {
            List<UserTypeResponseModel> response = new List<UserTypeResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetUserType;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.OwnerId, SqlDbType.Int) { Value = ownerId });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserTypeResponseModel userType = new UserTypeResponseModel();
                    userType.UserType = Convert.ToString(reader[ConstantResource.ItemType]);
                    userType.UserTypeDescription = Convert.ToString(reader[ConstantResource.ItemDescription]);
                    userType.Category = Convert.ToString(reader[ConstantResource.Category]);
                    response.Add(userType);
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
        /// This method is used to Get Role Information
        /// </summary>
        /// <returns>List<LISRoleResponseModel></returns>
        public List<LISRoleResponseModel> GetRoleInformation(string userType)
        {
            List<LISRoleResponseModel> response = new List<LISRoleResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetUserRolesByUserType;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.Usertype, SqlDbType.VarChar) { Value = userType });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LISRoleResponseModel lISRole = new LISRoleResponseModel();
                    lISRole.RoleId = Convert.ToInt32(reader[ConstantResource.LISUserRoleId]);
                    lISRole.RoleCode = Convert.ToString(reader[ConstantResource.RoleCode]);
                    lISRole.RoleName = Convert.ToString(reader[ConstantResource.RoleName]);
                    lISRole.RoleType = Convert.ToString(reader[ConstantResource.RoleType]);
                    lISRole.RoleStatus = Convert.ToString(reader[ConstantResource.RoleStatus]);
                    lISRole.Department = Convert.ToString(reader[ConstantResource.Department]);
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
        /// This method is used to Save User Information
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> AddUserInfo(LISUserRequestModel lISUser)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(lISUser.ToString()))
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspUserSaveChanges;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamUserCode, lISUser.UserCode.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamUserName, lISUser.UserName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamUsertype, lISUser.UserType.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAccountstatus, lISUser.AccountStatus.Trim()));
                    var password = Common.Base64Encode(lISUser.Password);
                    command.Parameters.Add(new SqlParameter(ConstantResource.Password, password.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRolesAssigned, lISUser.AssignedRole.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamUserClassificationId, lISUser.UserClassificationId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAuthenticationMode, lISUser.AuthenticationMode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAccountId, lISUser.AccountId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCreatedBy, lISUser.CreatedBy));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamDepartment, lISUser.Department));
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
        /// This method is used to Update User Information
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> UpdateUserInfo(PartnerUserRequestModel partnerUser)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(partnerUser.ToString()))
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspUserUpdateChanges;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.UserUniqueId, partnerUser.UserId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.FirstName, partnerUser.FirstName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.LastName, partnerUser.LastName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.Email, partnerUser.Email.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.MobileNumber, partnerUser.MobileNumber.Trim()));
                    var password = Common.Base64Encode(partnerUser.Password);
                    command.Parameters.Add(new SqlParameter(ConstantResource.Password, password.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmDepartmentId, partnerUser.DepartmentId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.RoleId, partnerUser.RoleId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmUserStatus, partnerUser.UserStatus));
                    command.Parameters.Add(new SqlParameter(ConstantResource.PartnerId, partnerUser.PartnerId.Trim()));
                    byte[] imageData = Common.DecodeBase64(partnerUser.UserLogo);
                    string base64Prefix = Common.PrefixOfBase64(partnerUser.UserLogo);
                    string userlogoPrefix = base64Prefix + ",";
                    string fileName = Common.GetFileNameFromBase64(partnerUser.UserLogo);
                    string filePath = Common.SaveImageToFile(imageData, _uploadImagePath.FolderPath, fileName);
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmUserLogo, fileName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamUserLogoPrefix, userlogoPrefix.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamModifiedById, partnerUser.ModifiedById.Trim()));

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
        /// This method is used to check Aready Exists User Code
        /// </summary>
        /// <returns>List<AlreadyExistsUserCodeResponseModel></returns>
        public AlreadyExistsUserCodeResponseModel GetAreadyExistsUserCode(string userCode)
        {
            AlreadyExistsUserCodeResponseModel response = new AlreadyExistsUserCodeResponseModel();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetAreadyCodeExists;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamUserCode, SqlDbType.VarChar) { Value = userCode });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    response.UserCode = Convert.ToString(reader[ConstantResource.UserCode]);
                    response.UserName = Convert.ToString(reader[ConstantResource.LISUserName]);
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
        /// This method is used to Get All Roles By Department
        /// </summary>
        /// <returns>List<LISRoleResponseModel></returns>
        public List<LISRoleResponseModel> GetAllRolesByDepartment(string userType, string department)
        {
            List<LISRoleResponseModel> response = new List<LISRoleResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetUserRolesByDepartment;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.Usertype, SqlDbType.VarChar) { Value = userType });
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamDepartment, SqlDbType.VarChar) { Value = department });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LISRoleResponseModel lISRole = new LISRoleResponseModel();
                    lISRole.RoleId = Convert.ToInt32(reader[ConstantResource.LISUserRoleId]);
                    lISRole.RoleCode = Convert.ToString(reader[ConstantResource.RoleCode]);
                    lISRole.RoleName = Convert.ToString(reader[ConstantResource.RoleName]);
                    lISRole.RoleType = Convert.ToString(reader[ConstantResource.RoleType]);
                    lISRole.RoleStatus = Convert.ToString(reader[ConstantResource.RoleStatus]);
                    lISRole.Department = Convert.ToString(reader[ConstantResource.Department]);
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
        /// This method is used to Get User Details By User Code
        /// </summary>
        /// <returns>List<LISUserResponseModel></returns>
        public List<LISUserResponseModel> GetUserDetailsByUserCode(string userCode, int ownerId)
        {
            List<LISUserResponseModel> response = new List<LISUserResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetUsersByUserCode;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamUserCode, SqlDbType.VarChar) { Value = userCode });
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamAccountId, SqlDbType.VarChar) { Value = ownerId });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LISUserResponseModel lISUesr = new LISUserResponseModel();
                    lISUesr.UserCode = Convert.ToString(reader[ConstantResource.UserCode]);
                    lISUesr.UserName = Convert.ToString(reader[ConstantResource.LISUserName]);
                    lISUesr.CreatedOn = Convert.ToDateTime(reader[ConstantResource.CreatedOn]);
                    lISUesr.CreatedBy = Convert.ToString(reader[ConstantResource.CreatedBy]);
                    lISUesr.UserType = Convert.ToString(reader[ConstantResource.UserType]);
                    lISUesr.AccountStatus = Convert.ToString(reader[ConstantResource.AccountStatus]);
                    var userPassword = Common.Base64Decode(Convert.ToString(reader[ConstantResource.UserPassword]));
                    lISUesr.Password = userPassword;
                    lISUesr.Default_Url = Convert.ToString(reader[ConstantResource.Default_Url]);
                    lISUesr.AssignedRole = Convert.ToString(reader[ConstantResource.AssignedRole]);
                    lISUesr.AccountId = Convert.ToInt32(reader[ConstantResource.AccountId]);
                    lISUesr.LastName = Convert.ToString(reader[ConstantResource.LISUserLastName]);
                    lISUesr.UserClassificationId = Convert.ToString(reader[ConstantResource.UserClassificationId]);
                    lISUesr.AuthenticationMode = Convert.ToString(reader[ConstantResource.AuthenticationMode]);
                    lISUesr.Department = Convert.ToString(reader[ConstantResource.UserDepartment]);
                    if (reader.IsDBNull(ConstantResource.IsBlocked))
                    {
                        lISUesr.IsBlocked = false;
                    }
                    else
                    {
                        lISUesr.IsBlocked = Convert.ToBoolean(reader[ConstantResource.IsBlocked]);
                    }
                    response.Add(lISUesr);
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
        /// This method is used to Search User details
        /// </summary>
        /// <returns>List<UserResponseModel></returns>
        public List<UserResponseModel> SearchUsers(string? userName, string userType, string accountStatus, int accountId)
        {
            List<UserResponseModel> response = new List<UserResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspSearchUser;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamUserName, SqlDbType.VarChar) { Value = userName });
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamUsertype, SqlDbType.VarChar) { Value = userType });
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamAccountstatus, SqlDbType.VarChar) { Value = accountStatus });
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamAccountId, SqlDbType.VarChar) { Value = accountId });

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserResponseModel lISUesr = new UserResponseModel();
                    lISUesr.UserCode = Convert.ToString(reader[ConstantResource.UserCode]);
                    lISUesr.UserName = Convert.ToString(reader[ConstantResource.LISUserName]);
                    lISUesr.AccountStatus = Convert.ToString(reader[ConstantResource.AccountStatus]);
                    lISUesr.UserCategory = Convert.ToString(reader[ConstantResource.UserCategory]);
                    lISUesr.RoleName = Convert.ToString(reader[ConstantResource.RoleName]);
                    lISUesr.AssociatePartnerCode = Convert.ToString(reader[ConstantResource.UserClassificationId]);
                    response.Add(lISUesr);
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

        public List<LoginResponseModel> SearchAllUsers(UserRequestModel userRequest)
        {
            List<LoginResponseModel> response = new List<LoginResponseModel>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspSearchUserDetails;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, userRequest.PartnerId.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmUserStatus, userRequest.UserStatus.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamRoleType, userRequest.RoleType.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.Email, userRequest.Email.Trim()));
                cmd.CommandType = CommandType.StoredProcedure;

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LoginResponseModel loginResponse = new LoginResponseModel();
                    loginResponse.UserId = Convert.ToInt32(reader[ConstantResource.UserId]);
                    loginResponse.FullName = Convert.ToString(reader[ConstantResource.FullName]) ?? string.Empty;
                    var userPassword = Common.Base64Decode(Convert.ToString(reader[ConstantResource.UserPassword]) ?? string.Empty);
                    loginResponse.Password = userPassword;
                    loginResponse.RoleId = Convert.ToInt32(reader[ConstantResource.UserRoleId]);
                    loginResponse.Email = Convert.ToString(reader[ConstantResource.UserEmail]) ?? string.Empty;
                    loginResponse.RoleType = Convert.ToString(reader[ConstantResource.RoleType]) ?? string.Empty;
                    loginResponse.RoleName = Convert.ToString(reader[ConstantResource.RoleName]) ?? string.Empty;
                    loginResponse.IsMobileVerified = Convert.ToBoolean(reader[ConstantResource.IsMobileVerified]);
                    loginResponse.IsEmailVerified = Convert.ToBoolean(reader[ConstantResource.IsEmailVerified]);
                    //  response.UserLogo = Configuration[ConstantResources.AppSettingsbaseUrl] + Convert.ToString(reader["UserLogo"]);
                    loginResponse.MobileNumber = Convert.ToString(reader[ConstantResource.PhoneNumber]) ?? string.Empty;
                    loginResponse.UserStatus = Convert.ToString(reader[ConstantResource.UserStatus]) ?? string.Empty;
                    loginResponse.PartnerId = Convert.ToString(reader[ConstantResource.PartnerId]) ?? string.Empty;
                    response.Add(loginResponse);
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

        public List<LabDepartmentResponse> GetAllDepartments()
        {
            List<LabDepartmentResponse> response = new List<LabDepartmentResponse>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetAllDepartment;
                cmd.CommandType = CommandType.StoredProcedure;

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    LabDepartmentResponse labDepartment = new LabDepartmentResponse();
                    labDepartment.DepartmentId = Convert.ToInt32(reader[ConstantResource.DepartmentId]);
                    labDepartment.DepartmentName = Convert.ToString(reader[ConstantResource.DepartmentName]) ?? string.Empty;
                    response.Add(labDepartment);
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

        public APIResponseModel<object> AddPartnerUsers(PartnerUserRequestModel partnerUser)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(partnerUser.ToString()))
                {

                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspSavePartnerUsers;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.FirstName, partnerUser.FirstName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.LastName, partnerUser.LastName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.Email, partnerUser.Email.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.MobileNumber, partnerUser.MobileNumber.Trim()));
                    var password = Common.Base64Encode(partnerUser.Password);
                    command.Parameters.Add(new SqlParameter(ConstantResource.Password, password.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmDepartmentId, partnerUser.DepartmentId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.RoleId, partnerUser.RoleId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmUserStatus, partnerUser.UserStatus));
                    command.Parameters.Add(new SqlParameter(ConstantResource.PartnerId, partnerUser.PartnerId.Trim()));
                    byte[] imageData = Common.DecodeBase64(partnerUser.UserLogo);
                    string base64Prefix = Common.PrefixOfBase64(partnerUser.UserLogo);
                    string userlogoPrefix = base64Prefix + ",";
                    string fileName = Common.GetFileNameFromBase64(partnerUser.UserLogo);
                    string filePath = Common.SaveImageToFile(imageData, _uploadImagePath.FolderPath, fileName);
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmUserLogo, fileName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamUserLogoPrefix, userlogoPrefix.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmCreatedById, partnerUser.CreatedById.Trim()));


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
                        response.Status = parameterModel.IsSuccess;
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

        public APIResponseModel<object> UserDeletedById(string userId, string delById)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(userId.ToString()) && !string.IsNullOrEmpty(delById.ToString()))
                {

                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspDeleteUserbyId;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.UserUniqueId, userId.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamDeletedById, delById.Trim()));


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
