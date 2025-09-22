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
        /// <summary>
        /// Used to delete profile by profile code and partnerId
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="profileCode"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> DeleteProfile(string partnerId, string profileCode);
        /// <summary>
        /// used to get profiles by profile code
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="profileCode"></param>
        /// <returns></returns>
        Task<APIResponseModel<ProfileResponse>>GetProfilesByProfileCode(string partnerId, string profileCode);
        /// <summary>
        /// used to get all profile's mapped test details
        /// </summary>
        /// <param name="profileCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<ProfileMappingResponse>>> GetAllMappedTests(string profileCode, string partnerId);
        /// <summary>
        /// Used to save profile details
        /// </summary>
        /// <param name="profileRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> SaveProfileDetails(ProfileRequest profileRequest);
        /// <summary>
        /// used to update profile details
        /// </summary>
        /// <param name="profileRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> UpdateProfileDetails(ProfileRequest profileRequest);
    }
}
