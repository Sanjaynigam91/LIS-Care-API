using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.Reporting;
using LISCareDTO.SampleAccession;
using LISCareUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class ReportingController : ControllerBase
    {
        private readonly IReporting reporting;
        private readonly ILogger<ReportingController> logger;

        public ReportingController(IReporting reporting, ILogger<ReportingController> logger)
        {
            this.reporting = reporting;
            this.logger = logger;
        }

        [HttpGet]
        [Route(ConstantResource.RetrievePendingPatients)]
        [Authorize]
        public async Task<IActionResult> RetrievePendingPatients([FromQuery] string partnerId,DateTime startDate,DateTime endDate,string? barcode,string? department,string? patientName,string? centerCode,
              string reportStatus)
        {
            try
            {
                var response = await reporting.RetrievePendingPatients(
                    partnerId,
                    startDate,
                    endDate,
                    barcode,
                    department,
                    patientName,
                    centerCode,
                    reportStatus);

                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

    }
}
