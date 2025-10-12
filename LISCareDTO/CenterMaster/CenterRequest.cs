using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.CenterMaster
{
    public class CenterRequest
    {
        public required string CenterCode { get; set; }
        public required string CenterName { get; set; }
        public required string CenterInchargeName { get; set; }
        public required string SalesIncharge { get; set; }
        public required string CenterAddress { get; set; }
        public required string Pincode { get; set; }
        public required string MobileNumber { get; set; }
        public string? AlternateContactNo { get; set; }
        public required string EmailId { get; set; }
        public string RateType { get; set; } = string.Empty;
        public bool CenterStatus { get; set; }=false;
        public string? IntroducedBy { get; set; }
        public int? CreditLimit { get; set; }
        public required bool IsAutoBarcode { get; set; }
        public required string PartnerId { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
