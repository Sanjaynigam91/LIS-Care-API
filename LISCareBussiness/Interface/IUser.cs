using LISCareDTO;
using LISCareDTO.MetaData;


namespace LISCare.Interface
{
    public interface IUser
    {
        // This interface is used for user signup.
        // This interface used SignupRequset.
        APIResponseModel<object> SignUp(SignUpRequestModel signUpRequest);
        // This interface used to Get User Details
        LoginResponseModel GetUserDetails(LoginRequsetModel loginRequset);
        List<LoginResponseModel> GetAllUserInfo(string partnerId);
        LoginResponseModel GetUserDetailsByUserId(int userId);
        LoginResponseModel UserLogin(LoginRequsetModel loginRequset);
        /// <summary>
        /// This interface is used to get All LIS User information
        /// </summary>
        /// <returns>List<LISUserResponseModel></returns>
        List<LISUserResponseModel> GetALlLISUserInformation();
        /// <summary>
        /// This interface is used to get UserType information
        /// </summary>
        /// <returns>List<UserTypeResponseModel></returns>
        List<UserTypeResponseModel> GetUserTypeInformation(int ownerId);
        /// <summary>
        /// This interface is used to get Role information
        /// </summary>
        List<LISRoleResponseModel> GetRoleInformation(string userType);
        /// <summary>
        /// This interface is used to Save User Information
        /// </summary>
        APIResponseModel<object> AddUserInfo(LISUserRequestModel lISUser);
        /// <summary>
        /// This interface is used to Save User Information
        /// </summary>
        APIResponseModel<object> UpdateUserInfo(PartnerUserRequestModel partnerUser);
        /// <summary>
        /// This interface is used to to check already exist user code
        /// </summary>
        AlreadyExistsUserCodeResponseModel GetAreadyExistsUserCode(string userCode);
        /// <summary>
        /// This interface is used to get Role information by department and user type
        /// </summary>
        List<LISRoleResponseModel> GetAllRolesByDepartment(string userType,string department);
        /// <summary>
        /// This interface is used to get LIS User by User Code
        /// </summary>
        /// <returns>List<LISUserResponseModel></returns>
        List<LISUserResponseModel> GetUserDetailsByUserCode(string userCode, int ownerId);
        /// <summary>
        /// This interface is used to Search User details
        /// </summary>
        /// <returns>List<UserResponseModel></returns>
        List<UserResponseModel> SearchUsers(string? userName, string userType, string accountStatus, int accountId);
        List<LoginResponseModel> SearchAllUsers(UserRequestModel userRequest);
        List<LabDepartmentResponse> GetAllDepartments();
        // This interface used add Users
        APIResponseModel<object> AddPartnerUsers(PartnerUserRequestModel partnerUser);
        // This interface used delete Users
        APIResponseModel<object> UserDeletedById(string userId, string delById);
    }
}
