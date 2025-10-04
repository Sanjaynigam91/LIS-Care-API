using LISCareDTO;
using LISCareDTO.AnalyzerMaster;
using LISCareDTO.ProfileMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Interface
{
    public interface IAnalyzerRepository
    {
        /// <summary>
        /// used to get all analyzer details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<AnalyzerResponse>>> GetAllAnalyzerDetails(string partnerId, string? AnalyzerNameOrShortCode = "", string? AnalyzerStatus="");
        /// <summary>
        /// used to get all suppliers
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<SupplierResponse>>> GetAllSuppliers(string partnerId);
        /// <summary>
        /// used to get analyzer details by analyzerId
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="analyzerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<AnalyzerResponse>>> GetAnalyzerDetails(string partnerId,int analyzerId);
        /// <summary>
        /// used to get anlyzer's test mapping details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="analyzerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<AnalyzerMappingResponse>>>GetAnalyzerTestMappings(string partnerId, int analyzerId);
        /// <summary>
        /// used to save analyzer details
        /// </summary>
        /// <param name="analyzerRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> SaveAnalyzerDetails(AnalyzerRequest analyzerRequest);
        /// <summary>
        /// used to update analyzer details
        /// </summary>
        /// <param name="analyzerRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> UpdateAnalyzerDetails(AnalyzerRequest analyzerRequest);
        /// <summary>
        /// used to delete analyzer details
        /// </summary>
        /// <param name="analyzerId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>>DeleteAnalyzerDetails(int analyzerId, string partnerId);



    }
}
