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
                    response.ResponseMessage = "No client details found for the given PartnerId.";
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

    }
}
