using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO
{
    public class PartnerUserRequestModel
    {
        public int UserId {  get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int DepartmentId { get; set; } = 0;
        public int RoleId { get; set; } = 0;
        public string UserStatus { get; set; } = string.Empty;
        public string PartnerId { get; set; } = string.Empty;
        public string UserLogo { get; set; } = string.Empty;
        public string CreatedById { get; set; } = string.Empty;
        public string ModifiedById { get; set; } = string.Empty;



    }
}
