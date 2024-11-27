using LISCare.Interface;
using LISCareDTO;
using LISCareUtility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace LISCareLimited.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration Configuration;
        private new IUser User;

        public LoginController(IConfiguration configuration, IUser user)
        {
            Configuration = configuration;
            User = user;
        }

        [HttpPost]
      //  [EnableCors("AllowSpecificOrigin")]
        [Route(ConstantResource.Login)]
        public IActionResult UserLogin(LoginRequsetModel loginRequset)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = User.UserLogin(loginRequset);
            if (result != null && result.UserId > 0)
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
                responseModel.ResponseMessage = ConstantResource.Failed;
                responseModel.Data = result;
                return NotFound(responseModel);
            }
            
        }
    }
}
