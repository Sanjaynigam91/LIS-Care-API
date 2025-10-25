using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.OutLab;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class OutLabBAL(IOutLabRepository outLabRepository):IOutLab
    {
        private readonly IOutLabRepository _outLabRepository= outLabRepository;

        /// <summary>
        /// used to get out labs
        /// </summary>
        /// <param name="labStatus"></param>
        /// <param name="labname"></param>
        /// <param name="labCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<OutLabResponse>>> GetAllOutLabs(bool? labStatus, string? labname, string? labCode, string partnerId)
        {
            try
            {
                return await _outLabRepository.GetAllOutLabs(labStatus, labname, labCode, partnerId);
            }
            catch
            {
                throw;
            }
        }
    }
}
