using LISCareDTO.TestMaster;

namespace LISCareRepository.Interface
{
    public interface ITestMgmtRepository
    {
        List<TestDataSearchResponse>GetTestDetails(TestMasterSearchRequest searchRequest);

    }
}
