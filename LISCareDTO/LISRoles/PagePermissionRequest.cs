using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.LISRoles
{
    public class PagePermissionRequest
    {
        public required string MenuId { get; set; }
        public int RoleId { get; set; }
        public bool Visibility { get; set; }
        public bool IsReadEnabled { get; set; }
        public bool IsWriteEnabled { get; set; }
        public bool IsApproveEnabled { get; set; }
        public bool IsSpecialPermssion { get; set; }
        public required string PartnerId { get; set; }
        
    }
}
