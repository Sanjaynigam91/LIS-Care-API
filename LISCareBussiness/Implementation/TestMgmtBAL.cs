
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
                response = _testMgmtRepository.ViewTestData(partnerId,testCode);
            }
            catch
            {
                throw;
            }
            return response;
        }
        public ReferalRangeResponse GetReferalRangeValue(string partnerId, string testCode)
        {
            var response = new ReferalRangeResponse();
            try
            {
                response = _testMgmtRepository.GetReferalRangeValue(partnerId, testCode);
            }
            catch
            {
                throw;
            }
            return response;
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
    }
}
