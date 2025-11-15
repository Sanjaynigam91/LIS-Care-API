using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.Projects
{
    public class ProjectSpecialRateResponse
    {
        public int MappingId { get; set; } = 0;
        public int ProjectId { get; set; } = 0;
        public string? ProjectName { get; set; }
        public string? Testcode { get; set; }
        public string? TestName { get; set; }
        public decimal MRP { get; set; } = 0;
        public bool IsProfile { get; set; } = false;
        public decimal SpecialRate { get; set; } = 0;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool IsCovered { get; set; } = false;
        public bool IsApprovalMandatory { get; set; } = false;

    }
}
