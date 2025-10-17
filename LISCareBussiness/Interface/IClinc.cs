using LISCareDTO;
using LISCareDTO.ClinicMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Interface
{
    public interface IClinc
    {
        /// <summary>
        /// used to get all clinic details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="centerCode"></param>
        /// <param name="clinicStatus"></param>
        /// <param name="searchBy"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<ClinicResponse>>> GetAllClinicDetails(string? partnerId, string? centerCode, string? clinicStatus, string? searchBy = "");
    }
}
