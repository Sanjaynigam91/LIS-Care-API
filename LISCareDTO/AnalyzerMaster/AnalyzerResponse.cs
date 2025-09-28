using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.AnalyzerMaster
{
    public class AnalyzerResponse
    {
        public int AnalyzerId { get; set; }
        public string AnalyzerName { get; set; }= string.Empty;
        public string AnalyzerCode { get; set; }= string.Empty;
        public Boolean AnalyzerStatus { get; set; }=false;
        public string SupplierCode { get; set; }= string.Empty; 
        public decimal PurchaseValue { get; set; }=0;
        public DateTime WarrantyEndDate { get; set; }=DateTime.Now;
        public string EngineerContactNo { get; set; }= string.Empty;
        public string AssetCode { get; set; }= string.Empty;
        public required string PartnerId { get; set; }

    }
}
