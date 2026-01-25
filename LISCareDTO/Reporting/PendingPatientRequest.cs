namespace LISCareDTO.Reporting
{
    public class PendingPatientRequest
    {
        public string PartnrerId { get; set; }=string.Empty;
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public string Barcode { get; set; }=string.Empty;
        public string Department { get; set; }=string.Empty;
        public string PatientName { get; set; }=string.Empty;
        public string CenterCode {  get; set; }=string.Empty;
        public string ReportStatus {  get; set; }=string.Empty;

    }
}
