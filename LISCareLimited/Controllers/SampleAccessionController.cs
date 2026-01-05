using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.SampleAccession;
using LISCareDTO.SampleManagement;
using LISCareReporting.LISBarcode;
using LISCareRepository.Implementation;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class SampleAccessionController : ControllerBase
    {
        private readonly IAccession accession;
        private readonly ILogger<SampleAccessionController> logger;

        public SampleAccessionController(IAccession accession, ILogger<SampleAccessionController> logger)
        {
            this.accession = accession;
            this.logger = logger;
        }

        [HttpGet]
        [Route(ConstantResource.GetAllSamplesForAccession)]
        public async Task<IActionResult> GetAllPendingSampleForAccession([FromQuery] DateTime startDate, DateTime endDate, string? barcode, string? centerCode,
            string? patientName, string partnerId)
        {
            var response = new APIResponseModel<List<PendingAccessionResponse>>
            {
                Data = []
            };
            response = await accession.GetPendingSampleForAccession(startDate, endDate, barcode, centerCode, patientName, partnerId);

            if (patientName != null)
            {
                var result = response.Data.Where(x => x.PatientName == patientName);
                response.Data = result.ToList();
                return StatusCode(response.StatusCode, response);
            }

            if (response.Data.Count > 0)
            {
                return StatusCode(response.StatusCode, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

        }


        [HttpGet]
        [Route(ConstantResource.GetLastImported)]
        public async Task<IActionResult> GetLastImported([FromQuery] DateTime woeDate, string partnerId)
        {
            var response = new APIResponseModel<int>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = 0
            };
            response = await accession.GetLastImported(woeDate, partnerId);

            if (response.Status)
            {
                return StatusCode(response.StatusCode, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

        }

        [HttpGet]
        [Route(ConstantResource.GetSampleTypeByVisitId)]
        public async Task<IActionResult> GetSampleTypeByVisitId([FromQuery] int visitId, string partnerId)
        {
            var response = new APIResponseModel<List<SampleTypeResponse>>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = []
            };
            response = await accession.GetSampleTypesByVisitid(visitId, partnerId);

            if (response.Status)
            {
                return StatusCode(response.StatusCode, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

        }

        [HttpGet]
        [Route(ConstantResource.GetPatientInfoByBarcode)]
        public async Task<IActionResult> GetPatientInfoByBarcode([FromQuery] int visitId, string? sampleType, string partnerId)
        {
            var response = new APIResponseModel<PatientInfoResponse>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = null
            };
            response = await accession.GetPatientInfoByBarcode(visitId, sampleType, partnerId);

            if (response.Status)
            {
                return StatusCode(response.StatusCode, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

        }

        [HttpGet]
        [Route(ConstantResource.GetTestsByBarcode)]
        public async Task<IActionResult> GetTestDetailsByBarcode([FromQuery] string barcode, string? sampleType, string partnerId)
        {
            var response = new APIResponseModel<List<SampleAccessionTestResponse>>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = []
            };
            response = await accession.GetTestDetailsByBarcode(barcode, sampleType, partnerId);

            if (response.Status)
            {
                return StatusCode(response.StatusCode, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

        }

        [HttpGet]
        [Route(ConstantResource.PrintBarcode)]
        public async Task<IActionResult> PrintBarcode([FromQuery] int visitId, string? sampleType, string partnerId)
        {
            var response = new APIResponseModel<BarcodeResponse>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = null
            };
            response = await accession.CreateBarcode(visitId, sampleType, partnerId);

            if (response.Status && response.Data != null)
            {
                var pdfBytes = SampleBarcode.GenerateBarcodeLabel(response.Data);
                // Return the PDF file as a FileResult instead of writing to disk
                return File(pdfBytes, "application/pdf", "SampleBarcode.pdf");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route(ConstantResource.AcceptSampleByBarcode)]
        public async Task<IActionResult> AcceptSelectedSample(AcceptSampleRequest acceptSample)
        {
            var response = new APIResponseModel<AcceptSampleResponse>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = null
            };
            response = await accession.AcceptSelectedSample(acceptSample);

            if (response.Status && response.Data != null)
            {
                return StatusCode(response.StatusCode, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
