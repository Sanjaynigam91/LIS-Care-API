namespace LISCareDTO.SampleAccession
{
    public class AcceptSampleRequest
    {
        public DateTime WoeDate { get; set; } = DateTime.Now; 
        public string Barcode { get; set; }=string.Empty;
        public int PatientSpecimenId {  get; set; }=int.MaxValue;
        public string PatientCode {  get; set; }=string.Empty;
        public string SpecimenType {  get; set; }=string.Empty;
        public string CreatedBy { get; set; }= string.Empty;
        public string PartnerId {  get; set; }=string.Empty;
        public int VisitId {  get; set; }=int.MaxValue;

    }
}
