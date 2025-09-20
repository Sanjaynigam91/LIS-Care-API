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
        /// <summary>
        /// used to get all profiles details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<ProfileResponse>>> GetAllProfileDetails(string partnerId);
        Task<APIResponseModel<string>> DeleteProfile(string partnerId, string profileCode);

    }
}
