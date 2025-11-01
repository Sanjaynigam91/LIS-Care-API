using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.Barcode
{
    public class BarcodeRequest
    {
        public int SequenceStart { get; set; } = 0;
        public int SequenceEnd { get;set; } = 0;
        public string PartnerId { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
