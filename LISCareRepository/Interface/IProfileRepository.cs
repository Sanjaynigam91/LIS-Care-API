using LISCareDTO;
using LISCareDTO.ProfileMaster;
using LISCareDTO.TestMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Interface
{
    public interface IProfileRepository
    {
        Task<APIResponseModel<List<ProfileResponse>>> GetAllProfileDetails(string partnerId);

    }
}
