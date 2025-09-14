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
        Task<ReferalRangeResponse> GetReferalRangeValueAsync(string partnerId, string testCode);
        List<SpecialValueResponse> GetSpecialValue(string partnerId, string testCode);
        List<CenterRateResponse> GetCenterRates(string partnerId, string testCode);
        Task<APIResponseModel<object>> SaveTestDetailsAsync(TestMasterRequest testMasterRequest);
        Task<APIResponseModel<object>> UpdateTestDetails(TestMasterRequest testMasterRequest);
        Task<APIResponseModel<string>> SaveUpdateReferralRanges(ReferralRangesRequest referralRangesRequest);
        Task<APIResponseModel<string>> DeleteReferralRanges(int referralId);
        Task<APIResponseModel<string>> SaveUpdateSepecialValue(SpecialValueRequest specialValueRequest);
        Task<APIResponseModel<string>> DeleteSpecialValue(int recordId, string partnerId);

    }
}
