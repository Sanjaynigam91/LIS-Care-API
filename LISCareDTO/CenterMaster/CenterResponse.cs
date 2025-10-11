using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.CenterMaster
{
    public class CenterResponse
    {
        public string CenterCode { get; set; }= string.Empty;
        public string CenterName { get; set; }= string.Empty;
        public string CenterInchargeName { get; set; }= string.Empty;
        public string SalesIncharge { get; set; }= string.Empty;
        public string CenterAddress { get; set; } = string.Empty;
        public string Pincode { get; set; } = string.Empty;
        public string MobileNumber { get; set; }= string.Empty;
        public string AlternateContactNo { get; set; }= string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string RateType { get; set; }= string.Empty;
        public bool CenterStatus { get; set; } = false;
        public string IntroducedBy { get; set; }= string.Empty;
        public int CreditLimit { get; set; } = 0;
        public bool IsAutoBarcode { get; set; } = false;
        public string City { get; set; }= string.Empty;
        public string PartnerId { get; set; }= string.Empty;

    }
}
