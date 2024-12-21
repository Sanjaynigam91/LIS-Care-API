using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.TestMaster;
using LISCareUtility;
using Microsoft.AspNetCore.Mvc;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class TestMgmtController : ControllerBase
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

        [HttpPost]
        [Route(ConstantResource.SearchTests)]
        public IActionResult SearchTestInformation(TestMasterSearchRequest searchRequest)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _testMgmt.SearchTestDetails(searchRequest);
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

        [HttpGet]
        [Route(ConstantResource.GetTestByTestCode)]
        public IActionResult GetTestByTestCode(string partnerId, string testCode)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _testMgmt.ViewTestData(partnerId, testCode);
            if (result != null)
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

        [HttpDelete]
        [Route(ConstantResource.DeleteTest)]
        public IActionResult DeleteTestByTestCode(string partnerId, string testCode)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _testMgmt.DeleteTestByTestCode(partnerId, testCode);
            if (result.Status && result.StatusCode == 200)
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

        [HttpGet]
        [Route(ConstantResource.GetReferalRangeByTestCode)]
        public IActionResult GetReferalRangeByTestCode(string partnerId, string testCode)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _testMgmt.GetReferalRangeValue(partnerId, testCode);
            if (result != null)
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


        [HttpGet]
        [Route(ConstantResource.GetSpecialValueByTestCode)]
        public IActionResult GetSpecialValueByTestCode(string partnerId, string testCode)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _testMgmt.GetSpecialValue(partnerId, testCode);
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


        [HttpGet]
        [Route(ConstantResource.GetCenterRateByTestCode)]
        public IActionResult GetCenterRateByTestCode(string partnerId, string testCode)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _testMgmt.GetCenterRates(partnerId, testCode);
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
        [Route(ConstantResource.CreateTest)]
        public IActionResult CreateNewTest(TestMasterRequest testMasterRequest)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _testMgmt.SaveTestDetails(testMasterRequest);
            if (result.Status && result.StatusCode == 200)
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

        [HttpPut]
        [Route(ConstantResource.UpdateTest)]
        public IActionResult UpdateTestInfo(TestMasterRequest testMasterRequest)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = _testMgmt.UpdateTestDetails(testMasterRequest);
            if (result.Status && result.StatusCode == 200)
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
