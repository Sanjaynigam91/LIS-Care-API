using LISCareDTO;
using LISCareDTO.ProfileMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Interface
{
    public interface IProfile
    { 
      /// <summary>
      /// used to get all profiles details
      /// </summary>
      /// <param name="partnerId"></param>
      /// <returns></returns>
        Task<APIResponseModel<List<ProfileResponse>>> GetAllProfileDetails(string partnerId);
        /// <summary>
        /// used to delete profile by profile code and partnerId
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="profileCode"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> DeleteProfile(string partnerId,string profileCode);
        /// <summary>
        /// used to get profiles by profile code
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="profileCode"></param>
        /// <returns></returns>
        Task<APIResponseModel<ProfileResponse>> GetProfilesByProfileCode(string partnerId, string profileCode);
    }
}
