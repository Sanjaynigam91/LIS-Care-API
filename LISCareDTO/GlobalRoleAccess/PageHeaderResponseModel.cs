using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.GlobalRoleAccess
{
    public class PageHeaderResponseModel
    {
        public string NavigationId {  get; set; }
        public string UrlLabel { get; set; }
        public string MessageHeader {  get; set; }
        public string MenuId {  get; set; }
        public string RoleId { get; set; }
        public bool Visibility { get; set; }
        public bool IsReadEnabled {  get; set; }
        public bool IsWriteEnabled { get; set;}
        public bool IsApproveEnabled { get; set; }
        public bool IsSpecialPermssion { get; set; }
        public string PartnerId { get; set; }

    }
}
