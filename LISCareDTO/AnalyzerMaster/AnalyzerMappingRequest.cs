using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.AnalyzerMaster
{
    public class AnalyzerMappingRequest
    {
        public int MappingId { get; set; } = 0;
        public int AnalyzerId { get; set; } = 0;
        public string AnalyzerTestCode { get; set; } = string.Empty;
        public string LabTestCode { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public string PartnerId { get; set; } = string.Empty;

    }
}
