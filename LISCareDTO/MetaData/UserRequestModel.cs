using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.MetaData
{
    public class UserRequestModel
    {
        public string PartnerId {  get; set; }
        public string UserStatus {  get; set; }=string.Empty;
        public string RoleType { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
