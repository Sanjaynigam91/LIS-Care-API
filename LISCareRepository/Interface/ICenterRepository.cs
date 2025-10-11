using LISCareDTO;
using LISCareDTO.AnalyzerMaster;
using LISCareDTO.CenterMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Interface
{
    public interface ICenterRepository
    {
        /// <summary>
        /// used to get all center details
        /// </summary>
        /// <param name="centerStatus"></param>
        /// <param name="searchBy"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<CenterResponse>>> GetAllCenterDetails(string? partnerId,string? centerStatus = "",string? searchBy = "");

    }
}
