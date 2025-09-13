
using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.SampleCollectionPlace;
using LISCareDTO.TestMaster;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using LISCareReposotiory.Implementation;
using Microsoft.Extensions.Configuration;

namespace LISCareBussiness.Implementation
{
    public class TestMgmtBAL : ITestMgmt
    {
        private readonly IConfiguration _configuration;
        private readonly ITestMgmtRepository _testMgmtRepository;

        public TestMgmtBAL(IConfiguration configuration, ITestMgmtRepository testMgmtRepository)
        {
            _configuration = configuration;
            _testMgmtRepository = testMgmtRepository;
        }

        public APIResponseModel<object> DeleteTestByTestCode(string partnerId, string testCode)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = _testMgmtRepository.DeleteTestByTestCode(partnerId, testCode);
            }
            catch { throw; }
            return response;
        }

        public List<TestDepartmentResponse> GetTestDepartmentData(string partnerId)
        {
            var response = new List<TestDepartmentResponse>();
            try
            {
                response = _testMgmtRepository.GetTestDepartmentData(partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<TestDataSearchResponse> GetTestDetails(TestMasterSearchRequest searchRequest)
        {
            var response = new List<TestDataSearchResponse>();
            try
            {
                response = _testMgmtRepository.GetTestDetails(searchRequest);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<TestDataSearchResponse> SearchTestDetails(TestMasterSearchRequest searchRequest)
        {
            var response = new List<TestDataSearchResponse>();
            try
            {
                response = _testMgmtRepository.SearchTestDetails(searchRequest);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public TestDataResponse ViewTestData(string partnerId, string testCode)
        {
            var response = new TestDataResponse();
            try
            {
                response = _testMgmtRepository.ViewTestData(partnerId, testCode);
            }
            catch
            {
                throw;
            }
            return response;
        }
        public async Task<ReferalRangeResponse> GetReferalRangeValueAsync(string partnerId, string testCode)
        {
            if (string.IsNullOrWhiteSpace(partnerId))
                throw new ArgumentException("PartnerId cannot be null or empty", nameof(partnerId));

            if (string.IsNullOrWhiteSpace(testCode))
                throw new ArgumentException("TestCode cannot be null or empty", nameof(testCode));

            try
            {
                // Assuming repository also has async version
                return await _testMgmtRepository.GetReferalRangeValueAsync(partnerId, testCode);
            }
            catch (Exception ex)
            {
                // log ex here if needed
                throw; // Rethrow keeps stack trace intact
            }
        }


        public List<SpecialValueResponse> GetSpecialValue(string partnerId, string testCode)
        {
            var response = new List<SpecialValueResponse>();
            try
            {
                response = _testMgmtRepository.GetSpecialValue(partnerId, testCode);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<CenterRateResponse> GetCenterRates(string partnerId, string testCode)
        {
            var response = new List<CenterRateResponse>();
            try
            {
                response = _testMgmtRepository.GetCenterRates(partnerId, testCode);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public async Task<APIResponseModel<object>> SaveTestDetailsAsync(TestMasterRequest testMasterRequest)
        {
            try
            {
                return await _testMgmtRepository.SaveTestDetailsAsync(testMasterRequest);
            }
            catch
            {
                throw;
            }
        }


        public async Task<APIResponseModel<object>> UpdateTestDetails(TestMasterRequest testMasterRequest)
        {
            try
            {
                return await _testMgmtRepository.UpdateTestDetails(testMasterRequest);
            }
            catch
            {
                throw;
            }
        }

        public async Task<APIResponseModel<string>> SaveUpdateReferralRanges(ReferralRangesRequest referralRangesRequest)
        {
            var response = new APIResponseModel<string>();
            try
            {
                response = await _testMgmtRepository.SaveUpdateReferralRanges(referralRangesRequest);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public async Task<APIResponseModel<string>> DeleteReferralRanges(int referralId)
        {
            var response = new APIResponseModel<string>();
            try
            {
                response = await _testMgmtRepository.DeleteReferralRanges(referralId);
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
