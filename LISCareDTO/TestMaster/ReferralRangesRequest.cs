

namespace LISCareDTO.TestMaster
{
    public class ReferralRangesRequest
    {
        public string OpType {  get; set; }=string.Empty;
        public int ReferralId {  get; set; }=0;
        public string TestCode {  get; set; }=string.Empty;
        public decimal LowRange {  get; set; }=0;
        public decimal HighRange {  get; set; }=0;
        public string NormalRange {  get; set; }=string.Empty;
        public int AgeFrom {  get; set; }=0;
        public int AgeTo {  get; set; }=0;
        public string Gender {  get; set; }=string.Empty;
        public bool IsPregnant {  get; set; }=false;
        public decimal LowCriticalValue {  get; set; }=0;
        public string PartnerId {  get; set; }=string.Empty;
        public string UpdatedBy {  get; set; }=string.Empty;
        public decimal HighCriticalValue {  get; set; }=0;
    }
}
