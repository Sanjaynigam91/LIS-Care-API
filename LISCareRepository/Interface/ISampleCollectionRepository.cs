using LISCareDTO;
using LISCareDTO.SampleCollectionPlace;

namespace LISCareRepository.Interface
{
    public interface ISampleCollectionRepository
    {
        List<SampleCollectedAtResponse> GetSampleCollectedPlace(string partnerId);
        APIResponseModel<object> AddSampleCollectedPlaces(SampleCollectedRequest sampleCollected);
        APIResponseModel<object> RemoveSamplePlace(int recordId, string partnerId);

    }
}
