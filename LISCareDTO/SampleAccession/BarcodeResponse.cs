namespace LISCareDTO.SampleAccession
{
    public class BarcodeResponse
    {
        public string Barcode { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public DateTime RegisteredDate { get; set; } = DateTime.Now;
        public string Age { get; set; } = string.Empty;
        public string SampleType { get; set; } = string.Empty;
        public string TestShortName { get; set; } = string.Empty;
    }
}
