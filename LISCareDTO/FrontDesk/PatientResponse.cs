using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.FrontDesk
{
    public class PatientResponse
    {
        public Guid PatientId { get; set; }
        public string PatientCode { get; set; } = string.Empty;
        public string PatientName { get; set; }=string.Empty;
        public string WoeDate {  get; set; } = string.Empty;
        public string CenterCode {  get; set; } = string.Empty;
        public DateTime CreatedOn {  get; set; }=DateTime.Now;
        public string Barcode {  get; set; } = string.Empty;
        public decimal TotalOriginalAmount {  get; set; } = 0;
        public decimal BillAmount { get; set; } = 0;
        public decimal ReceivedAmount {  get; set; } = 0;
        public decimal BalanceAmount {  get; set; } = 0;
        public int VisitId { get; set; }
        public string RegistrationStatus { get; set; } = string.Empty;
        public string ReferredDoctor {  get; set; } = string.Empty;
        public string PartnerId {  get; set; } = string.Empty;
        public string DiscountStatus {  get; set; } = string.Empty;
        public string TestRequested {  get; set; } = string.Empty;
        public string PatientTestType {  get; set; } = string.Empty;
        public bool IsProject {  get; set; }=false;
        public string InvoiceReceiptNo {  get; set; } = string.Empty;
        public DateTime RegisteredOn {  get; set; } = DateTime.Now;
        public string ReferredLab {  get; set; } = string.Empty;
        public string CenterrName {  get; set; } = string.Empty;
        public string SpecimenType { get; set; } = string.Empty;
        public string PartnerType {  get; set; } = string.Empty;
        public string ReferredBy {  get; set; } = string.Empty;

    }
}
