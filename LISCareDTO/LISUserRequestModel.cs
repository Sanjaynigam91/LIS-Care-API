using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO
{
    public class LISUserRequestModel
    {

        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string AccountStatus { get; set; }
        public string Password { get; set; }
        public string AssignedRole { get; set; }
        public int AccountId { get; set; }
        public string UserClassificationId { get; set; }
        public string? AuthenticationMode { get; set; }
        public string Department { get; set; }
        public string CreatedBy { get; set; }
    }
}
