using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.SampleManagement
{
    public class SampleRequest
    {
        public string Barcode { get; set; }=string.Empty;
        public DateTime CollectionTime {  get; set; }=DateTime.Now;
        public string CollectedBy {  get; set; }=string.Empty;
        public string SpecimenType {  get; set; }=string.Empty;
        public Guid PatientId {  get; set; }=Guid.Empty;
    }
}
