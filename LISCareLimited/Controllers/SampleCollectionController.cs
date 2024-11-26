using LISCare.Interface;
using LISCareBussiness.Interface;
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

    }
}
