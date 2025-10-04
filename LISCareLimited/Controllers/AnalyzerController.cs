using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.AnalyzerMaster;
using LISCareDTO.ProfileMaster;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class AnalyzerController(IConfiguration configuration, IAnalyzer analyzer) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IAnalyzer _analyzer = analyzer;

        /// <summary>
        /// used to get all analyzer details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ConstantResource.GetAllAnalyzers)]
        public async Task<IActionResult> GetAllAnalyzersDetails([FromQuery] string partnerId, string? AnalyzerNameOrShortCode = "", string? AnalyzerStatus = "")
        {
            var response = new APIResponseModel<List<AnalyzerResponse>>
            {
                Data = []
            };
            try
            {
                response = await _analyzer.GetAllAnalyzerDetails(partnerId, AnalyzerNameOrShortCode, AnalyzerStatus);
                if (response.Data == null || response.Data.Count == 0)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No analyzers details found for the given PartnerId.";
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
        [Route(ConstantResource.GetAllSuppliers)]
        public async Task<IActionResult> GetAllSupplierData([FromQuery] string partnerId)
        {
            var response = new APIResponseModel<List<SupplierResponse>>
            {
                Data = []
            };
            try
            {
                response = await _analyzer.GetAllSuppliers(partnerId);
                if (response.Data == null || response.Data.Count == 0)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No suppliers details found for the given PartnerId.";
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

        /// <summary>
        /// used to get all analyzer details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="analyzerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ConstantResource.GetAnalyzerDetailById)]
        public async Task<IActionResult> GetAnalyzersDetailsById([FromQuery] string partnerId, int analyzerId)
        {
            var response = new APIResponseModel<List<AnalyzerResponse>>
            {
                Data = []
            };
            try
            {
                response = await _analyzer.GetAnalyzerDetails(partnerId, analyzerId);
                if (response.Data == null)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No analyzers details found for the given PartnerId.";
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
        /// <summary>
        /// used to get analyzer's test mappings
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="analyzerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ConstantResource.GetAnalyzerTestMappings)]
        public async Task<IActionResult> GetAnalyzersMappings([FromQuery] string partnerId, int analyzerId)
        {
            var response = new APIResponseModel<List<AnalyzerMappingResponse>>
            {
                Data = []
            };
            try
            {
                response = await _analyzer.GetAnalyzerTestMappings(partnerId, analyzerId);
                if (response.Data == null)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No analyzers test mappings details found for the given PartnerId.";
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
        [Route(ConstantResource.AddNewAnalyzer)]
        public async Task<IActionResult> AddNewLISAanalyzer(AnalyzerRequest analyzerRequest)
        {
            if (!string.IsNullOrEmpty(analyzerRequest.PartnerId) && !string.IsNullOrEmpty(analyzerRequest.AnalyzerName))
            {
                var result = await _analyzer.SaveAnalyzerDetails(analyzerRequest);
                return StatusCode(result.StatusCode, result);
            }

            return BadRequest("Invalid analyzer request");
        }
        [HttpPut]
        [Route(ConstantResource.UpdateAnalyzer)]
        public async Task<IActionResult> UpdateLISAanalyzer(AnalyzerRequest analyzerRequest)
        {
            if (analyzerRequest.AnalyzerId > 0)
            {
                var result = await _analyzer.UpdateAnalyzerDetails(analyzerRequest);
                return StatusCode(result.StatusCode, result);
            }

            return BadRequest("Invalid analyzer request");
        }

        [HttpDelete]
        [Route(ConstantResource.DeleteAnalyzer)]
        public async Task<IActionResult> DeleteAnalyzerById(int analyzerId, string partnerId)
        {
            if (analyzerId > 0 && !string.IsNullOrEmpty(partnerId))
            {
                var result = await _analyzer.DeleteAnalyzerDetails(analyzerId, partnerId);
                return StatusCode(result.StatusCode, result);
            }

            return BadRequest(ConstantResource.ProfileCodeEmpty);
        }
    }
}
