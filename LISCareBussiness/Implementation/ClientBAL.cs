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
        /// <summary>
        /// used to get all clients
        /// </summary>
        /// <param name="clientStatus"></param>
        /// <param name="searchBy"></param>
        /// <param name="partnerId"></param>
        /// <param name="centerCode"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<ClientResponse>>> GetAllClients(bool clientStatus, string? searchBy, string partnerId, string? centerCode)
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
    }
}
