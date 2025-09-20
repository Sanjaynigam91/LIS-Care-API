using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.ProfileMaster;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
