
using LISCareBussiness.Interface;
using LISCareDTO.SampleCollectionPlace;
using LISCareDTO.TestMaster;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
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
    }
}
