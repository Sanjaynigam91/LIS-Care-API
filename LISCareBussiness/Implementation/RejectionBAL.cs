using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
