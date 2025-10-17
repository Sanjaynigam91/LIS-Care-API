using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.CenterMaster
{
    public class CentreCustomRateResponse
    {
        public int MappingId { get; set; }
        public string? CenterCode { get; set; }
        public string? CenterName { get; set; }
        public string? TestCode { get; set; }
        public string? TestName { get; set; }
        public int Mrp { get; set; }
        public decimal? CustomRate { get; set; }
    }
}
