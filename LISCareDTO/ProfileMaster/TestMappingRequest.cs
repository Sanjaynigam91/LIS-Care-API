using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.ProfileMaster
{
    public class TestMappingRequest
    {
        public string ProfileCode { get; set; }= string.Empty;
        public string TestCode { get; set; }= string.Empty; 
        public string PartnerId { get; set; }= string.Empty;
        public string SectionName { get; set; }= string.Empty;
        public int PrintOrder { get; set; }= 0;
        public string ReportTemplateName { get; set; }= string.Empty;
        public string GroupHeader { get; set; }= string.Empty;
    }
}
