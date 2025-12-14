using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.ClientMaster;
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

        [HttpPost]
        [Route(ConstantResource.PatientRegistration)]
        public async Task<IActionResult> PatientRegistration(PatientRequest patientRequest)
        {
            logger.LogInformation($"PatientRegistration, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(patientRequest.PartnerId))
            {
                var result = await patient.AddUpdatePatients(patientRequest);
                logger.LogInformation($"PatientRegistration, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            logger.LogInformation($"PatientRegistration, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid patient request");
        }

        [HttpPost]
        [Route(ConstantResource.AddTestRequested)]
        public async Task<IActionResult> AddTestRequested(PatientTestRequest testRequest)
        {
            logger.LogInformation($"AddTestRequested, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(testRequest.PartnerId) || testRequest.PatientId != Guid.Empty)
            {
                var result = await patient.AddTestsRequested(testRequest);
                logger.LogInformation($"AddTestRequested, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            logger.LogInformation($"AddTestRequested, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid test request");
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

        [HttpGet]
        [Route(ConstantResource.GetSelectedSamples)]
        public async Task<IActionResult> GetPatientsRequestedTestDetails([FromQuery] Guid patientId, string partnerId)
        {
            logger.LogInformation($"GetselectedSamples, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<TestSampleResponse>>
            {
                Data = []
            };
            try
            {
                response = await patient.GetPatientsRequestedTestDetails(patientId, partnerId);
                logger.LogInformation($"GetselectedSamples, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                logger.LogInformation($"GetselectedSamples, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route(ConstantResource.GetPatientSummary)]
        public async Task<IActionResult> GetPatientRegistrationSummary([FromQuery] string? barcode, DateTime? startDate, DateTime? endDate, string? patientName,string? patientCode, string? centerCode, string? status, string partnerId)
        {
            logger.LogInformation($"GetPatientSummary, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<PatientResponse>>
            {
                Data = []
            };
            try
            {
                response = await patient.GetPatientSummary(barcode, startDate, endDate, patientName, patientCode, centerCode, status, partnerId);
                logger.LogInformation($"GetPatientSummary, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                logger.LogInformation($"GetPatientSummary, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


    }
}
