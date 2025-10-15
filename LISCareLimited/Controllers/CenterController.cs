using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.AnalyzerMaster;
using LISCareDTO.CenterMaster;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet]
        [Route(ConstantResource.GetSalesIncharge)]
        public async Task<IActionResult> GetSalesInchargeDetails([FromQuery] string? partnerId)
        {
            var response = new APIResponseModel<List<SalesInchargeResponse>>
            {
                Data = []
            };
            try
            {
                response = await _center.GetSalesInchargeDetails(partnerId);
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

        [HttpGet]
        [Route(ConstantResource.GetCenterByCenterCode)]
        public async Task<IActionResult> GetAllCenterByCenterCode([FromQuery] string? partnerId, string? centerCode)
        {
            var response = new APIResponseModel<List<CenterResponse>>
            {
                Data = []
            };
            try
            {
                response = await _center.GetCenterByCenterCode(partnerId, centerCode);
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

        [HttpPost]
        [Route(ConstantResource.AddNewCenter)]
        public async Task<IActionResult> CreateNewCenter(CenterRequest centerRequest)
        {
            if (!string.IsNullOrEmpty(centerRequest.PartnerId) && !string.IsNullOrEmpty(centerRequest.CenterCode))
            {
                var result = await _center.CreateNewCenter(centerRequest);

                return StatusCode(result.StatusCode, result);
            }

            return BadRequest("Invalid center request");
        }
        [HttpPut]
        [Route(ConstantResource.UpdateCenter)]
        public async Task<IActionResult> UpdateCenterDetails(CenterRequest centerRequest)
        {
            if (!string.IsNullOrEmpty(centerRequest.PartnerId) && !string.IsNullOrEmpty(centerRequest.CenterCode))
            {
                var result = await _center.UpdateCenter(centerRequest);

                return StatusCode(result.StatusCode, result);
            }

            return BadRequest("Invalid center request");
        }
        [HttpDelete]
        [Route(ConstantResource.DeleteCenter)]
        public async Task<IActionResult> DeleteCenterByCenterCode(string? centerCode, string? partnerId)
        {
            if (!string.IsNullOrEmpty(centerCode))
            {
                var result = await _center.DeleteCenter(centerCode, partnerId);
                return StatusCode(result.StatusCode, result);
            }

            return BadRequest(ConstantResource.CenterCodeEmpty);
        }

        [HttpGet]
        [Route(ConstantResource.GetCentreCustomRates)]
        public async Task<IActionResult> GetAllCenterCustomRates([FromQuery] string? opType, string? centerCode, string? partnerId, string? testCode)
        {
            var response = new APIResponseModel<List<CentreCustomRateResponse>>
            {
                Data = []
            };
            try
            {
                response = await _center.GetCentreCustomRates(opType,centerCode,partnerId,testCode);
                if (response.Data == null || response.Data.Count == 0)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No centers custom rates found for the given PartnerId.";
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

        [HttpPut]
        [Route(ConstantResource.UpdateCenterRates)]
        public async Task<IActionResult> UpdateCentersRates(CenterRatesRequest centerRates)
        {
            if (!string.IsNullOrEmpty(centerRates.PartnerId) && !string.IsNullOrEmpty(centerRates.CenterCode))
            {
                var result = await _center.UpdateCentersRates(centerRates);

                return StatusCode(result.StatusCode, result);
            }

            return BadRequest("Invalid center request");
        }

    }
}
