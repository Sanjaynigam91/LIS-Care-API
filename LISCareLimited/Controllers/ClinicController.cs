using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.CenterMaster;
using LISCareDTO.ClinicMaster;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class ClinicController(IClinc clinic) : ControllerBase
    {
        private readonly IClinc _clinic= clinic;
        [HttpGet]
        [Route(ConstantResource.GetAllClinics)]
        public async Task<IActionResult> GetAllClinicDetails([FromQuery] string? partnerId, string? centerCode, string? clinicStatus, string? searchBy = "")
        {
            var response = new APIResponseModel<List<ClinicResponse>>
            {
                Data = []
            };
            try
            {
                response = await _clinic.GetAllClinicDetails(partnerId, centerCode,clinicStatus, searchBy);
                if (response.Data == null || response.Data.Count == 0)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No centers details found for the given PartnerId.";
                }
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
