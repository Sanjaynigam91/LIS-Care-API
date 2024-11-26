using LISCareBussiness.Interface;
using LISCareDTO.SampleCollectionPlace;
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
    }
}
