using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.LISRoles
{
    public class LisPageResponseModel
    {
        public string NavigationId {  get; set; }
        public string PageName {  get; set; }
        public string PageEntity { get; set; }
        public string Criteria {  get; set; }
        public string Status { get; set; }
        public string PartnerId { get; set; }
    }
}
