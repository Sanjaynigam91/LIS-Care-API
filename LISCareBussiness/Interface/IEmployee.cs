using LISCareDTO;
using LISCareDTO.Employee;
using LISCareDTO.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Interface
{
    public interface IEmployee
    {
        /// <summary>
        /// Used to get all 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<MetaDataTagsResponseModel>>> GetEmployeeDepartments(string? category, string partnerId);
        /// <summary>
        /// used to get all employee details
        /// </summary>
        /// <param name="recordStatus"></param>
        /// <param name="department"></param>
        /// <param name="employeeName"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<EmployeeResponse>>> GetAllEmployees(string? empStatus, string? department, string? employeeName, string partnerId);
        /// <summary>
        /// used to get employee details by employee Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<EmployeeResponse>>> GetEmployeeById(string employeeId, string partnerId);
        /// <summary>
        /// used to add new employee details
        /// </summary>
        /// <param name="employeeRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> AddNewEmployee(EmployeeRequest employeeRequest);
        /// <summary>
        /// used to update employee details
        /// </summary>
        /// <param name="employeeRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> UpdateEmployee(EmployeeRequest employeeRequest);
        /// <summary>
        /// used to delete employee details
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> DeleteEmployee(string employeeId, string partnerId);
    }
}
