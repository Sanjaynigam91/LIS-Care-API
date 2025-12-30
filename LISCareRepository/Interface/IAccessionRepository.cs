using LISCareDTO;
using LISCareDTO.SampleAccession;
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
        Task<APIResponseModel<List<PendingAccessionResponse>>>GetPendingSampleForAccession(DateTime startDate, DateTime endDate,string?barcode,string?centerCode,
            string?patientName,string partnerId);

    }
}
