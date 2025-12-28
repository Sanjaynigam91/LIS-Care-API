using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.SampleCollectionPlace;
using LISCareDTO.SampleManagement;
using LISCareRepository.Interface;
using Microsoft.Extensions.Configuration;

namespace LISCareBussiness.Implementation
{
    public class SampleCollectionBAL : ISampleCollection
    {
        private readonly IConfiguration _configuration;
        private readonly ISampleCollectionRepository _sampleCollectionRepository;

        public SampleCollectionBAL(IConfiguration configuration, ISampleCollectionRepository sampleCollectionRepository)
        {
            _configuration = configuration;
            _sampleCollectionRepository = sampleCollectionRepository;
        }

        public List<SampleCollectedAtResponse> GetSampleCollectedPlace(string partnerId)
        {
            var response = new List<SampleCollectedAtResponse>();
            try
            {
                response = _sampleCollectionRepository.GetSampleCollectedPlace(partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public APIResponseModel<object> AddSampleCollectedPlaces(SampleCollectedRequest sampleCollected)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = _sampleCollectionRepository.AddSampleCollectedPlaces(sampleCollected);
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }

        public APIResponseModel<object> RemoveSamplePlace(int recordId, string partnerId)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = _sampleCollectionRepository.RemoveSamplePlace(recordId, partnerId);
            }
            catch { throw; }
            return response;
        }
        /// <summary>
        /// used to get pending samples for collection
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="patientCode"></param>
        /// <param name="centerCode"></param>
        /// <param name="patientName"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<SampleCollectionResponse>>> GetPatientsForCollection(DateTime startDate, DateTime endDate, string? patientCode, string? centerCode, string? patientName, string partnerId)
        {
            APIResponseModel<List<SampleCollectionResponse>> response;
            try
            {
                response = await _sampleCollectionRepository.GetPatientsForCollection(startDate, endDate, patientCode, centerCode, patientName, partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public async Task<APIResponseModel<List<SamplePendingCollectionResponse>>> GetSamplesForCollection(Guid patientId)
        {
            APIResponseModel<List<SamplePendingCollectionResponse>> response;
            try
            {
                response = await _sampleCollectionRepository.GetSamplesForCollection(patientId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public async Task<APIResponseModel<List<RequestedTest>>> GetRequestedSampleForCollection(Guid patientId, string barcode)
        {
            APIResponseModel<List<RequestedTest>> response;
            try
            {
                response = await _sampleCollectionRepository.GetRequestedSampleForCollection(patientId,barcode);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public async Task<APIResponseModel<string>> UpdateSampleStatusAsCollectionDone(SampleRequest sampleRequest)
        {
            APIResponseModel<string> response;
            try
            {
                response = await _sampleCollectionRepository.UpdateSampleStatusAsCollectionDone(sampleRequest);
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
