using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.CenterMaster;
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

        /// <summary>
        /// used to get all clients
        /// </summary>
        /// <param name="clientStatus"></param>
        /// <param name="partnerId"></param>
        /// <param name="searchBy"></param>
        /// <param name="centerCode"></param>
        /// <returns></returns>
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

        /// <summary>
        /// used to get client by Id
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// used to add new client
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
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

        /// <summary>
        /// used to update client
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
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
            return BadRequest("Invalid client request");
        }

        /// <summary>
        /// used to delete client
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// used to get client custom rates
        /// </summary>
        /// <param name="opType"></param>
        /// <param name="clientCode"></param>
        /// <param name="partnerId"></param>
        /// <param name="testCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ConstantResource.GetClientCustomRates)]
        public async Task<IActionResult> GetAllClientCustomRates([FromQuery] string? opType, string? clientCode, string? partnerId, string? testCode)
        {
            _logger.LogInformation($"GetClientCustomRates, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<ClientCustomResponse>>
            {
                Data = []
            };
            try
            {
                response = await _client.GetClientCustomRates(opType, clientCode, partnerId, testCode);
                if (response.Data == null || response.Data.Count == 0)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.ResponseMessage = "No centers custom rates found for the given PartnerId.";
                }
                _logger.LogInformation($"GetClientCustomRates, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                _logger.LogInformation($"GetClientCustomRates, API execution falied at:{DateTime.Now} due to {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// used to update all test rates of a client
        /// </summary>
        /// <param name="clientRates"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(ConstantResource.UpdateClientTestRates)]
        public async Task<IActionResult> UpdateClientsAllTestRates(ClientRatesRequest clientRates)
        {
            _logger.LogInformation($"UpdateClientTestRates, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(clientRates.PartnerId) && !string.IsNullOrEmpty(clientRates.ClientCode))
            {
                var result = await _client.UpdateClientsRate(clientRates);
                _logger.LogInformation($"UpdateClientTestRates, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            _logger.LogInformation($"UpdateClientTestRates, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid client request");
        }

        /// <summary>
        /// used to import all test rates of a client
        /// </summary>
        /// <param name="clientRates"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ConstantResource.ImportClientRates)]
        public async Task<IActionResult> ImportClientTestRates(ClientRatesRequest clientRates)
        {
            _logger.LogInformation($"ImportClientRates, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(clientRates.PartnerId) && !string.IsNullOrEmpty(clientRates.ClientCode))
            {
                var result = await _client.ImportClientsRate(clientRates);
                _logger.LogInformation($"ImportClientRates, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            _logger.LogInformation($"ImportClientRates, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid client request");
        }

    }
}
