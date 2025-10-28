using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.ClientMaster
{
    public class ClientCustomResponse
    {
        public int MappingId { get; set; }
        public string? ClientCode { get; set; }
        public string? ClientName { get; set; }
        public string? TestCode { get; set; }
        public string? TestName { get; set; }
        public int Mrp { get; set; }
        public decimal? CustomRate { get; set; }

    }
}
