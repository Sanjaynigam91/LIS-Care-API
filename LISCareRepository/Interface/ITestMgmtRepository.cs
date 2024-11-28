using LISCareDTO.TestMaster;

namespace LISCareRepository.Interface
{
    public interface ITestMgmtRepository
    {
        List<TestDataSearchResponse>GetTestDetails(TestMasterSearchRequest searchRequest);
        List<TestDepartmentResponse> GetTestDepartmentData(string partnerId);

    }
}
