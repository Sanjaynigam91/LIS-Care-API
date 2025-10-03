using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.AnalyzerMaster;
using LISCareRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class AnalyzerBAL(IConfiguration configuration, IAnalyzerRepository analyzerRepository) : IAnalyzer
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IAnalyzerRepository _analyzerRepository = analyzerRepository;
        /// <summary>
        /// used to get all analyzer details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="AnalyzerNameOrShortCode"></param>
        /// <param name="AnalyzerStatus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<APIResponseModel<List<AnalyzerResponse>>> GetAllAnalyzerDetails(string partnerId, string? AnalyzerNameOrShortCode = "", string? AnalyzerStatus = "")
        {
            try
            {
                return await _analyzerRepository.GetAllAnalyzerDetails(partnerId, AnalyzerNameOrShortCode, AnalyzerStatus);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to get all suppliers
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<SupplierResponse>>> GetAllSuppliers(string partnerId)
        {
            try
            {
                return await _analyzerRepository.GetAllSuppliers(partnerId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to get analyzer details by analyzerId
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="analyzerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<AnalyzerResponse>>> GetAnalyzerDetails(string partnerId, int analyzerId)
        {
            try
            {
                return await _analyzerRepository.GetAnalyzerDetails(partnerId, analyzerId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to get Analyzer's test mappings
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="analyzerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<AnalyzerMappingResponse>>> GetAnalyzerTestMappings(string partnerId, int analyzerId)
        {
            try
            {
                return await _analyzerRepository.GetAnalyzerTestMappings(partnerId, analyzerId);
            }
            catch
            {
                throw;
            }
        }
    }
}
