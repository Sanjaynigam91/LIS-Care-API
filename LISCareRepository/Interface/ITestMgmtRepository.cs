using LISCareDTO;
using LISCareDTO.TestMaster;

namespace LISCareRepository.Interface
{
    public interface ITestMgmtRepository
    {
        List<TestDataSearchResponse>GetTestDetails(TestMasterSearchRequest searchRequest);
        List<TestDepartmentResponse> GetTestDepartmentData(string partnerId);
        List<TestDataResponse> ViewTestData(string partnerId,string testCode);
        // This interface used delete Users
        APIResponseModel<object> DeleteTestByTestCode(string partnerId, string testCode);
        List<TestDataSearchResponse> SearchTestDetails(TestMasterSearchRequest searchRequest);


    }
}
