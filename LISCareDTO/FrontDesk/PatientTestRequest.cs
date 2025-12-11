using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.FrontDesk
{
    public class PatientTestRequest
    {
        public Guid PatientId {  get; set; }
        public string TestCode { get; set; }=string.Empty;
        public bool IsProfile { get; set; } = false;
        public string SpecimenType {  get; set; } = string.Empty;
        public string PartnerId { get; set; } = string.Empty;
        public decimal OriginalPrice { get; set; } = 0;
        public decimal Price { get; set; }

    }
}
