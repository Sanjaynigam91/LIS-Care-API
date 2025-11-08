using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.Projects
{
    public class ProjectResponse
    {
        public int ProjectId { get; set; } = 0;
        public string ProjectName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AlternateEmail { get; set; } = string.Empty;
        public string ProjectAddress { get; set; } = string.Empty;
        public string ReferedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public bool ProjectStatus { get; set; } = false;
        public string PartnerId { get; set; } = string.Empty;
        public DateTime ValidFrom { get; set; } = DateTime.Now;
        public DateTime ValidTo { get; set; } = DateTime.Now;
        public string RateType { get; set; } = string.Empty;
        public string ReceiptPrefix { get; set; } = string.Empty;
        public int PatientCount { get; set; } = 0;
        public DateTime PatientCountLastUpdatedOn { get; set; } = DateTime.Now;
        public string ExpiryPeriods {  get; set; } = string.Empty;
    }
}
