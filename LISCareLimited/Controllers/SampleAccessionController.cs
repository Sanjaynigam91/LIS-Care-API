using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.SampleAccession;
using LISCareDTO.SampleManagement;
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
        public async Task<IActionResult> GetPatientInfoByBarcode([FromQuery] Guid patientId, string partnerId)
        {
            var response = new APIResponseModel<PatientInfoResponse>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = null
            };
            response = await accession.GetPatientInfoByBarcode(patientId, partnerId);

            if (response.Status)
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
