using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.CenterMaster;
using LISCareDTO.ClinicMaster;
using LISCareRepository.Implementation;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class ClinicController(IClinc clinic, ILogger<ClinicController> logger) : ControllerBase
    {
        private readonly IClinc _clinic= clinic;
        private readonly ILogger<ClinicController> _logger = logger;
        [HttpGet]
        [Route(ConstantResource.GetAllClinics)]
        public async Task<IActionResult> GetAllClinicDetails([FromQuery] string? partnerId, string? centerCode, string? clinicStatus, string? searchBy = "")
        {
            _logger.LogInformation($"GetAllClinics, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<ClinicResponse>>
            {
                Data = []
            };
            try
            {
                response = await _clinic.GetAllClinicDetails(partnerId, centerCode,clinicStatus, searchBy);
                if (response.Data == null || response.Data.Count == 0)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No centers details found for the given PartnerId.";
                }
                _logger.LogInformation($"GetAllClinics, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                _logger.LogInformation($"GetAllClinics, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route(ConstantResource.GetClinicById)]
        public async Task<IActionResult> GetAllClinicDetailsById([FromQuery] int clinicId, string partnerId)
        {
            _logger.LogInformation($"GetClinicById, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<ClinicResponse>>
            {
                Data = []
            };
            try
            {
                response = await _clinic.GetClinicDeatilsById(clinicId, partnerId);
                if (response.Data == null || response.Data.Count == 0)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No centers details found for the given PartnerId.";
                }
                _logger.LogInformation($"GetClinicById, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                _logger.LogInformation($"GetClinicById, API execution failed at:{DateTime.Now} with error message: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        [Route(ConstantResource.AddNewClinic)]
        public async Task<IActionResult> CreateNewClinic(ClinicRequest clinicRequest)
        {
            _logger.LogInformation($"AddNewClinic, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(clinicRequest.PartnerId) && !string.IsNullOrEmpty(clinicRequest.CenterCode))
            {
                var result = await _clinic.CreateNewClinic(clinicRequest);
                _logger.LogInformation($"AddNewClinic, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            _logger.LogInformation($"AddNewClinic, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid center request");
        }
        [HttpPut]
        [Route(ConstantResource.UpdateClinic)]
        public async Task<IActionResult> UpdateClinicDetails(ClinicRequest clinicRequest)
        {
            _logger.LogInformation($"UpdateClinic, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(clinicRequest.PartnerId) && !string.IsNullOrEmpty(clinicRequest.CenterCode))
            {
                var result = await _clinic.UpdateClinic(clinicRequest);
                _logger.LogInformation($"UpdateClinic, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            _logger.LogInformation($"UpdateClinic, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid center request");
        }
        [HttpDelete]
        [Route(ConstantResource.DeleteClinic)]
        public async Task<IActionResult> DeleteClinicDetails(int clinicId, string partnerId)
        {
            _logger.LogInformation($"DeleteClinic, API execution started at:{DateTime.Now}");
            if (clinicId>0 && !string.IsNullOrEmpty(partnerId))
            {
                var result = await _clinic.DeleteClinic(clinicId, partnerId);
                _logger.LogInformation($"DeleteClinic, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            _logger.LogInformation($"DeleteClinic, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid center request");
        }
    }
}
