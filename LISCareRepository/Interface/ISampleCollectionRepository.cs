using LISCareDTO.SampleCollectionPlace;

namespace LISCareRepository.Interface
{
    public interface ISampleCollectionRepository
    {
        List<SampleCollectedAtResponse> GetSampleCollectedPlace(string partnerId);
    }
}
