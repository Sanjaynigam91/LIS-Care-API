using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.FrontDesk
{
    public class TestSampleResponse
    {
        public string? PartnerId { get; set; }
        public string ? SampleCode { get; set; }
        public string? SampleName { get; set; }
        public string? ReportingLeadTime { get; set; }=string.Empty;
        public decimal BillRate { get; set; }=0;
        public bool IsProfile { get; set; }=false;
        public string?SampleType { get; set; }
        public decimal MRP { get; set; }=0;
        public string ? CptCodes { get; set; }
        public decimal LabRate { get; set; }=0;
        public bool IsRestricted { get; set; }=false;
        public string? PrintAs { get; set; }
        public string ? Disipline { get; set; }
        public string ? CenterCode { get; set; }
        public int ProjectId { get; set; }=0;
        public string? IsProject { get; set; }
        public string? IsCustomRate { get; set; }
        public bool IsLMP { get; set; }=false;

    }
}
