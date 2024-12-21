namespace LISCareDTO.TestMaster
{
    public class TestMasterRequest
    {
        public string partnerId { get; set; }
        public string testCode { get; set; }
        public string testName { get; set; }
        public string department {  get; set; }
        public string subDepartment {  get; set; }
        public string methodology { get; set; }
        public string specimenType { get; set; }
        public string referenceUnits { get; set; }
        public string reportingStyle { get; set; }
        public string reportTemplateName { get; set; }
        public int reportingDecimals { get; set; }
        public bool isOutlab { get; set; }
        public int printSequence { get; set; }
        public string isReserved { get; set; }
        public string testShortName { get; set; }
        public int patientRate { get; set; }
        public int clientRate { get; set; }
        public int labRate { get; set; }
        public bool status { get; set; }
        public string analyzerName {  get; set; }
        public bool isAutomated {  get; set; }
        public bool isCalculated {  get; set; }
        public string labTestCode { get; set; }
        public string testApplicable { get; set; }
        public bool isLMP { get; set; }
        public bool isNABLApplicable { get; set; }
        public string referalRangeComments { get; set; }
        public string updatedBy { get; set; } 
        
    }
}
