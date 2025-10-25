using LISCareDTO;
using LISCareDTO.OutLab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Interface
{
    public interface IOutLab
    {
        /// <summary>
        /// used to get out labs details
        /// </summary>
        /// <param name="labStatus"></param>
        /// <param name="labname"></param>
        /// <param name="labCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<OutLabResponse>>> GetAllOutLabs(bool? labStatus, string? labname, string? labCode, string partnerId);
    }
}
