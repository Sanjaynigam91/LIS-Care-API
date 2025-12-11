using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.FrontDesk
{
    public class PatientSelectedTestResponse
    {
        public string TestCode {  get; set; }=string.Empty;
        public string TestName {  get; set; }=string.Empty;
        public string ReportingLeadTime {  get; set; }=string.Empty;
        public decimal MRP { get; set; } = 0;
        public bool IsProfile { get; set; } = false;
        public Guid RequestId {  get; set; }=Guid.Empty;
        public string SpecimenType {  get; set; }=string.Empty;
        public int PatientSpecimenId {  get; set; }=0;
        public string RateType {  get; set; }=string.Empty;
        public decimal Price {  get; set; } = 0;
        public string CenterCode {  get; set; }=string.Empty;
        public bool ImportStatus { get; set; }=false;
        public bool IsRejected { get; set; }=false;
    }
}
