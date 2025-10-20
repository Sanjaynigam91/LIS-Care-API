using LISCareDTO;
using LISCareDTO.ClinicMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Interface
{
    public interface IClinc
    {
        /// <summary>
        /// used to get all clinic details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="centerCode"></param>
        /// <param name="clinicStatus"></param>
        /// <param name="searchBy"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<ClinicResponse>>> GetAllClinicDetails(string? partnerId, string? centerCode, string? clinicStatus, string? searchBy = "");
        /// <summary>
        /// used to create new clinic
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> CreateNewClinic(ClinicRequest request);
        /// <summary>
        /// used to create new clinic
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> UpdateClinic(ClinicRequest request);
        /// <summary>
        /// used to delete clinic details
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> DeleteClinic(int clinicId, string partnerId);
        /// <summary>
        /// used to get clinic detail by Id
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<ClinicResponse>>> GetClinicDeatilsById(int clinicId, string partnerId);

    }
}
