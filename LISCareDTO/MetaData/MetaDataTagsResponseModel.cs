using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.MetaData
{
    public class MetaDataTagsResponseModel
    {
        public int RecordId { get; set; }
        public string PartnerId { get; set; }
        public string? Category { get; set; }
        public string? ItemType { get; set; }
        public string? ItemDescription { get; set; }

    }
}
