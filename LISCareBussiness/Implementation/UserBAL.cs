using LISCare.Interface;
using LISCareDTO;
using LISCareDTO.MetaData;
using LISCareReposotiory.Interface;
using Microsoft.Extensions.Configuration;


namespace LISCare.Implementation
{
    public class UserBAL : IUser
    {
        private readonly IConfiguration Configuration;
        private readonly IUserRepository UserRepository;
        public UserBAL(IConfiguration configuration, IUserRepository userRepository)
        {
            Configuration = configuration;
            UserRepository = userRepository;
        }
        // This method is used for Signup
        public APIResponseModel<object> SignUp(SignUpRequestModel signUpRequest)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = UserRepository.SignUp(signUpRequest);
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }
        // This Method used to Get User Details
        public LoginResponseModel GetUserDetails(LoginRequsetModel loginRequset)
        {
            var response = new LoginResponseModel();
            try
            {
                response = UserRepository.GetUserDetails(loginRequset);
            }
            catch 
            {
                throw;
            }
            return response;

        }

        public List<LoginResponseModel> GetAllUserInfo(string partnerId)
        {
            var response = new List<LoginResponseModel>();
            try
            {
                response = UserRepository.GetAllUserInfo(partnerId);
            }
            catch
            {
                throw;
            }
            return response;

        }

        public LoginResponseModel GetUserDetailsByUserId(int userId)
        {
            var response = new LoginResponseModel();
            try
            {
                response = UserRepository.GetUserDetailsByUserId(userId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public LoginResponseModel UserLogin(LoginRequsetModel loginRequset)
        {
            var response = new LoginResponseModel();
            try
            {
              response = UserRepository.UserLogin(loginRequset);
            }
            catch
            {
                throw;
            }
            return response;
        }
        /// <summary>
        /// This method is used to get All LIS User information
        /// </summary>
        /// <returns>List<LISUserResponseModel></returns>

        public List<LISUserResponseModel> GetALlLISUserInformation()
        {
            var response = new List<LISUserResponseModel>();
            try
            {
                response = UserRepository.GetALlLISUserInformation();
            }
            catch
            {
                throw;
            }
            return response;
        }
        /// <summary>
        /// This method is used to get UserType information
        /// </summary>
        /// <returns>List<UserTypeResponseModel></returns>
        public List<UserTypeResponseModel> GetUserTypeInformation(int ownerId)
        {
            var response = new List<UserTypeResponseModel>();
            try
            {
                response = UserRepository.GetUserTypeInformation(ownerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<LISRoleResponseModel> GetRoleInformation(string userType)
        {
            var response = new List<LISRoleResponseModel>();
            try
            {
                response = UserRepository.GetRoleInformation(userType);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public APIResponseModel<object> AddUserInfo(LISUserRequestModel lISUser)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = UserRepository.AddUserInfo(lISUser);
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }

        public APIResponseModel<object> UpdateUserInfo(PartnerUserRequestModel partnerUser)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = UserRepository.UpdateUserInfo(partnerUser);
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }

        public AlreadyExistsUserCodeResponseModel GetAreadyExistsUserCode(string userCode)
        {
            var response = new AlreadyExistsUserCodeResponseModel();
            try
            {
                response = UserRepository.GetAreadyExistsUserCode(userCode);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<LISRoleResponseModel> GetAllRolesByDepartment(string userType, string department)
        {
            var response = new List<LISRoleResponseModel>();
            try
            {
                response = UserRepository.GetAllRolesByDepartment(userType, department);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<LISUserResponseModel> GetUserDetailsByUserCode(string userCode, int ownerId)
        {
            var response = new List<LISUserResponseModel>();
            try
            {
                response = UserRepository.GetUserDetailsByUserCode(userCode, ownerId);
            }
            catch
            {
                throw;
            }
            return response;
        }
        /// <summary>
        /// This method is used to Search User details
        /// </summary>
        /// <returns>List<UserResponseModel></returns>
        public List<UserResponseModel> SearchUsers(string? userName, string userType, string accountStatus, int accountId)
        {
            var response = new List<UserResponseModel>();
            try
            {
                response = UserRepository.SearchUsers(userName, userType,accountStatus,accountId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<LoginResponseModel> SearchAllUsers(UserRequestModel userRequest)
        {
            var response = new List<LoginResponseModel>();
            try
            {
                response = UserRepository.SearchAllUsers(userRequest);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<LabDepartmentResponse> GetAllDepartments()
        {
            var response = new List<LabDepartmentResponse>();
            try
            {
                response = UserRepository.GetAllDepartments();
            }
            catch
            {
                throw;
            }
            return response;
        }

        public APIResponseModel<object> AddPartnerUsers(PartnerUserRequestModel partnerUser)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = UserRepository.AddPartnerUsers(partnerUser);
            }
            catch { throw; }
            return response;
        }

        public APIResponseModel<object> UserDeletedById(string userId, string delById)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = UserRepository.UserDeletedById(userId,delById);
            }
            catch { throw; }
            return response;
        }
    }
}
