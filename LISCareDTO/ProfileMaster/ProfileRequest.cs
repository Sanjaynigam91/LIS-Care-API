using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.ProfileMaster
{
    public class ProfileRequest
    {
        public string ProfileCode { get; set; } = string.Empty;
        public string ProfileName { get; set; } = string.Empty;
        public string PartnerId { get; set; } = string.Empty;
        public int PatientRate { get; set; } = 0;
        public int ClientRate { get; set; } = 0;
        public int LabRate { get; set; } = 0;
        public Boolean ProfileStatus { get; set; } = false;
        public string TestShortName { get; set; } = string.Empty;
        public int PrintSequence { get; set; } = 0;
        public string SampleTypes { get; set; } = string.Empty;
        public Boolean IsAvailableForAll { get; set; } = false;
        public string LabTestCode { get; set; } = string.Empty;
        public Boolean IsProfileOutLab { get; set; } = false;
        public string TestApplicable { get; set; } = string.Empty;
        public Boolean IsLMP { get; set; } = false;
        public Boolean IsNABApplicable { get; set; } = false;
        public string ProfileFooter { get; set; } = string.Empty;
    }
}
