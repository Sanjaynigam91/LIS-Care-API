using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.TestMaster;
using LISCareUtility;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                responseModel.Data = result ?? new object(); 
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
        public async Task<IActionResult> GetReferalRangeByTestCode(string partnerId, string testCode)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = await _testMgmt.GetReferalRangeValueAsync(partnerId, testCode);
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
                responseModel.Data = result ?? new object(); // Fix: never assign null
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
        public async Task<IActionResult> CreateNewTestAsync(TestMasterRequest testMasterRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                   .Where(ms => ms.Value?.Errors != null && ms.Value.Errors.Count > 0)
                   .Select(ms => new
                   {
                       Field = ms.Key,
                       Errors = ms.Value?.Errors?.Select(e => e.ErrorMessage) ?? Enumerable.Empty<string>()
                   });
                return BadRequest(ModelState);

            }
            var result = await _testMgmt.SaveTestDetailsAsync(testMasterRequest);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Route(ConstantResource.UpdateTest)]
        public async Task<IActionResult> UpdateTestInfo([FromBody] TestMasterRequest testMasterRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                   .Where(ms => ms.Value?.Errors != null && ms.Value.Errors.Count > 0)
                   .Select(ms => new
                   {
                       Field = ms.Key,
                       Errors = ms.Value?.Errors?.Select(e => e.ErrorMessage) ?? Enumerable.Empty<string>()
                   });
                return BadRequest(ModelState);
     
            }

            var result = await _testMgmt.UpdateTestDetails(testMasterRequest);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost]
        [Route(ConstantResource.SaveUpdateReferralRanges)]
        public async Task<IActionResult> SaveUpdateReferralRangesData(ReferralRangesRequest referralRangesRequest)
        {
            try
            {
                var result = await _testMgmt.SaveUpdateReferralRanges(referralRangesRequest);
                if (result.Status && result.StatusCode == (int)HttpStatusCode.OK)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to Save or Update Referral Ranges {ex.Message}");
            }

        }

        [HttpDelete]
        [Route(ConstantResource.DeleteReferralRanges)]
        public async Task<IActionResult> DeleteReferralRangesById(int referralId)
        {
            try
            {
                var result = await _testMgmt.DeleteReferralRanges(referralId);
                if (result.Status && result.StatusCode == (int)HttpStatusCode.OK)
                {
                    return Ok(result);
                }
                else if (!result.Status && result.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    return NotFound(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to Delete Referral Ranges {ex.Message}");
            }

        }

        [HttpPost]
        [Route(ConstantResource.SaveUpdateSpecialValues)]
        public async Task<IActionResult> SaveUpdateSpecialValues(SpecialValueRequest specialValueRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                   .Where(ms => ms.Value?.Errors != null && ms.Value.Errors.Count > 0)
                   .Select(ms => new
                   {
                       Field = ms.Key,
                       Errors = ms.Value?.Errors?.Select(e => e.ErrorMessage) ?? Enumerable.Empty<string>()
                   });
                return BadRequest(ModelState);

            }
            var result = await _testMgmt.SaveUpdateSepecialValue(specialValueRequest);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete]
        [Route(ConstantResource.DeleteSpecialValue)]
        public async Task<IActionResult> DeleteSpecialValue(int recordId, string partnerId)
        {
            if (recordId > 0 && !string.IsNullOrEmpty(partnerId))
            {
                var result = await _testMgmt.DeleteSpecialValue(recordId, partnerId);
                return StatusCode(result.StatusCode, result);
            }

            return BadRequest("Invalid recordId or partnerId.");
        }
    }
}
