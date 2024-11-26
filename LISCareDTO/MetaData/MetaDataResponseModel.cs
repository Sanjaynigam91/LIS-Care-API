using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.MetaData
{
    public class MetaDataResponseModel
    {
        public int TagId { get; set; }
        public string PartnerId { get; set; }
        public string TagCode { get; set; }
        public string TagDescription { get; set; }
        public string MetaStatus { get; set; }
    }
}
