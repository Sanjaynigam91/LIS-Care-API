using LISCareDTO;
using LISCareDTO.TestMaster;

namespace LISCareBussiness.Interface
{
    public interface ITestMgmt
    {
        List<TestDataSearchResponse> GetTestDetails(TestMasterSearchRequest searchRequest);
        List<TestDepartmentResponse> GetTestDepartmentData(string partnerId);
        List<TestDataResponse> ViewTestData(string partnerId, string testCode);
        // This interface used delete Users
        APIResponseModel<object> DeleteTestByTestCode(string partnerId, string testCode);
    }
}
