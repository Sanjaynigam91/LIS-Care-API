using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.AnalyzerMaster
{
    public class AnalyzerRequest
    {
        public int AnalyzerId { get; set; } = 0;
        public required string AnalyzerName { get; set; }= string.Empty;
        public string AnalyzerShortCode { get; set; }= string.Empty;
        public string Status { get; set; }= string.Empty;
        public string SupplierCode { get; set; }= string.Empty;
        public decimal ? PurchasedValue { get; set; } = null;
        public DateTime? WarrantyEndDate { get; set; } = null;
        public string EngineerContactNo { get; set; } = string.Empty;
        public string AssetCode { get; set; }= string.Empty;
        public required string PartnerId { get; set; }= string.Empty;



    }
}
