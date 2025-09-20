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
        Task<APIResponseModel<string>> DeleteProfile(string partnerId,string profileCode);
    }
}
