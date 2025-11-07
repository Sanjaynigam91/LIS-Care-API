using Autofac.Core;
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


        [HttpGet(ConstantResource.GenerateBulkBarcode)]
        public async Task<IActionResult> GenerateBarcodes([FromQuery] int sequenceStart, int sequenceEnd)
        {
            try
            {
                if (sequenceStart <= 0)
                    sequenceStart = 1;

                if (sequenceEnd < sequenceStart)
                    sequenceEnd = sequenceStart;

                if (sequenceStart <= 0)
                {
                    return BadRequest("Invalid sequence range provided.");
                }
                var pdfBytes = await _barcode.GenerateBarcodes(sequenceStart, sequenceEnd);
                return File(pdfBytes, "application/pdf", "Barcodes.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generating barcodes: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet(ConstantResource.GetLastPrintedBarcode)]
        public async Task<IActionResult> GetLastProintedBarcode([FromQuery] string partnerId)
        {
            _logger.LogInformation($"GetLastPrintedBarcode, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<int>
            {
                Data = 0
            };
            try
            {
                response = await _barcode.GetLastPrintedBarcode(partnerId);
                _logger.LogInformation($"GetLastPrintedBarcode, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                _logger.LogInformation($"GetLastPrintedBarcode, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        [Route(ConstantResource.SavePrintedBarcode)]
        public async Task<IActionResult> SavePrintedBarcode(BarcodeRequest barcodeRequest)
        {
            _logger.LogInformation($"SavePrintedBarcode, API execution started at:{DateTime.Now}");
            if (barcodeRequest.SequenceStart > 0 && barcodeRequest.SequenceEnd > 0)
            {
                var result = await _barcode.SavePrintedBarcodes(barcodeRequest);
                _logger.LogInformation($"SavePrintedBarcode, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            _logger.LogInformation($"SavePrintedBarcode, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid employee request");
        }

    }
}
