using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.TestMaster;
using LISCareUtility;
using Microsoft.AspNetCore.Mvc;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class TestMgmtController: ControllerBase
    {
        private IConfiguration _configuration;
        private new ITestMgmt _testMgmt;

        public TestMgmtController(IConfiguration configuration, ITestMgmt testMgmt)
        {
            _configuration = configuration;
            _testMgmt = testMgmt;
        }

        [HttpGet]
        [Route(ConstantResource.GetTestDepartments)]
        public IActionResult GetTestDepartmentsData(string partnerId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _testMgmt.GetTestDepartmentData(partnerId);
            if (result.Count > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.Success;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = "No Record found!";
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }

        [HttpPost]
        [Route(ConstantResource.GetLabTestInfo)]
        public IActionResult GetLabTestData(TestMasterSearchRequest searchRequest)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _testMgmt.GetTestDetails(searchRequest);
            if (result.Count > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResource.Success;
                responseModel.Data = result;
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = "No Record found!";
                responseModel.Data = result;
                return NotFound(responseModel);
            }

        }
    }
}
