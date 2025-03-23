namespace LISCareDTO.TestMaster
{
    public class TestMasterSearchRequest
    {
        public string partnerId {  get; set; }
        public string testName { get; set; }
        public bool isActive {  get; set; }
        public string deptOrDiscipline {  get; set; }
        public string isProcessedAt { get; set;}
    }
}
