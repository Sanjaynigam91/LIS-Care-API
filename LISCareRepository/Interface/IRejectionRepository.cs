using LISCareDTO;
using LISCareDTO.AnalyzerMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Interface
{
    public interface IRejectionRepository
    {
        Task<APIResponseModel<string>> RejectTestsBeforeAccession(int patientSpecimenId,string testCode,string rejectionReason,string rejectedBy,string partnerId);
    }
}
