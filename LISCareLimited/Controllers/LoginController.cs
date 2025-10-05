using LISCare.Interface;
using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.AnalyzerMaster;
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
        public async Task<IActionResult> UserLogin(LoginRequsetModel loginRequset)
        {
            var result = await User.UserLogin(loginRequset);
            return StatusCode(result.StatusCode, result);
        }
    }
}
