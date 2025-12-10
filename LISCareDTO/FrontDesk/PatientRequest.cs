using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.FrontDesk
{
    public class PatientRequest
    {
        public bool IsAddPatient { get; set; } = false;
        public Guid? PatientId { get; set; }
        public string PatientCode { get; set; }=string.Empty;
        public string Title { get; set; }=string.Empty;
        public string Gender { get; set; }=string.Empty;
        public string PatientName { get; set; }=string.Empty;
        public int Age { get; set; } = 0;
        public string AgeType { get; set; }=string.Empty;
        public string EmailId { get; set; }=string.Empty;
        public string MobileNumber { get; set; }=string.Empty;
        public string CenterCode { get; set; }=string.Empty;
        public string ReferredDoctor { get; set; }=string.Empty;
        public string ReferredLab { get; set; }=string.Empty;
        public bool IsProject { get; set; } = false;
        public int ProjectId { get; set; } = 0;
        public string LabInstruction { get; set; }=string.Empty;
        public string ReferalNumber { get; set; }=string.Empty;
        public string SampleCollectedAt { get; set; }=string.Empty;
        public bool IsPregnant { get; set; } = false;
        public int PregnancyWeeks { get; set; } = 0;
        public string PaymentType { get; set; }=string.Empty;
        public decimal TotalOriginalAmount { get; set; } = 0;
        public decimal BillAmount { get; set; } = 0;
        public decimal ReceivedAmount { get; set; } = 0;
        public decimal BalanceAmount { get; set; } = 0;
        public decimal DiscountAmount { get; set; } = 0;
        public decimal AgreedRatesBilling { get; set; } = 0;
        public string DiscountStatus { get; set; }=string.Empty;
        public string DiscountRemarks { get; set; }=string.Empty;
        public string PatientType { get; set; }=string.Empty;
        public string EnteredBy { get; set; }=string.Empty;
        public string Nationality { get; set; }=string.Empty;
        public string ClinicalHistory { get; set; }=string.Empty;
        public string ClinicalRemarks { get; set; }=string.Empty;
        public bool IsPercentage { get; set; } = false;
        public string InvoiceReceiptNo { get; set; }=string.Empty;
        public bool IsReportUploaded { get; set; } = false;
        public string PartnerId { get; set; }=string.Empty;
        public string CreatedBy { get; set; }=string.Empty;
        public string UpdatedBy { get; set; }=string.Empty;
        
    }
}
