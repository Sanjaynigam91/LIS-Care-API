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
        Task<APIResponseModel<List<ProfileResponse>>> GetAllProfileDetails(string partnerId);
    }
}
