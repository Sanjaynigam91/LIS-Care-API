using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.Barcode
{
    public class BarcodeResponse
    {
        public string GeneratedOn {  get; set; }=string.Empty;
        public int SequenceStart {  get; set; }=0;
        public int SequenceEnd { get; set; } =0;
        public string CreatedBy {  get; set; }=string.Empty;
        public int GenerateId {  get; set; }=0;
    }
}
