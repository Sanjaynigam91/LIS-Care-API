using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.SampleAccession
{
    public class SampleAccessionTestResponse
    {
        public string TestCode { get; set; }=string.Empty;
        public string TestName {  get; set; }=string.Empty;
        public string SampleType {  get; set; }=string.Empty;
        public DateTime CollectionTime {  get; set; }=DateTime.Now;
        public int SampleId { get; set; }=0;
        public string WorkOrderStatus {  get; set; }=string.Empty;
        public string RejectionRemarks {  get; set; }=string.Empty;
        public string RejectedBy {  get; set; }=string.Empty;
        public DateTime RejectedDate {  get; set; }=DateTime.Now;
        public Guid RequestId {  get; set; }=Guid.Empty;
        public bool IsRejected {  get; set; }=false;
        public string PatientName {  get; set; }=string.Empty;
        public DateTime CreatedOn {  get; set; }=DateTime.Now;
        public string Barcode {  get; set; }=string.Empty;
        public string CancelRejectionRemark {  get; set; }=string.Empty;
        public bool IsApprovalMandatory {  get; set; }=false;

    }
}
