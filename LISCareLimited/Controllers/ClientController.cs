using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.ClientMaster;
using LISCareDTO.ClinicMaster;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class ClientController(IClient client, ILogger<ClientController> logger) : ControllerBase
    {
        private readonly IClient _client= client;
        private readonly ILogger<ClientController> _logger=logger;
        [HttpGet]
        [Route(ConstantResource.GetAllClients)]
        public async Task<IActionResult> GetAllClientDetails([FromQuery] string? clientStatus, string partnerId, string? searchBy = "", string? centerCode = "")
        {
           _logger.LogInformation($"GetAllClients, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<ClientResponse>>
            {
                Data = []
            };
            try
            {
                response = await _client.GetAllClients(clientStatus, searchBy, partnerId, centerCode);
                if (response.Data == null || response.Data.Count == 0)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No client details found for the given PartnerId.";
                }
               _logger.LogInformation($"GetAllClients, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                _logger.LogInformation($"GetAllClients, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route(ConstantResource.GetClientById)]
        public async Task<IActionResult> GetClientById([FromQuery] string clientId, string partnerId)
        {
            _logger.LogInformation($"GetClientById, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<ClientResponse>>
            {
                Data = []
            };
            try
            {
                response = await _client.GetClientById(clientId, partnerId);
                if (response.Data == null || response.Data.Count == 0)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No client details found for the given clientId.";
                }
                _logger.LogInformation($"GetClientById, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                _logger.LogInformation($"GetClientById, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
