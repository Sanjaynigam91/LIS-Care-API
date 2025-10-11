using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.AnalyzerMaster;
using LISCareDTO.CenterMaster;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class CenterController(ICenter center) : ControllerBase
    {
        private readonly ICenter _center = center;
        [HttpGet]
        [Route(ConstantResource.GetAllCenters)]
        public async Task<IActionResult> GetAllCenterDetails([FromQuery] string? partnerId, string? centerStatus = "", string? searchBy = "")
        {
            var response = new APIResponseModel<List<CenterResponse>>
            {
                Data = []
            };
            try
            {
                response = await _center.GetAllCenterDetails(partnerId, centerStatus, searchBy);
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
