using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.SampleAccession;
using LISCareDTO.SampleManagement;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class AccessionBAL : IAccession
    {
        private readonly IAccessionRepository accessionRepository;

        public AccessionBAL(IAccessionRepository accessionRepository)
        {
            this.accessionRepository = accessionRepository;
        }

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
        public async Task<APIResponseModel<List<PendingAccessionResponse>>> GetPendingSampleForAccession(DateTime startDate, DateTime endDate, string? barcode, string? centerCode, string? patientName, string partnerId)
        {
            APIResponseModel<List<PendingAccessionResponse>> response;
            try
            {
                response = await accessionRepository.GetPendingSampleForAccession(startDate, endDate, barcode, centerCode, patientName, partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
