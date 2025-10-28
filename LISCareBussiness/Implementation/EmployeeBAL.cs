using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.MetaData;
using LISCareRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class EmployeeBAL(IEmployeeRepository employeeRepository) : IEmployee
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
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
    }
}
