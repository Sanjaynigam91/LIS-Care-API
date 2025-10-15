using LISCareDTO;
using LISCareDTO.AnalyzerMaster;
using LISCareDTO.CenterMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Interface
{
    public interface ICenterRepository
    {
        /// <summary>
        /// used to get all center details
        /// </summary>
        /// <param name="centerStatus"></param>
        /// <param name="searchBy"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<CenterResponse>>> GetAllCenterDetails(string? partnerId, string? centerStatus = "", string? searchBy = "");
        /// <summary>
        /// used to get sales incharge details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<SalesInchargeResponse>>> GetSalesInchargeDetails(string? partnerId);
        /// <summary>
        /// used to get center details by center code
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="centerCode"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<CenterResponse>>> GetCenterByCenterCode(string? partnerId, string? centerCode);
        /// <summary>
        /// used to create new center
        /// </summary>
        /// <param name="centerRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> CreateNewCenter(CenterRequest centerRequest);
        /// <summary>
        /// used to update center
        /// </summary>
        /// <param name="centerRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> UpdateCenter(CenterRequest centerRequest);
        /// <summary>
        /// used to delete center by center code
        /// </summary>
        /// <param name="centerCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> DeleteCenter(string? centerCode, string? partnerId);
        /// <summary>
        /// used to get center custom rates
        /// </summary>
        /// <param name="opType"></param>
        /// <param name="centerCode"></param>
        /// <param name="partnerId"></param>
        /// <param name="testCode"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<CentreCustomRateResponse>>> GetCentreCustomRates(string? opType, string? centerCode, string? partnerId, string? testCode);
        /// <summary>
        /// used to update centre's rates
        /// </summary>
        /// <param name="centerCode"></param>
        /// <param name="partnerId"></param>
        /// <param name="testCode"></param>
        /// <param name="billRate"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> UpdateCentersRates(CenterRatesRequest centerRates);

    }
}
