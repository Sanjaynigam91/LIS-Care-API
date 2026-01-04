using LISCareDTO;
using LISCareDTO.SampleAccession;
using LISCareDTO.SampleManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Interface
{
    public interface IAccessionRepository
    {
        /// <summary>
        /// used to get pending sample for accession
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="barcode"></param>
        /// <param name="centerCode"></param>
        /// <param name="patientName"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<PendingAccessionResponse>>> GetPendingSampleForAccession(DateTime startDate, DateTime endDate, string? barcode, string? centerCode,
            string? patientName, string partnerId);
       
        /// <summary>
        /// Used to get last imported
        /// </summary>
        /// <param name="woeDate"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<int>> GetLastImported(DateTime woeDate, string partnerId);
       
        /// <summary>
        /// used to get sample types by visitId
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<SampleTypeResponse>>> GetSampleTypesByVisitid(int visitId, string partnerId);

        /// <summary>
        /// used to get patient info by barcode
        /// </summary>
        /// <param name="sampleId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<PatientInfoResponse>> GetPatientInfoByBarcode(int visitId, string? sampleType, string partnerId);

        /// <summary>
        /// used to get test details by barcode
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="sampleType"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<SampleAccessionTestResponse>>> GetTestDetailsByBarcode(string barcode, string? sampleType, string partnerId);

    }
}
