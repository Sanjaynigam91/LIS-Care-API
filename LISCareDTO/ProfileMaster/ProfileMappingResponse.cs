using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.ProfileMaster
{
    public class ProfileMappingResponse
    {
        public string TestsMappingId { get; set; } = string.Empty;
        public string ProfileCode { get; set; } = string.Empty;
        public string ProfileName { get; set; } = string.Empty;
        public string TestCode { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public string ReportTemplateName { get; set; } = string.Empty;
        public int PrintOrder { get; set; } = 0;
        public string SectionName { get; set; } = string.Empty;
        public string GroupHeader { get; set; } = string.Empty;
        public Boolean CanPrintSeparately { get; set; } = false;


    }
}
