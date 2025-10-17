using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.AnalyzerMaster
{
    public class AnalyzerMappingResponse
    {
        public required int MappingId { get; set; }
        public string AnalyzerTestCode { get; set; }=string.Empty;
        public int AnalyzerId { get; set; }=int.MaxValue;
        public string LabTestCode { get; set; }=string.Empty;
        public bool Status { get; set; }=false;
        public bool IsProfileCode { get; set; }=false ;
        public string SampleType { get; set; }= string.Empty;
        public string PartnerId { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
    }
}
