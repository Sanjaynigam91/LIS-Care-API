using LISCareBussiness.Interface;
using LISCareDTO.AnalyzerMaster;
using LISCareUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class RejectionController(IConfiguration configuration, IRejection rejection) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IRejection _rejection = rejection;

        [HttpPut]
        [Route(ConstantResource.RejectTestBeforeAccession)]
        [Authorize]
        public async Task<IActionResult> RejectTestsBeforeAccession(int patientSpecimenId, string testCode, string rejectionReason, string rejectedBy, string partnerId)
        {
            if (patientSpecimenId > 0)
            {
                var result = await _rejection.RejectTestsBeforeAccession(patientSpecimenId,testCode,rejectionReason,rejectedBy,partnerId);
                return StatusCode(result.StatusCode, result);
            }

            return BadRequest("Invalid Rejection request");
        }

        [HttpGet]
        [Route(ConstantResource.GetRejectedSamples)]
        [Authorize]
        public async Task<IActionResult> GetRejectedSamples([FromQuery] string partnerId, DateTime startDate, DateTime endDate, string? barcode, string? patientCode, string? clientCode)
        {
            try
            {
                var response = await _rejection.GetRejectedSamples(partnerId, startDate, endDate, barcode, patientCode, clientCode);

                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

    }
}
