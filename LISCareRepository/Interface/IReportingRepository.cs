using LISCareDTO;
using LISCareDTO.Reporting;


namespace LISCareRepository.Interface
{
    public interface IReportingRepository
    {
        /// <summary>
        /// used to retrieve pending patients based on various filters for test entry
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<PendingPatientResponse>>> RetrievePendingPatients(string partnerId, DateTime startDate, DateTime endDate, string? barcode,
           string? department, string? patientName, string? centerCode, string reportStatus);


    }
}
