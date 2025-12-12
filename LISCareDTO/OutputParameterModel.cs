namespace LISCareDTO
{
    public class OutputParameterModel
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }=string.Empty;
        public Guid PatientId { get; set; }
        public string PatientCode { get; set; } = string.Empty;
        public int VisitId { get; set; }
        public string RegistrationStatus { get; set; } = string.Empty;
        public decimal ProfitRate { get; set; }=0;


    }
}

