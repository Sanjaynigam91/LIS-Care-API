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
        /// <summary>
        /// used to add or update patients
        /// </summary>
        /// <param name="patientRequests"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> AddUpdatePatients(PatientRequest patientRequest);
        /// <summary>
        /// used to add tests requested by patient
        /// </summary>
        /// <param name="patientRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> AddTestsRequested(PatientTestRequest testRequest);
        /// <summary>
        /// used to get patient requested test details 
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<TestSampleResponse>>> GetPatientsRequestedTestDetails(Guid patientId, string partnerId);
        /// <summary>
        /// used to get patient summary
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="patientName"></param>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <param name="centerCode"></param>
        /// <param name="status"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<PatientResponse>>> GetPatientSummary(string? barcode, DateTime? startDate, DateTime? endDate, string? patientName, string? patientCode,
            string? centerCode, string? status, string partnerId);
        /// <summary>
        /// used to get patient details
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<APIResponseModel<PatientDetailResponse>> GetPatientDetails(Guid? patientId);

    }
}
