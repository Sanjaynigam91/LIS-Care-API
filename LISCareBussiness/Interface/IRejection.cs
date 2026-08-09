using LISCareDTO;
using LISCareDTO.RejectedSample;
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
        /// <summary>
        /// used to get rejected samples based on the given parameters
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="barcode"></param>
        /// <param name="patientCode"></param>
        /// <param name="clientCode"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<RejectedSample>>> GetRejectedSamples(string partnerId, DateTime startDate, DateTime endDate, string barcode, string patientNameOrCode, string centerCode);
    }
}
