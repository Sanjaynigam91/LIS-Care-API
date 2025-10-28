using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.OutLab;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class OutLabBAL(IOutLabRepository outLabRepository) : IOutLab
    {
        private readonly IOutLabRepository _outLabRepository = outLabRepository;

        /// <summary>
        /// used to create or add new out lab
        /// </summary>
        /// <param name="outLabRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> AddNewOutLab(OutLabRequest outLabRequest)
        {
            try
            {
                return await _outLabRepository.AddNewOutLab(outLabRequest);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to delete out lab
        /// </summary>
        /// <param name="labCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> DeleteOutLabDetails(string labCode, string partnerId)
        {
            try
            {
                return await _outLabRepository.DeleteOutLabDetails(labCode, partnerId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to get out labs
        /// </summary>
        /// <param name="labStatus"></param>
        /// <param name="labname"></param>
        /// <param name="labCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<OutLabResponse>>> GetAllOutLabs(bool? labStatus, string? labname, string? labCode, string partnerId)
        {
            try
            {
                return await _outLabRepository.GetAllOutLabs(labStatus, labname, labCode, partnerId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to get out lab by lab code
        /// </summary>
        /// <param name="labCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<OutLabResponse>>> GetOutLabByLabCode(string labCode, string partnerId)
        {
            try
            {
                return await _outLabRepository.GetOutLabByLabCode(labCode, partnerId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to get all out lab rates
        /// </summary>
        /// <param name="optype"></param>
        /// <param name="labCode"></param>
        /// <param name="testCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<OutlabRatesRespons>>> GetOutLabRates(string optype, string? labCode, string? testCode, string partnerId)
        {
            try
            {
                return await _outLabRepository.GetOutLabRates(optype, labCode, testCode, partnerId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<APIResponseModel<string>> UpdateOutLabDetails(OutLabRequest outLabRequest)
        {
            try
            {
                return await _outLabRepository.UpdateOutLabDetails(outLabRequest);
            }
            catch
            {
                throw;
            }
        }
    }
}
