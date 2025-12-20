using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.FrontDesk
{
    public class PatientDetailResponse
    {
        public string Title { get; set; }=string.Empty;
        public string Gender {  get; set; }=string.Empty;
        public string PatientName {  get; set; }=string.Empty;
        public int Age {  get; set; }=int.MaxValue;
        public string AgeType {  get; set; }=string.Empty;
        public string EmailId {  get; set; }=string.Empty;
        public string MobileNumber {  get; set; }=string.Empty;
        public string CenterCode {  get; set; }=string.Empty;
        public string ReferredDoctor {  get; set; }=string.Empty;
        public string PatientType {  get; set; }=string.Empty;
        public bool IsProject { get; set; }=false;
        public int ProjectId {  get; set; }=int.MaxValue;
        public string LabInstruction {  get; set; }=string.Empty;
        public string ReferralNumber {  get; set; }=string.Empty;
        public string SampleCollectedAt {  get; set; }=string.Empty;
        public decimal TotalOriginalAmount {  get; set; }=decimal.MaxValue;
        public decimal BillAmount {  get; set; }=decimal.MaxValue;
        public decimal ReceivedAmount {  get; set; }=decimal.MaxValue;
        public decimal BalanceAmount {  get; set; }=decimal.MaxValue;
        public decimal DiscountAmount {  get; set; }=decimal.MaxValue;
        public bool IsPercentage {  get; set; }=false;
        public string DiscountRemarks {  get; set; }=string.Empty;
        public int PatientSpecimenId {  get; set; }=int.MaxValue;
        public string Barcode {  get; set; }=string.Empty;
        public DateTime CollectionTime {  get; set; }=DateTime.MaxValue;
        public string WOEStatus {  get; set; }=string.Empty;
        public string SpecimenType {  get; set; }=string.Empty;
        public string PaymentType { get; set; } = string.Empty;
        public int VisitId {  get; set; }=int.MaxValue;
        public string DiscountStatus {  get; set; }=string.Empty;
        public string PartnerId {  get; set; }=string.Empty;
        public Guid PatientId { get; set; }=Guid.Empty;
        public string PatientCode {  get; set; }=string.Empty;
        public DateTime RegistrationDate {  get; set; }=DateTime.MaxValue;
    }
}
