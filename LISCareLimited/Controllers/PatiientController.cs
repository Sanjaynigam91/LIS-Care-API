using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.FrontDesk;
using LISCareDTO.Projects;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class PatiientController : ControllerBase
    {
        private readonly IPatient patient;
        private readonly ILogger<PatiientController> logger;
        public PatiientController(IPatient patient, ILogger<PatiientController> logger) 
        {
            this.patient = patient;
            this.logger = logger;
        }

        [HttpGet]
        [Route(ConstantResource.GetAllTestSamples)]
        public async Task<IActionResult> GetAllTestSampleDetails([FromQuery] string partnerId, string? centerCode, int projectCode = 0, string? testCode = null, string? testApplicable = null)
        {
            logger.LogInformation($"GetAllTestSamples, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<TestSampleResponse>>
            {
                Data = []
            };
            try
            {
                response = await patient.GetAllSamples(partnerId, centerCode, projectCode, testCode, testApplicable);
                logger.LogInformation($"GetAllTestSamples, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                logger.LogInformation($"GetAllTestSamples, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }



    }
}
