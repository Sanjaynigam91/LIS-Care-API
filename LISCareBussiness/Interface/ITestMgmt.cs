using LISCareDTO.TestMaster;

namespace LISCareBussiness.Interface
{
    public interface ITestMgmt
    {
        List<TestDataSearchResponse> GetTestDetails(TestMasterSearchRequest searchRequest);
        List<TestDepartmentResponse> GetTestDepartmentData(string partnerId);
    }
}
