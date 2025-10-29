using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.Employee;
using LISCareDTO.MetaData;
using LISCareRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class EmployeeBAL(IEmployeeRepository employeeRepository) : IEmployee
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        /// <summary>
        /// used to add new employee
        /// </summary>
        /// <param name="employeeRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> AddNewEmployee(EmployeeRequest employeeRequest)
        {
            try
            {
                return await _employeeRepository.AddNewEmployee(employeeRequest);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to delete employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> DeleteEmployee(string employeeId, string partnerId)
        {
            try
            {
                return await _employeeRepository.DeleteEmployee(employeeId, partnerId);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to get all employee details
        /// </summary>
        /// <param name="empStatus"></param>
        /// <param name="department"></param>
        /// <param name="employeeName"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<EmployeeResponse>>> GetAllEmployees(string? empStatus, string? department, string? employeeName, string partnerId)
        {
            try
            {
                return await _employeeRepository.GetAllEmployees(empStatus, department, employeeName, partnerId);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to get employee by employeeId
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<EmployeeResponse>>> GetEmployeeById(string employeeId, string partnerId)
        {
            try
            {
                return await _employeeRepository.GetEmployeeById(employeeId, partnerId);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to get all employee departments
        /// </summary>
        /// <param name="category"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<MetaDataTagsResponseModel>>> GetEmployeeDepartments(string? category, string partnerId)
        {
            try
            {
                return await _employeeRepository.GetEmployeeDepartments(category, partnerId);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to update employee details
        /// </summary>
        /// <param name="employeeRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateEmployee(EmployeeRequest employeeRequest)
        {
            try
            {
                return await _employeeRepository.UpdateEmployee(employeeRequest);
            }
            catch
            {
                throw;
            }
        }
    }
}
