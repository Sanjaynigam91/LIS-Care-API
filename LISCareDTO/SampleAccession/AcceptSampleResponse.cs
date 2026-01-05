using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.SampleAccession
{
    public class AcceptSampleResponse
    {
        public int VisitId { get; set; } = 0;
        public int WOEVialNo {  get; set; }= 0;
        public string Message { get; set; } =string.Empty;
    }
}
