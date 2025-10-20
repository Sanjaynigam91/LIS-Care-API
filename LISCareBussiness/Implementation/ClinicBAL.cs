using Azure.Core;
using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.ClinicMaster;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class ClinicBAL(IClinicRepository clinicRepository) : IClinc
    {
        private readonly IClinicRepository _clinicRepository = clinicRepository;
        /// <summary>
        /// used to Create new Clinic
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> CreateNewClinic(ClinicRequest request)
        {
            try
            {
                return await _clinicRepository.CreateNewClinic(request);
            }
            catch
            {
                throw;
            }
        }

        public async Task<APIResponseModel<string>> DeleteClinic(int clinicId, string partnerId)
        {
            try
            {
                return await _clinicRepository.DeleteClinic(clinicId, partnerId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<APIResponseModel<List<ClinicResponse>>> GetAllClinicDetails(string? partnerId, string? centerCode, string? clinicStatus, string? searchBy = "")
        {
            try
            {
                return await _clinicRepository.GetAllClinicDetails(partnerId, centerCode, clinicStatus, searchBy);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to Get clinic detail by Id
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<ClinicResponse>>> GetClinicDeatilsById(int clinicId, string partnerId)
        {
            try
            {
                return await _clinicRepository.GetClinicDeatilsById(clinicId, partnerId);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to update clinic details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateClinic(ClinicRequest request)
        {
            try
            {
                return await _clinicRepository.UpdateClinic(request);
            }
            catch
            {
                throw;
            }
        }
    }
}
