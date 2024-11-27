using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.MetaData
{
    public class MetaTagModel
    {
        public int TagId {  get; set; }
        public required string TagCode { get; set; }
        public required string TagDescription { get; set; }
        public required string TagStatus { get; set; }
        public string PartnerId { get; set; }
    }
}
