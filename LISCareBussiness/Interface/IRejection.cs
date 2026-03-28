using LISCareDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Interface
{
    public interface IRejection
    {
        Task<APIResponseModel<string>> RejectTestsBeforeAccession(int patientSpecimenId, string testCode, string rejectionReason, string rejectedBy, string partnerId);
    }
}
