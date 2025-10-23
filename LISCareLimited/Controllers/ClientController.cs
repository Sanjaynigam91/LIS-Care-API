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

        [HttpPost]
        [Route(ConstantResource.AddNewClient)]
        public async Task<IActionResult> CreateNewClinic(ClientRequest clientRequest)
        {
            _logger.LogInformation($"AddNewClient, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(clientRequest.PartnerId) && !string.IsNullOrEmpty(clientRequest.ClientName))
            {
                var result = await _client.SaveClient(clientRequest);
                _logger.LogInformation($"AddNewClient, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            _logger.LogInformation($"AddNewClient, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid center request");
        }

        [HttpPut]
        [Route(ConstantResource.UpdateClient)]
        public async Task<IActionResult> UpdateClientDeatils(ClientRequest clientRequest)
        {
            _logger.LogInformation($"UpdateClient, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(clientRequest.PartnerId) && !string.IsNullOrEmpty(clientRequest.ClientName))
            {
                var result = await _client.UpdateClient(clientRequest);
                _logger.LogInformation($"UpdateClient, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            _logger.LogInformation($"UpdateClient, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid center request");
        }

        [HttpDelete]
        [Route(ConstantResource.DeleteClient)]
        public async Task<IActionResult> DeleteClientById([FromQuery] string clientId, string partnerId)
        {
            _logger.LogInformation($"UpdateClient, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(partnerId))
            {
                var result = await _client.DeleteClient(clientId,partnerId);
                _logger.LogInformation($"UpdateClient, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            _logger.LogInformation($"UpdateClient, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid center request");
        }


    }
}
