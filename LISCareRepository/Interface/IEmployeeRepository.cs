using LISCareDTO;
using LISCareDTO.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Interface
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Used to get all 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<MetaDataTagsResponseModel>>> GetEmployeeDepartments(string? category, string partnerId);
    }
}
