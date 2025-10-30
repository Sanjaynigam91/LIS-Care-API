using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.Barcode;
using LISCareDTO.Employee;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LISCareLimited.Controllers
{

    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class BarcodeController : ControllerBase
    {
        private readonly IBarcode _barcode;
        private readonly ILogger<BarcodeController> _logger;

        public BarcodeController(IBarcode barcode, ILogger<BarcodeController> logger)
        {
            _barcode = barcode;
            _logger = logger;
        }


        [HttpGet]
        [Route(ConstantResource.GetAllBarcodes)]
        public async Task<IActionResult> GetAllBarcodeDetails([FromQuery] string partnerId)
        {
            _logger.LogInformation($"GetAllBarcodes, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<BarcodeResponse>>
            {
                Data = []
            };
            try
            {
                response = await _barcode.GetAllBarcodeDetails(partnerId);
                _logger.LogInformation($"GetAllBarcodes, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                _logger.LogInformation($"GetAllBarcodes, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


    }
}
