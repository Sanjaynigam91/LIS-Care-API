using LISCare.Interface;
using LISCareDTO;
using LISCareDTO.MetaData;
using LISCareUtility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LISCare.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]

    public class SignUpController : ControllerBase
    {
        private IConfiguration Configuration;
        private new IUser User;

        public SignUpController(IConfiguration configuration, IUser user)
        {
            Configuration = configuration;
            User = user;
        }
        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        [Route(ConstantResource.UserSignUp)]
        public IActionResult UserSignup(SignUpRequestModel signUpRequest)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.SignUp(signUpRequest);
            if (result.Status && result.StatusCode == (int)HttpStatusCode.OK)
            {
                responseModel.Status = result.Status;
                responseModel.StatusCode = result.StatusCode;
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = string.Empty;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = result.Status;
                responseModel.StatusCode = result.StatusCode;
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = string.Empty;
                return NotFound(responseModel);
            }

        }
        [HttpGet]
        [Route(ConstantResource.GetAllUsers)]
        public IActionResult GetAllUserInfo(string partnerId)
       {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.GetAllUserInfo(partnerId);
            if (result != null && result.Count > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.Success;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = ConstantResource.Failed;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        [HttpGet]
        [Route(ConstantResource.GetUserById)]
        public IActionResult GetUserDetailsByUserId(int userId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.GetUserDetailsByUserId(userId);
            if (result.UserId > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.Success;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = ConstantResource.InvaidUser;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        [HttpGet]
        [Route(ConstantResource.GetAllLISUsers)]
        public IActionResult GetAllLISUsers()
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.GetALlLISUserInformation();
            if (result != null && result.Count > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.Success;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = ConstantResource.Failed;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        [HttpGet]
        [Route(ConstantResource.GetUserTypeByOwnerId)]
        public IActionResult GetUserTypeDetails(int ownerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.GetUserTypeInformation(ownerId);
            if (result.Count > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.Success;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = ConstantResource.InvaidUser;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        [HttpGet]
        [Route(ConstantResource.GetUserRolesByUserType)]
        public IActionResult GetUserRolesByUserType(string userType)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.GetRoleInformation(userType);
            if (result.Count > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.Success;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = ConstantResource.InvaidUser;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        [HttpPost]
        [Route(ConstantResource.SaveUserInformation)]
        public IActionResult AddUserInfo(LISUserRequestModel lISUser)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.AddUserInfo(lISUser);
            if (result.Status && result.StatusCode == (int)HttpStatusCode.OK)
            {
                responseModel.Status = result.Status;
                responseModel.StatusCode = result.StatusCode;
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = string.Empty;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = result.Status;
                responseModel.StatusCode = result.StatusCode;
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = string.Empty;
                return NotFound(responseModel);
            }

        }
        [HttpPut]
        [Route(ConstantResource.UpdateUserInformation)]
        public IActionResult UpdateUserDetails(PartnerUserRequestModel partnerUser)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.UpdateUserInfo(partnerUser);
            if (result.Status && result.StatusCode == (int)HttpStatusCode.OK)
            {
                responseModel.Status = result.Status;
                responseModel.StatusCode = result.StatusCode;
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = string.Empty;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = result.Status;
                responseModel.StatusCode = result.StatusCode;
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = string.Empty;
                return NotFound(responseModel);
            }

        }
        [HttpGet]
        [Route(ConstantResource.GetAreadyExistsUser)]
        public IActionResult GetAreadyExistsUser(string userCode)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.GetAreadyExistsUserCode(userCode);
            if (result.UserCode != null)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.AlreadyExistsUser;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = ConstantResource.InvaidUser;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        [HttpGet]
        [Route(ConstantResource.GetUserRolesByDepartment)]
        public IActionResult GetAllRolesByDepartment(string userType, string department)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.GetAllRolesByDepartment(userType, department);
            if (result.Count > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.Success;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = ConstantResource.RolesNotFound;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        [HttpGet]
        [Route(ConstantResource.GetUserDetailsByUserCode)]
        public IActionResult GetUserDetailsByUserCodeAndOwnerId(string userCode, int ownerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.GetUserDetailsByUserCode(userCode, ownerId);
            if (result.Count > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.Success;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = ConstantResource.UserNotExists;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        [HttpGet]
        [Route(ConstantResource.SearchUser)]
        public IActionResult SearchUserDetails(string? userName, string userType, string accountStatus, int accountId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.SearchUsers(userName,userType,accountStatus,accountId);
            if (result.Count > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.Success;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = ConstantResource.UserNotExists;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }

        [HttpPost]
        [Route(ConstantResource.SearchAllUser)]
        public IActionResult SearchAllUser(UserRequestModel userRequest)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.SearchAllUsers(userRequest);
            if (result != null && result.Count > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = (int)HttpStatusCode.OK;
                responseModel.ResponseMessage = ConstantResource.Success;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = (int)HttpStatusCode.NotFound;
                responseModel.ResponseMessage = ConstantResource.Failed;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        [HttpGet]
        [Route(ConstantResource.GetAllDepartments)]
        public IActionResult GetAllDepartments()
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.GetAllDepartments();
            if (result != null && result.Count > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.Success;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = ConstantResource.Failed;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }

        [HttpPost]
        [Route(ConstantResource.AddUsers)]
        public IActionResult AddUsers(PartnerUserRequestModel partnerUser)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.AddPartnerUsers(partnerUser);
            if (result.Status && result.StatusCode == (int)HttpStatusCode.OK)
            {
                responseModel.Status = result.Status;
                responseModel.StatusCode = result.StatusCode;
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = string.Empty;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = result.Status;
                responseModel.StatusCode = result.StatusCode;
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = string.Empty;
                return NotFound(responseModel);
            }

        }

        [HttpDelete]
        [Route(ConstantResource.DeleteUserById)]
        public IActionResult DeleteUserById(string userId, string delById)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.UserDeletedById(userId,delById);
            if (result.Status && result.StatusCode == (int)HttpStatusCode.OK)
            {
                responseModel.Status = result.Status;
                responseModel.StatusCode = result.StatusCode;
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = string.Empty;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = result.Status;
                responseModel.StatusCode = result.StatusCode;
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = string.Empty;
                return NotFound(responseModel);
            }

        }
    }
}
