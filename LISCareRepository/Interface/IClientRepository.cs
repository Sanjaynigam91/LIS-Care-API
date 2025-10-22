using LISCareDTO;
using LISCareDTO.ClientMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Interface
{
    public interface IClientRepository
    {
        /// <summary>
        /// used to get all clients 
        /// </summary>
        /// <param name="clientStatus"></param>
        /// <param name="searchBy"></param>
        /// <param name="partnerId"></param>
        /// <param name="centerCode"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<ClientResponse>>> GetAllClients(bool clientStatus, string? searchBy, string partnerId, string? centerCode);
        /// <summary>
        /// Used to get client by Id
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<ClientResponse>>>GetClientById(string clientId,string partnerId);
    }
}
