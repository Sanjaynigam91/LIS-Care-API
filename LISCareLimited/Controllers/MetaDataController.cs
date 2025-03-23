using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.MetaData;
using LISCareUtility;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class MetaDataController : ControllerBase
    {
        private IConfiguration _configuration;
        private IMetaData _metaData;
        public MetaDataController(IConfiguration configuration, IMetaData metaData)
        {
            _configuration = configuration;
            _metaData = metaData;
        }
        /// <summary>
        /// This API is used to Create New Master List  
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpPost]
        [Route(ConstantResource.CreateNewMasterList)]
        public IActionResult CreateNewMasterList(MasterListRequestModel masterList)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _metaData.CreateNewMasterList(masterList);
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
        /// <summary>
        /// This API is used to Create New Meta Data Tag
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpPost]
        [Route(ConstantResource.CreateNewMetaDataTag)]
        public IActionResult CreateNewMetaDataTag(MetaDataTagRequestModel metaDataTag)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _metaData.CreateNewMetaDataTag(metaDataTag);
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
        /// <summary>
        /// This API is used to Update Meta Data Tag
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpPut]
        [Route(ConstantResource.UpdateMetaDataTag)]
        public IActionResult UpdateMetaDataTag(MetaTagModel metaDataTag)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _metaData.UpdateTagInfo(metaDataTag);
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
        /// <summary>
        /// This API is used to Get All Tags
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetAllTag)]
        public IActionResult GetAllMetaDataByPartnerId(string partnerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _metaData.GetAllMetaData(partnerId);
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
        /// This API is used to Get All Master List By Category
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetAllMasterListByCategory)]
        public IActionResult GetMasterListByCategory(string category, string partnerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _metaData.GetMetaDataTagsByCategory(category, partnerId);
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
                responseModel.ResponseMessage = ConstantResource.NoMetaDataTagsFound;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        /// <summary>
        /// This API is used to Update Master List
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpPut]
        [Route(ConstantResource.UpdateMasterList)]
        public IActionResult UpdateMasterList(MasterListRequestModel masterList)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _metaData.UpdateMasterList(masterList);
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
        /// <summary>
        /// This API is used to Delete Master List
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpDelete]
        [Route(ConstantResource.DeleteMasterList)]
        public IActionResult DeleteMasterListByRecordId(int recordId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _metaData.DeleteMasterList(recordId);
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

        /// <summary>
        /// This API is used to Get All Master List By Category
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetMetaTagById)]
        public IActionResult GetMetaTagById(int tagId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _metaData.GetMetaDataTag(tagId);
            if (result.TagId > 0)
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
                responseModel.ResponseMessage = ConstantResource.NoMetaDataTagsFound;
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
        /// <summary>
        /// This API is used to Get Report Templates
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetReportTemplates)]
        public IActionResult GetReportTemplates(string partnerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _metaData.GetReportTemplates(partnerId);
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
        /// This API is used to Get Report Templates
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetReportingStyle)]
        public IActionResult GetReportingStyle(string partnerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _metaData.GetReportingStyle(partnerId);
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
        /// This API is used to Get Report Templates
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetSpecimenType)]
        public IActionResult GetSpecimenType(string partnerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _metaData.GetSpecimenType(partnerId);
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
        /// This API is used to Get Report Templates
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetSubTestDepartments)]
        public IActionResult GetSubTestDepartments(string partnerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _metaData.GetSubTestDepartments(partnerId);
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
        /// This API is used to Get Report Templates
        /// </summary>
        /// <returns>List<IActionResult></returns>
        [HttpGet]
        [Route(ConstantResource.GetDepartments)]
        public IActionResult GetTestDepartments(string partnerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _metaData.GetTestDepartments(partnerId);
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
    }
}
