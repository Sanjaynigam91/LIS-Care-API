using LISCareDTO;
using LISCareDTO.SampleCollectionPlace;
using LISCareDTO.SampleManagement;

namespace LISCareRepository.Interface
{
    public interface ISampleCollectionRepository
    {
        List<SampleCollectedAtResponse> GetSampleCollectedPlace(string partnerId);
        APIResponseModel<object> AddSampleCollectedPlaces(SampleCollectedRequest sampleCollected);
        APIResponseModel<object> RemoveSamplePlace(int recordId, string partnerId);
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
        Task<APIResponseModel<List<SampleCollectionResponse>>> GetPatientsForCollection(DateTime startDate, DateTime endDate, string? patientCode, string? centerCode, string? patientName, string partnerId);
        /// <summary>
        /// used to get samples for collection by patientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<SamplePendingCollectionResponse>>> GetSamplesForCollection(Guid patientId);
        /// <summary>
        /// used to get requested Sample for Collection
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<RequestedTest>>> GetRequestedSampleForCollection(Guid patientId, string? barcode);
        /// <summary>
        /// used to update sample status as collection done
        /// </summary>
        /// <param name="sampleCollected"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> UpdateSampleStatusAsCollectionDone(SampleRequest sampleRequest);

    }
}
