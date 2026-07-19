using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareRepository.Interface;
using LISCareDTO.RejectedSample;


namespace LISCareBussiness.Implementation
{
    public class RejectionBAL : IRejection
    {
        private readonly IRejectionRepository rejectionRepository;

        public RejectionBAL(IRejectionRepository rejectionRepository)
        {
            this.rejectionRepository = rejectionRepository;
        }

        public async Task<APIResponseModel<string>> RejectTestsBeforeAccession(int patientSpecimenId, string testCode, string rejectionReason, string rejectedBy, string partnerId)
        {
            APIResponseModel<string> response;
            try
            {
                response = await rejectionRepository.RejectTestsBeforeAccession(patientSpecimenId, testCode, rejectionReason, rejectedBy, partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public async Task<APIResponseModel<List<RejectedSample>>> GetRejectedSamples(string partnerId, DateTime startDate, DateTime endDate, string barcode, string patientName, string clientCode)
        {
            APIResponseModel<List<RejectedSample>> response;
            try
            {
                response = await rejectionRepository.GetRejectedSamples(partnerId, startDate, endDate, barcode, patientName, clientCode);
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
