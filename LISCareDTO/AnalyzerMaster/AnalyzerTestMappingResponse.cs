using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.AnalyzerMaster
{
    public class AnalyzerTestMappingResponse
    {
        public int MappingId { get; set; } = 0;
        public string AnalyzerTestCode { get; set; }=string.Empty;
        public int AnalyzerId { get; set; }=int.MaxValue;
        public string LabTestCode { get; set; }=string.Empty ;
        public bool Status { get; set; }=false;
        public string PartnerId { get; set; } = string.Empty;
        public bool IsProfileCode { get; set; }=false;
        public string SampleType { get; set; }= string.Empty;
    }
}
