using LISCareDTO;
using LISCareDTO.ClientMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Interface
{
    public interface IClient
    {
        /// <summary>
        /// used to get all clients 
        /// </summary>
        /// <param name="clientStatus"></param>
        /// <param name="searchBy"></param>
        /// <param name="partnerId"></param>
        /// <param name="centerCode"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<ClientResponse>>> GetAllClients(string? clientStatus, string? searchBy, string partnerId, string? centerCode);
        /// <summary>
        /// Used to get client by Id
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<ClientResponse>>> GetClientById(string clientId, string partnerId);
        /// <summary>
        /// used to save client details
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> SaveClient(ClientRequest clientRequest);
        /// <summary>
        /// used to update Client details
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> UpdateClient(ClientRequest clientRequest);
        /// <summary>
        /// used to delete client by client Id
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> DeleteClient(string clientId, string partnerId);
    }
}
