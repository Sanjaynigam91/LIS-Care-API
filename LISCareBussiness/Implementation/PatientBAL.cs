using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.FrontDesk;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class PatientBAL:IPatient
    {
        private readonly IPatientRepository patientRepository;
        public PatientBAL(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        /// <summary>
        /// used to get all samples 
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="centerCode"></param>
        /// <param name="projectCode"></param>
        /// <param name="testCode"></param>
        /// <param name="testApplicable"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<TestSampleResponse>>> GetAllSamples(string partnerId, string? centerCode, int projectCode = 0, string? testCode = null, string? testApplicable = null)
        {
            try
            {
                return await patientRepository.GetAllSamples(partnerId, centerCode, projectCode,testCode,testApplicable);
            }
            catch
            {
                throw;
            }
        }
    }
}
