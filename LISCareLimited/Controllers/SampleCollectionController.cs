using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.SampleCollectionPlace;
using LISCareUtility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LISCare.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]

    public class SampleCollectionController : ControllerBase
    {
        private IConfiguration Configuration;
        private new ISampleCollection _sample;

        public SampleCollectionController(IConfiguration configuration, ISampleCollection sample)
        {
            Configuration = configuration;
            _sample = sample;
        }
        [HttpGet]
        [Route(ConstantResource.GetSampleCollectedPlaces)]
        public IActionResult GetSampleCollectedPlaces(string partnerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _sample.GetSampleCollectedPlace(partnerId);
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
                responseModel.ResponseMessage = "No Record found!";
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }

        [HttpPost]
        [Route(ConstantResource.AddSampleCollectedPlace)]
        public IActionResult AddSampleCollectedPlace(SampleCollectedRequest sampleCollected)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _sample.AddSampleCollectedPlaces(sampleCollected);
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
        [Route(ConstantResource.DeleteSamplePlace)]
        public IActionResult RemoveSamplePlace(int recordId, string partnerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _sample.RemoveSamplePlace(recordId, partnerId);
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
