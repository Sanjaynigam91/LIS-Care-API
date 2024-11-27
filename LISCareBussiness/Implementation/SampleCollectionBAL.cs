using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.SampleCollectionPlace;
using LISCareRepository.Interface;
using LISCareReposotiory.Implementation;
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
    }
}
