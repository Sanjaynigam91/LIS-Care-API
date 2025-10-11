using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.CenterMaster;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class CenterBAL(ICenterRepository centerRepository) : ICenter
    {
        private readonly ICenterRepository _centerRepository = centerRepository;    
        public async Task<APIResponseModel<List<CenterResponse>>> GetAllCenterDetails(string? partnerId, string? centerStatus = "", string? searchBy = "")
        {
            try
            {
                return await _centerRepository.GetAllCenterDetails(partnerId, centerStatus, searchBy);
            }
            catch
            {
                throw;
            }
        }
    }
}
