using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.SampleAccession
{
    public class PendingAccessionResponse
    {
        public DateTime RegisterDate { get; set; }
        public DateTime WorkOrderDate { get; set; }
        public string PatientName{ get; set; }=string.Empty;
        public string ReferredBy { get; set; } = string.Empty;
        public string SampleStatus { get; set; } = string.Empty;
        public string RejectedDetails {  get; set; } = string.Empty;
        public int VisitId {  get; set; }
        public int ProjectId { get; set; }
        public string PartnerId { get; set; } = string.Empty;
        public string CenterName {  get; set; } = string.Empty;
        public string CenterCode {  get; set; } = string.Empty;
        public string Barcode {  get; set; } = string.Empty;
        public string SampleType { get; set; } = string.Empty;
        public string TestRequested {  get; set; } = string.Empty;
    }
}
