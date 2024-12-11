namespace LISCareDTO.TestMaster
{
    public class ReferalRangeResponse
    {
        public int referralId { get; set; }
        public string partnerId { get; set; }
        public string testCode { get; set; }
        public string gender { get; set; }
        public decimal lowRange { get; set; }
        public decimal highRange { get; set; }
        public string normalRange { get; set; }
        public int ageFrom { get; set; }
        public int ageTo { get; set; }
        public bool isPregnant { get; set; }
        public decimal lowCriticalValue { get; set; }
        public string ageUnits { get; set; }
        public decimal highCriticalValue { get; set; }
        public float labTest { get; set; }


    }
}