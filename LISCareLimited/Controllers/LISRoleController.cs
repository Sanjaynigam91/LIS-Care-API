using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.LISRoles;
using LISCareUtility;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class LISRoleController : ControllerBase
    {
        private IConfiguration _configuration;
        private ILISRole _lISRole;
        public LISRoleController(IConfiguration configuration, ILISRole lISRole)
        {
            _configuration = configuration;
            _lISRole = lISRole;
        }
        /// <summary>
        /// This API is used to add new LIS Role
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpPost]
        [Route(ConstantResource.AddLISRole)]
        public IActionResult AddNewLISRole(LISRoleRequestModel lISRoleRequest)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _lISRole.AddNewLISRole(lISRoleRequest);
            if (result.Status && result.StatusCode == (int)HttpStatusCode.OK)
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
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        /// <summary>
        /// This API is used to update LIS Role
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpPut]
        [Route(ConstantResource.UpdateLISRole)]
        public IActionResult UpdateLISRole(LISRoleUpdateRequestModel lISRoleUpdate)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _lISRole.UpdateLISRole(lISRoleUpdate);
            if (result.Status && result.StatusCode == (int)HttpStatusCode.OK)
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
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        /// <summary>
        /// This API is used to get All LIS Roles
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetAllLISRoles)]
        public IActionResult GetAllLISRoles()
       {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _lISRole.GetAllLISRoles();
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
                responseModel.ResponseMessage = ConstantResource.NoMetaDataFound;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        /// <summary>
        /// This API is used to get LIS Role type
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetLISRoleType)]
        public IActionResult GetLISRoleType()
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _lISRole.GetLISRoleType();
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
                responseModel.ResponseMessage = ConstantResource.NoMetaDataFound;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        /// <summary>
        /// This API is used to get LIS role By Role Id
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetLISRoleByRoleId)]
        public IActionResult GetLISRoleByRoleId(int roleId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _lISRole.GetLISRoleByRoleId(roleId);
            if (result.RoleId > 0)
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
                responseModel.ResponseMessage = ConstantResource.RoleNotExists;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }

        [HttpDelete]
        [Route(ConstantResource.DeleteRoleById)]
        public IActionResult DeleteRoleByRoleId(int roleId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _lISRole.RoleDeletedById(roleId);
            if (result.Status && result.StatusCode == (int)HttpStatusCode.OK)
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
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
    }
}
