using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.ClientMaster
{
    public class ClientRatesRequest
    {
        public required string ClientCode { get; set; }
        public required string PartnerId { get; set; }
        public required string TestCode { get; set; }
        public required string BillRate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
