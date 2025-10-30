using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.ClientMaster;
using LISCareDTO.Employee;
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

        [HttpPost]
        [Route(ConstantResource.AddNewEmployee)]
        public async Task<IActionResult> AddNewEmployeeDetail(EmployeeRequest employeeRequest)
        {
            _logger.LogInformation($"AddNewEmployee, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(employeeRequest.PartnerId) && !string.IsNullOrEmpty(employeeRequest.EmployeeName))
            {
                var result = await _employee.AddNewEmployee(employeeRequest);
                _logger.LogInformation($"AddNewEmployee, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            _logger.LogInformation($"AddNewEmployee, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid employee request");
        }

        /// <summary>
        /// used to get all employee departments 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// used to get all employees details
        /// </summary>
        /// <param name="empStatus"></param>
        /// <param name="department"></param>
        /// <param name="employeeName"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ConstantResource.GetAllEmployees)]
        public async Task<IActionResult> GetAllEmployeeDetails([FromQuery] string? empStatus, string? department, string? employeeName, string partnerId)
        {
            _logger.LogInformation($"GetAllEmployees, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<EmployeeResponse>>
            {
                Data = []
            };
            try
            {
                response = await _employee.GetAllEmployees(empStatus,department,employeeName, partnerId);
                _logger.LogInformation($"GetAllEmployees, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                _logger.LogInformation($"GetAllEmployees, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// used to get employee by employee Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ConstantResource.GetEmployeeById)]
        public async Task<IActionResult> GetEmployeeDetailsById([FromQuery] string employeeId, string partnerId)
        {
            _logger.LogInformation($"GetEmployeeById, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<EmployeeResponse>>
            {
                Data = []
            };
            try
            {
                response = await _employee.GetEmployeeById(employeeId, partnerId);
                _logger.LogInformation($"GetEmployeeById, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                _logger.LogInformation($"GetEmployeeById, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// used to update employee details
        /// </summary>
        /// <param name="employeeRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(ConstantResource.UpdateEmployee)]
        public async Task<IActionResult> UpdateEmployeeDetail(EmployeeRequest employeeRequest)
        {
            _logger.LogInformation($"UpdateEmployee, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(employeeRequest.PartnerId) && !string.IsNullOrEmpty(employeeRequest.EmployeeName))
            {
                var result = await _employee.UpdateEmployee(employeeRequest);
                _logger.LogInformation($"UpdateEmployee, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            _logger.LogInformation($"UpdateEmployee, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid employee request");
        }

        [HttpDelete]
        [Route(ConstantResource.DeleteEmployee)]
        public async Task<IActionResult> DeleteEmployeeDetail([FromQuery] string employeeId, string partnerId)
        {
            _logger.LogInformation($"DeleteEmployee, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(employeeId) && !string.IsNullOrEmpty(partnerId))
            {
                var result = await _employee.DeleteEmployee(employeeId,partnerId);
                _logger.LogInformation($"DeleteEmployee, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            _logger.LogInformation($"DeleteEmployee, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid employee request");
        }
    }
}
