using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.ProfileMaster;
using LISCareRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class ProfileBAL(IConfiguration configuration, IProfileRepository profileRepository):IProfile
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IProfileRepository _profileRepository = profileRepository;
        /// <summary>
        /// Delete profile by profile code and partnerId
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="profileCode"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<APIResponseModel<string>> DeleteProfile(string partnerId, string profileCode)
        {
            try
            {
                return await _profileRepository.DeleteProfile(partnerId,profileCode);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to get all profiles details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<ProfileResponse>>> GetAllProfileDetails(string partnerId)
        {
            try
            {
                return await _profileRepository.GetAllProfileDetails(partnerId);
            }
            catch
            {
                throw;
            }
        }
    }
}
