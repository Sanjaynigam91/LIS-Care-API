using LISCareDTO;
using LISCareDTO.FrontDesk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Interface
{
    public interface IPatientRepository
    {
        /// <summary>
        /// used to get all samples details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="centerCode"></param>
        /// <param name="projectCode"></param>
        /// <param name="testCode"></param>
        /// <param name="TestApplicable"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<TestSampleResponse>>> GetAllSamples(string partnerId, string? centerCode, int projectCode = 0, string? testCode = null, string? testApplicable = null);

    }
}
