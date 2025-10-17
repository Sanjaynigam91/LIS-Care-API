using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.CenterMaster;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class CenterBAL(ICenterRepository centerRepository) : ICenter
    {
        private readonly ICenterRepository _centerRepository = centerRepository;
        /// <summary>
        /// used to create new center
        /// </summary>
        /// <param name="centerRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> CreateNewCenter(CenterRequest centerRequest)
        {
            try
            {
                return await _centerRepository.CreateNewCenter(centerRequest);
            }
            catch
            {
                throw;
            }
        }

        public async Task<APIResponseModel<string>> DeleteCenter(string? centerCode, string? partnerId)
        {
            try
            {
                return await _centerRepository.DeleteCenter(centerCode,partnerId);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to get all center details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="centerStatus"></param>
        /// <param name="searchBy"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<CenterResponse>>> GetAllCenterDetails(string? partnerId, string? centerStatus = "", string? searchBy = "")
        {
            try
            {
                return await _centerRepository.GetAllCenterDetails(partnerId, centerStatus, searchBy);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to get center details by center code
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="centerCode"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<CenterResponse>>> GetCenterByCenterCode(string? partnerId, string? centerCode)
        {
            try
            {
                return await _centerRepository.GetCenterByCenterCode(partnerId,centerCode);
            }
            catch
            {
                throw;
            }
        }

        public async Task<APIResponseModel<List<CentreCustomRateResponse>>> GetCentreCustomRates(string? opType, string? centerCode, string? partnerId, string? testCode)
        {
            try
            {
                return await _centerRepository.GetCentreCustomRates(opType,centerCode,partnerId,testCode);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to get sales incharge details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<SalesInchargeResponse>>> GetSalesInchargeDetails(string? partnerId)
        {
            try
            {
                return await _centerRepository.GetSalesInchargeDetails(partnerId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to import center rates
        /// </summary>
        /// <param name="centerRates"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> ImportCentreTestRates(CenterRatesRequest centerRates)
        {
            try
            {
                return await _centerRepository.ImportCentreTestRates(centerRates);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to update center
        /// </summary>
        /// <param name="centerRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateCenter(CenterRequest centerRequest)
        {
            try
            {
                return await _centerRepository.UpdateCenter(centerRequest);
            }
            catch
            {
                throw;
            }
        }

        public async Task<APIResponseModel<string>> UpdateCentersRates(CenterRatesRequest centerRates)
        {
            try
            {
                return await _centerRepository.UpdateCentersRates(centerRates);
            }
            catch
            {
                throw;
            }
        }
    }
}
