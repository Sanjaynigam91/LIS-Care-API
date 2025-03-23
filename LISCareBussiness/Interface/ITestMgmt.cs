using LISCareDTO;
using LISCareDTO.TestMaster;

namespace LISCareBussiness.Interface
{
    public interface ITestMgmt
    {
        List<TestDataSearchResponse> GetTestDetails(TestMasterSearchRequest searchRequest);
        List<TestDepartmentResponse> GetTestDepartmentData(string partnerId);
        TestDataResponse ViewTestData(string partnerId, string testCode);
        // This interface used delete Users
        APIResponseModel<object> DeleteTestByTestCode(string partnerId, string testCode);
        List<TestDataSearchResponse> SearchTestDetails(TestMasterSearchRequest searchRequest);
        ReferalRangeResponse GetReferalRangeValue(string partnerId, string testCode);
        List<SpecialValueResponse> GetSpecialValue(string partnerId, string testCode);
        List<CenterRateResponse> GetCenterRates(string partnerId, string testCode);
        APIResponseModel<object> SaveTestDetails(TestMasterRequest testMasterRequest);
        APIResponseModel<object> UpdateTestDetails(TestMasterRequest testMasterRequest);
        Task<APIResponseModel<string>> SaveUpdateReferralRanges(ReferralRangesRequest referralRangesRequest);

    }
}
