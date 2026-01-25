using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.Reporting;
using LISCareRepository.Interface;

namespace LISCareBussiness.Implementation
{
    public class ReportingBAL : IReporting
    {
        private readonly IReportingRepository reportingRepository;

        public ReportingBAL(IReportingRepository reportingRepository)
        {
            this.reportingRepository = reportingRepository;
        }

        /// <summary>
        /// used to retrieve pending patients based on various filters for test entry
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<PendingPatientResponse>>> RetrievePendingPatients(string partnerId, DateTime startDate, DateTime endDate, string? barcode,
           string? department, string? patientName, string? centerCode, string reportStatus)
        {
            APIResponseModel<List<PendingPatientResponse>> response;
            try
            {
                response = await reportingRepository.RetrievePendingPatients(partnerId, startDate, endDate, barcode, department, patientName, centerCode, reportStatus);
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
