using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.Projects
{
    public class ProjectTestMappingRequest
    {
        public int MappingId { get; set; } = 0;
        public int ProjectId { get; set; }=0;
        public string PartnerId { get; set; } = string.Empty;
        public string TestCode { get; set; } = string.Empty;
        public decimal BillRate { get; set; } = 0;
    }
}
