using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.TestMaster
{
    public class SpecialValueRequest
    {
        public string PartnerId { get; set; } = string.Empty;
        public string OpType { get; set; } = string.Empty;
        public string TestCode { get; set; } = string.Empty;
        public int RecordId { get; set; } = 0;
        public string AllowedValue { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public bool IsAbnormal { get; set; } = false;


    }
}
