using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.GlobalRoleAccess;
using LISCareDTO.LISRoles;
using LISCareUtility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class GlobalRoleAccessController : ControllerBase
    {
        private IConfiguration _configuration;
        private IGlobalRoleAccess _globalRoleAccess;
        public GlobalRoleAccessController(IConfiguration configuration, IGlobalRoleAccess globalRoleAccess) 
        {
            _configuration= configuration;
            _globalRoleAccess= globalRoleAccess;
        }
        /// <summary>
        /// This API is used to get all Role type
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetAllRoleType)]
        public IActionResult GetLISRoleType()
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.GetAllRoleType();
            if (result.Count > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = (int)(HttpStatusCode.OK); ;
                responseModel.ResponseMessage = ConstantResource.Success;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = (int)(HttpStatusCode.NotFound);
                responseModel.ResponseMessage = ConstantResource.NoMetaDataFound;
                responseModel.Data = result;
                return NotFound(responseModel);
               
            }

        }
        /// <summary>
        /// This API is used to get roles by role type
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetRoleByRoleType)]
        public IActionResult GetRoleByRoleType(string roleType)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.GetRolesByRoleType(roleType);
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
                responseModel.ResponseMessage = ConstantResource.RoleNotExists;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        /// <summary>
        /// This API is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetPageHeadersByRoleId)]
        public IActionResult GetAllPageHeadersByRoleId(int roleId, string partnerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.GetAllPageHeaders(roleId,partnerId);
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
                responseModel.ResponseMessage = ConstantResource.RoleNotExists;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        /// <summary>
        /// This API is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpPut]
        [Route(ConstantResource.PageAccessPermission)]
        public IActionResult PageAccessPermission(RolePermissionRequestModel rolePermission)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.UpdatePageAccess(rolePermission);
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
                responseModel.ResponseMessage = ConstantResource.RoleNotExists;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }

        /// <summary>
        /// This API is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpPost]
        [Route(ConstantResource.SaveAccessPermission)]
        public IActionResult SaveAccessPermission(RolePermissionRequestModel rolePermission)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.SaveAllPageAccess(rolePermission);
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
                responseModel.ResponseMessage = ConstantResource.RoleNotExists;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }

        /// <summary>
        /// This API is used to get all Role type
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetLabRoles)]
        public IActionResult GetLabRoles()
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.GetAllLabRole();
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
        /// This API is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpPost]
        [Route(ConstantResource.SaveLisPageDetails)]
        public IActionResult SaveLisPageDetails(LisPageRequestModel lisPageRequest)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.SaveAllLisPageAccess(lisPageRequest);
            if (result.Status && result.StatusCode == (int)HttpStatusCode.OK)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.PageSuccess;
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

        /// <summary>
        /// This API is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetAllLisPages)]
        public IActionResult GetAllLisPagesByPartnerId(string partnerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.GetAllLisPages(partnerId);
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
                responseModel.ResponseMessage = ConstantResource.RoleNotExists;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }

        /// <summary>
        /// This API is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetPageEntity)]
        public IActionResult GetAllLisPagesEntityByPartnerId(string partnerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.GetAllPageEntity(partnerId);
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
                responseModel.ResponseMessage = ConstantResource.RoleNotExists;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }

        /// <summary>
        /// This API is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetAllPageByEntity)]
        public IActionResult GetAllLisPagesEntityByPartnerId(string partnerId, string pageEntity)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.GetAllPage(partnerId, pageEntity);
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
                responseModel.ResponseMessage = ConstantResource.RoleNotExists;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }

        /// <summary>
        /// This API is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetAllCriteria)]
        public IActionResult GetAllCriteria()
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.GetAllCriteria();
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
                responseModel.ResponseMessage = ConstantResource.RoleNotExists;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        /// <summary>
        /// This API is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetPageDetailsById)]
        public IActionResult GetPageDetailsById(string pageId, string partnerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.GetPageDetailsById(pageId, partnerId);
            if (!string.IsNullOrEmpty(result.NavigationId))
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

        /// <summary>
        /// This API is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpPut]
        [Route(ConstantResource.UpdateLisPage)]
        public IActionResult UpdatePageDetails(LisPageUpdateRequestModel lisPageUpdate)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.UpdatePageDetails(lisPageUpdate);
            if (result.Status && result.StatusCode == (int)HttpStatusCode.OK)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.PageSuccess;
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

        /// <summary>
        /// This API is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpDelete]
        [Route(ConstantResource.DeleteLisPage)]
        public IActionResult DeletePageDetails(string pageId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.DeleteLisPageById(pageId);
            if (result.Status && result.StatusCode == (int)HttpStatusCode.OK)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.PageSuccess;
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

        /// <summary>
        /// This API is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpPost]
        [Route(ConstantResource.SeachLisPages)]
        public IActionResult SearchLisPages(LisPageSearchRequestModel lisPageSearch)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _globalRoleAccess.SearchLisPages(lisPageSearch);
            if (result.Count > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.Success;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else if (result.Count == 0)
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
    }
}
