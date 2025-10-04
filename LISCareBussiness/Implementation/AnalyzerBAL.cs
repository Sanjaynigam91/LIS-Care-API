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

        public async Task<APIResponseModel<string>> DeleteAnalyzerDetails(int analyzerId, string partnerId)
        {
            try
            {
                return await _analyzerRepository.DeleteAnalyzerDetails(analyzerId, partnerId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to delete analyzer test mapping
        /// </summary>
        /// <param name="mappingId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> DeleteAnalyzerTestMapping(int mappingId, string partnerId)
        {
            try
            {
                return await _analyzerRepository.DeleteAnalyzerTestMapping(mappingId, partnerId);
            }
            catch
            {
                throw;
            }
        }

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

        public async Task<APIResponseModel<List<AnalyzerTestMappingResponse>>> GetAnalyzerTestMappingById(int mappingId, string partnerId)
        {
            try
            {
                return await _analyzerRepository.GetAnalyzerTestMappingById(mappingId, partnerId);
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
        /// <summary>
        /// used to save analyzer details
        /// </summary>
        /// <param name="analyzerRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> SaveAnalyzerDetails(AnalyzerRequest analyzerRequest)
        {
            try
            {
                return await _analyzerRepository.SaveAnalyzerDetails(analyzerRequest);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to save analyzer test mapping
        /// </summary>
        /// <param name="mappingRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> SaveAnalyzerTestMapping(AnalyzerMappingRequest mappingRequest)
        {
            try
            {
                return await _analyzerRepository.SaveAnalyzerTestMapping(mappingRequest);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to update analyzer details
        /// </summary>
        /// <param name="analyzerRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateAnalyzerDetails(AnalyzerRequest analyzerRequest)
        {
            try
            {
                return await _analyzerRepository.UpdateAnalyzerDetails(analyzerRequest);
            }
            catch
            {
                throw;
            }
        }

        public async Task<APIResponseModel<string>> UpdateAnalyzerTestMapping(AnalyzerMappingRequest mappingRequest)
        {
            try
            {
                return await _analyzerRepository.UpdateAnalyzerTestMapping(mappingRequest);
            }
            catch
            {
                throw;
            }
        }
    }
}
