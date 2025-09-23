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
        /// used to delete tests mapping by mapping Id and partnerId
        /// </summary>
        /// <param name="mappingId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<APIResponseModel<string>> DeleteMappingTests(string mappingId, string partnerId)
        {
            try
            {
                return await _profileRepository.DeleteMappingTests(mappingId, partnerId);
            }
            catch
            {
                throw;
            }
        }

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
        /// used to get all profile's mapped test details
        /// </summary>
        /// <param name="profileCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<APIResponseModel<List<ProfileMappingResponse>>> GetAllMappedTests(string profileCode, string partnerId)
        {
            try
            {
                return await _profileRepository.GetAllMappedTests(profileCode, partnerId);
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
        /// <summary>
        /// used to get profiles by profile code
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="profileCode"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<ProfileResponse>> GetProfilesByProfileCode(string partnerId, string profileCode)
        {
            try
            {
                return await _profileRepository.GetProfilesByProfileCode(partnerId, profileCode);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to save profile details
        /// </summary>
        /// <param name="profileRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> SaveProfileDetails(ProfileRequest profileRequest)
        {
            try
            {
                return await _profileRepository.SaveProfileDetails(profileRequest);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to update profile details
        /// </summary>
        /// <param name="profileRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateProfileDetails(ProfileRequest profileRequest)
        {
            try
            {
                return await _profileRepository.UpdateProfileDetails(profileRequest);
            }
            catch
            {
                throw;
            }
        }
    }
}
