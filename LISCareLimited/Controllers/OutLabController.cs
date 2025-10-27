using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.ClientMaster;
using LISCareDTO.OutLab;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class OutLabController(IOutLab outLab, ILogger<OutLabController> logger) : ControllerBase
    {
        private readonly IOutLab _outLab = outLab;
        private readonly ILogger<OutLabController> _logger = logger;

        /// <summary>
        /// used to get all out labs 
        /// </summary>
        /// <param name="labStatus"></param>
        /// <param name="labname"></param>
        /// <param name="labCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ConstantResource.GetAllOutLabs)]
        public async Task<IActionResult> GetAllOutLabDetails([FromQuery] bool? labStatus, string? labname, string? labCode, string partnerId)
        {
            _logger.LogInformation($"GetAllOutLabs, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<OutLabResponse>>
            {
                Data = []
            };
            try
            {
                response = await _outLab.GetAllOutLabs(labStatus, labname, labCode, partnerId);
                if (response.Data == null || response.Data.Count == 0)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No out lab details found for the given PartnerId.";
                }
                _logger.LogInformation($"GetAllOutLabs, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                _logger.LogInformation($"GetAllOutLabs, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
       
        /// <summary>
        /// used to get out lab by lab code
        /// </summary>
        /// <param name="labCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ConstantResource.GetOutLabByLabCode)]
        public async Task<IActionResult> GetOutLabDetailsByLabCode([FromQuery] string labCode, string partnerId)
        {
            _logger.LogInformation($"GetOutLabByLabCode, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<OutLabResponse>>
            {
                Data = []
            };
            try
            {
                response = await _outLab.GetOutLabByLabCode(labCode, partnerId);
                if (response.Data == null || response.Data.Count == 0)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No out lab found for the given lab code.";
                }
                _logger.LogInformation($"GetOutLabByLabCode, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                _logger.LogInformation($"GetOutLabByLabCode, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete]
        [Route(ConstantResource.DeleteOutLab)]
        public async Task<IActionResult> DeleteOutLabDetails([FromQuery] string labCode, string partnerId)
        {
            _logger.LogInformation($"DeleteOutLab, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(labCode) && !string.IsNullOrEmpty(partnerId))
            {
                var result = await _outLab.DeleteOutLabDetails(labCode, partnerId);
                _logger.LogInformation($"DeleteOutLab, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            _logger.LogInformation($"DeleteOutLab, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid lab code");
        }

        [HttpPost]
        [Route(ConstantResource.AddOutLab)]
        public async Task<IActionResult> AddNewOutLabDetails(OutLabRequest outLabRequest)
        {
            _logger.LogInformation($"AddOutLab, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(outLabRequest.LabName) && !string.IsNullOrEmpty(outLabRequest.PartnerId))
            {
                var result = await _outLab.AddNewOutLab(outLabRequest);
                _logger.LogInformation($"AddOutLab, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            _logger.LogInformation($"AddOutLab, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid out lab request");
        }

    }
}
