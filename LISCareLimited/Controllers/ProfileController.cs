using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.ProfileMaster;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class ProfileController(IConfiguration configuration, IProfile profile) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IProfile _profile = profile;

        /// <summary>
        /// used to get all profiles details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ConstantResource.GetAllProfileDetails)]
        public async Task<IActionResult> GetAllProfileDetails([FromQuery] string partnerId)
        {
            var response = new APIResponseModel<List<ProfileResponse>>
            {
                Data = new List<ProfileResponse>()
            };
            try
            {
                
                response = await _profile.GetAllProfileDetails(partnerId);
                if (response.Data == null || response.Data.Count == 0)
                { 
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No profiles found for the given PartnerId.";                  
                }
                return StatusCode(response.StatusCode,response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete]
        [Route(ConstantResource.DeleteProfileByProfileCode)]
        public async Task<IActionResult> DeleteProfileByProfileCode(string partnerId, string profileCode)
        {
            if (!string.IsNullOrEmpty(partnerId)&& !string.IsNullOrEmpty(profileCode))
            {
                var result = await _profile.DeleteProfile(partnerId,profileCode);
                return StatusCode(result.StatusCode, result);
            }

            return BadRequest(ConstantResource.ProfileCodeEmpty);
        }
        [HttpGet]
        [Route(ConstantResource.GetProfileByProfileCode)]
        public async Task<IActionResult> GetProfileByProfileCode(string partnerId, string profileCode)
        {
            if (!string.IsNullOrEmpty(partnerId) && !string.IsNullOrEmpty(profileCode))
            {
                var result = await _profile.GetProfilesByProfileCode(partnerId, profileCode);
                return StatusCode(result.StatusCode, result);
            }

            return NotFound(ConstantResource.ProfileCodeEmpty);
        }
        [HttpGet]
        [Route(ConstantResource.GetAllMappedTests)]
        public async Task<IActionResult> GetAllProfilesMappedTests(string profileCode, string partnerId)
        {
            if (!string.IsNullOrEmpty(partnerId) && !string.IsNullOrEmpty(profileCode))
            {
                var result = await _profile.GetAllMappedTests(profileCode, partnerId);
                return StatusCode(result.StatusCode, result);
            }

            return NotFound(ConstantResource.ProfileCodeEmpty);
        }
        [HttpPost]
        [Route(ConstantResource.CreateProfile)]
        public async Task<IActionResult> SaveProfileDetails(ProfileRequest profileRequest)
        {
            if (!string.IsNullOrEmpty(profileRequest.PartnerId) && !string.IsNullOrEmpty(profileRequest.ProfileName))
            {
                var result = await _profile.SaveProfileDetails(profileRequest);
                return StatusCode(result.StatusCode, result);
            }

            return BadRequest("Invalid pofile request");
        }

        [HttpPut]
        [Route(ConstantResource.UpdateTestProfile)]
        public async Task<IActionResult>UpdateProfileDetails(ProfileRequest profileRequest)
        {
            if (!string.IsNullOrEmpty(profileRequest.PartnerId) && !string.IsNullOrEmpty(profileRequest.ProfileName))
            {
                var result = await _profile.UpdateProfileDetails(profileRequest);
                return StatusCode(result.StatusCode, result);
            }

            return BadRequest("Invalid pofile request");
        }

        [HttpDelete]
        [Route(ConstantResource.DeleteMappedTest)]
        public async Task<IActionResult> DeleteMappedTests(string mappingId, string partnerId)
        {
            if (!string.IsNullOrEmpty(partnerId) && !string.IsNullOrEmpty(mappingId))
            {
                var result = await _profile.DeleteMappingTests(mappingId, partnerId);
                return StatusCode(result.StatusCode, result);
            }

            return BadRequest(ConstantResource.MappingIdEmpty);
        }
    }
}
