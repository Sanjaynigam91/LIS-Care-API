using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.FrontDesk
{
    public class PatientReceipt
    {
        public string PatientId { get; set; }= string.Empty;
        public string PatientName { get; set; }= string.Empty;
        public string AgeGender { get; set; }=string.Empty;
        public DateTime ReceiptDate { get; set; } = DateTime.Now;
        public string CentreName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public int BillNo { get; set; } = 0;
        public decimal TotalAmount { get; set; }=decimal.MaxValue;
        public decimal Discount { get; set; }= decimal.MaxValue;
        public decimal NetAmount { get; set; } = decimal.MaxValue;
        public decimal PaidAmount { get; set; }= decimal.MaxValue;
        public decimal BalanceAmount { get; set; } = decimal.MaxValue;
        public string AmountInWords { get; set; } = string.Empty;
        public string PartnerId {  get; set; } = string.Empty;
        public string PreparedBy {  get; set; } = string.Empty;
        public string MainLabName {  get; set; } = string.Empty;
        public string ReceiptLogo {  get; set; } = string.Empty;
        public List<ReceiptItem> Items { get; set; } = new();
    }
    public class ReceiptItem
    {
        public string ServiceName { get; set; } = string.Empty;
        public int Qty { get; set; }=int.MaxValue;
        public decimal Rate { get; set; } = decimal.MaxValue;   
        public decimal Gross => Qty * Rate;
    }
}
