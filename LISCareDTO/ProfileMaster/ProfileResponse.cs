using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.ProfileMaster
{
    public class ProfileResponse
    {
        public string PartnerId { get; set; }=string.Empty;
        public string ProfileCode { get; set; }=string.Empty;
        public string ProfileName { get; set; }=string.Empty;
        public int MRP { get; set; }=0;
        public bool ProfileStatus { get; set; }=false;
        public int B2CRates { get; set; }=0;
        public string SampleTypes { get; set; }=string.Empty;
        public int Labrates { get; set; } = 0;
        public int TatHrs { get; set; } = 0;
        public string CptCodes { get; set; }=string.Empty;  
        public int PrintSequence { get; set; }=0;
        public bool IsRestricted { get; set; }=false;
        public int SubProfilesCount { get; set; }=0;
        public int RecordId { get; set; }=0;
        public string NormalRangeFooter { get; set; }=string.Empty;
        public string TestShortName { get; set; }=string.Empty;
        public int ProfileProfitRate { get; set; }=0;
        public string LabTestCodes { get; set; }=string.Empty;
        public bool IsProfileOutLab { get; set; }=false;
        public string TestApplicable {get; set; }=string.Empty;
        public bool IsLMP { get; set; }=false;
        public bool IsNABLApplicable { get; set; }=false;


    }
}
