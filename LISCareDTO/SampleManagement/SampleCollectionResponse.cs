using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.SampleManagement
{
    public class SampleCollectionResponse
    {
        public Guid PatientId { get; set; }= Guid.Empty;
        public string PatientCode { get; set; }= string.Empty;
        public string PatientName { get; set; }= string.Empty;
        public string MobileNumber { get; set; }= string.Empty;
        public string CenterCode { get; set; }= string.Empty;
        public int VisitId { get; set; }= 0;
        public string ReferDoctor { get; set; }= string.Empty;
        public DateTime WorkOrderDate { get; set; }= DateTime.MinValue;
        public string EnteredBy { get; set; }= string.Empty;
    }
}
