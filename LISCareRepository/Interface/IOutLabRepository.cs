using LISCareDTO;
using LISCareDTO.OutLab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Interface
{
    public interface IOutLabRepository
    {
        /// <summary>
        /// used to get out labs details
        /// </summary>
        /// <param name="labStatus"></param>
        /// <param name="labname"></param>
        /// <param name="labCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<OutLabResponse>>> GetAllOutLabs(bool? labStatus, string? labname, string? labCode, string partnerId);
       /// <summary>
       /// used to get out lab by lab code
       /// </summary>
       /// <param name="labCode"></param>
       /// <param name="partnerId"></param>
       /// <returns></returns>
        Task<APIResponseModel<List<OutLabResponse>>>GetOutLabByLabCode(string labCode, string partnerId);
       /// <summary>
       /// used to delete out lab Details
       /// </summary>
       /// <param name="labCode"></param>
       /// <param name="partnerId"></param>
       /// <returns></returns>
        Task<APIResponseModel<string>>DeleteOutLabDetails(string labCode, string partnerId);
        /// <summary>
        /// used to add or create new out lab details
        /// </summary>
        /// <param name="outLabRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> AddNewOutLab(OutLabRequest outLabRequest);
        /// <summary>
        /// used to update out lab details
        /// </summary>
        /// <param name="outLabRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>>UpdateOutLabDetails(OutLabRequest outLabRequest);
    }
}
