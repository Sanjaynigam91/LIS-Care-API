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
    public class PatientBAL : IPatient
    {
        private readonly IPatientRepository patientRepository;
        public PatientBAL(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public async Task<APIResponseModel<string>> AddTestsRequested(PatientTestRequest testRequest)
        {
            try
            {
                return await patientRepository.AddTestsRequested(testRequest);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to add or update patients
        /// </summary>
        /// <param name="patientRequests"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> AddUpdatePatients(PatientRequest patientRequest)
        {
            try
            {
                return await patientRepository.AddUpdatePatients(patientRequest);
            }
            catch
            {
                throw;
            }
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
                return await patientRepository.GetAllSamples(partnerId, centerCode, projectCode, testCode, testApplicable);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to get patient requested test details 
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<TestSampleResponse>>> GetPatientsRequestedTestDetails(Guid patientId, string partnerId)
        {
            try
            {
                return await patientRepository.GetPatientsRequestedTestDetails(patientId, partnerId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<APIResponseModel<List<PatientResponse>>> GetPatientSummary(string? barcode, DateTime? startDate, DateTime? endDate, string? patientName, string? patientCode, string? centerCode, string? status, string partnerId)
        {
            try
            {
                return await patientRepository.GetPatientSummary(barcode, startDate, endDate, patientName, patientCode, centerCode, status, partnerId);
            }
            catch
            {
                throw;
            }
        }
    }
}
