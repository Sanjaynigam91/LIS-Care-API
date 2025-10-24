using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.ClientMaster;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class ClientBAL(IClientRepository clientRepository) : IClient
    {
        public readonly IClientRepository _clientRepository = clientRepository;

        public async Task<APIResponseModel<string>> DeleteClient(string clientId, string partnerId)
        {
            try
            {
                return await _clientRepository.DeleteClient(clientId, partnerId);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to get all clients
        /// </summary>
        /// <param name="clientStatus"></param>
        /// <param name="searchBy"></param>
        /// <param name="partnerId"></param>
        /// <param name="centerCode"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<ClientResponse>>> GetAllClients(string? clientStatus, string? searchBy, string partnerId, string? centerCode)
        {
            try
            {
                return await _clientRepository.GetAllClients(clientStatus, searchBy, partnerId, centerCode);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get client by Id
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<ClientResponse>>> GetClientById(string clientId, string partnerId)
        {
            try
            {
                return await _clientRepository.GetClientById(clientId, partnerId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<APIResponseModel<List<ClientCustomResponse>>> GetClientCustomRates(string? opType, string? clientCode, string? partnerId, string? testCode)
        {
            try
            {
                return await _clientRepository.GetClientCustomRates(opType,clientCode, partnerId,testCode);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to save client details
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> SaveClient(ClientRequest clientRequest)
        {
            try
            {
                return await _clientRepository.SaveClient(clientRequest);
            }
            catch
            {
                throw;
            }
        }

        public async Task<APIResponseModel<string>> UpdateClient(ClientRequest clientRequest)
        {
            try
            {
                return await _clientRepository.UpdateClient(clientRequest);
            }
            catch
            {
                throw;
            }
        }
    }
}
