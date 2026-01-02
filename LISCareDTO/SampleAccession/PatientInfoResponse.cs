using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.SampleAccession
{
    public class PatientInfoResponse
    {
        public int SampleId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string ReferDoctor { get; set; } = string.Empty;
        public string ReferLab { get; set; } = string.Empty;
        public string CenterCode { get; set; } = string.Empty;
        public string WorkOrderStatus { get; set; } = string.Empty;
        public string PatientCode { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string PaymentType { get; set; } = string.Empty;
        public string SampleType { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public string AllowAccession { get; set; } = string.Empty;          
        public string PartnerId { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
    }
}
