using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.OutLab
{
    public class OutlabRatesRespons
    {
        public int MappingId { get; set; } = 0;
        public string LabCode { get; set; }=string.Empty;
        public string LabName { get; set; }=string.Empty;
        public string TestCode {  get; set; }=string.Empty;
        public string TestName {  get; set; }=string.Empty;
        public decimal Mrp { get; set; } = 0;
        public bool IsProfile { get; set; }=false;
        public decimal AgreedRate {  get; set; } = 0;
        public string CptCodes {  get; set; }=string.Empty;
        public bool IsOutsource {  get; set; }=false;
        public string PartnerId {  get; set; }=string.Empty;
    }
}
