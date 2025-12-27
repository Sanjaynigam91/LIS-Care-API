using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.SampleCollectionPlace;
using LISCareDTO.SampleManagement;
using LISCareUtility;
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
                return BadRequest(responseModel);
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

        [HttpGet]
        [Route(ConstantResource.SearchPatientForCollection)]
        public async Task<IActionResult> GetPatientsForSampleCollection([FromQuery] DateTime startDate, DateTime endDate, string? patientCode, string? centerCode, string? patientName, string partnerId)
        {
            var response = new APIResponseModel<List<SampleCollectionResponse>>
            {
                Data = []
            };
            response = await _sample.GetPatientsForCollection(startDate, endDate, patientCode, centerCode, patientName, partnerId);

            if (patientName != null)
            {
                var result = response.Data.Where(x => x.PatientName == patientName);
                response.Data = result.ToList();
                return StatusCode(response.StatusCode, response);
            }

            if (response.Data.Count > 0)
            {
                return StatusCode(response.StatusCode, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

        }

    }
}
