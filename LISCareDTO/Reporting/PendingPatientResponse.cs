namespace LISCareDTO.Reporting
{
    public class PendingPatientResponse
    {
        public string WorkOrderDate { get; set; } = string.Empty;
        public string CenterCode { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string PatientCode { get; set; } = string.Empty;
        public string Departments { get; set; } = string.Empty;
        public string BarcodeIds { get; set; } = string.Empty;
        public string TestProfiles { get; set; } = string.Empty;
        public DateTime NewWorkOrderDate { get; set; }
        public string ReferredBy { get; set; } = string.Empty;
        public int ClinicFileNumber { get; set; }
        public int VisitId { get; set; }
        public string CenterName { get; set; } = string.Empty;
        public string SampleType { get; set; } = string.Empty;
    }
}
