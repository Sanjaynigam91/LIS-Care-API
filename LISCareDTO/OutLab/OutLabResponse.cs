using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.OutLab
{
    public class OutLabResponse
    {
        public string LabCode { get; set; }=string.Empty;
        public string LabName { get; set; } = string.Empty;
        public string City {  get; set; } = string.Empty;
        public string MobileNumber {  get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public string ContactPerson {  get; set; } = string.Empty;
        public bool LabStatus { get; set; } = false;
        public string PartnerId { get; set; } = string.Empty;
        public string IntroducedBy {  get; set; } = string.Empty;
    }
}
