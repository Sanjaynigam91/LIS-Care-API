using LISCareDTO;
using LISCareDTO.SampleCollectionPlace;

namespace LISCareBussiness.Interface
{
    public interface ISampleCollection
    {
        List<SampleCollectedAtResponse> GetSampleCollectedPlace(string partnerId);
        APIResponseModel<object> AddSampleCollectedPlaces(SampleCollectedRequest sampleCollected);
        APIResponseModel<object> RemoveSamplePlace(int recordId, string partnerId);
    }
}
