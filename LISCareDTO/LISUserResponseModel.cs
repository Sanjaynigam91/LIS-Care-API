using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO
{
    public class LISUserResponseModel
    {
     
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UserType { get; set; }
        public string AccountStatus { get; set; }
        public string Password { get; set; }
        public string Default_Url { get; set; }
        public string AssignedRole { get; set; }
        public DateTime LastLogintime { get; set; }
        public DateTime CurrentLogintime { get; set; }
        public int AccountId { get; set; }
        public string LastName { get; set; }
        public string UserClassificationId { get; set; }
        public string AuthenticationMode { get; set; }
        public string Department { get; set; }
        public bool IsBlocked { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
