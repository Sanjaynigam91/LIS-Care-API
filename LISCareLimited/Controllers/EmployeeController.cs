using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.ClientMaster;
using LISCareDTO.MetaData;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployee employee, ILogger<EmployeeController> logger)
        {
            _employee = employee;
            _logger = logger;
        }

        [HttpGet]
        [Route(ConstantResource.GetEmployeeDepartment)]
        public async Task<IActionResult> GetEmployeeDepartments([FromQuery] string? category, string partnerId)
        {
            _logger.LogInformation($"GetEmployeeDepartment, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<MetaDataTagsResponseModel>>
            {
                Data = []
            };
            try
            {
                response = await _employee.GetEmployeeDepartments(category,partnerId);
                if (response.Data == null || response.Data.Count == 0)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No departments found!";
                }
                _logger.LogInformation($"GetEmployeeDepartment, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                _logger.LogInformation($"GetEmployeeDepartment, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
