namespace LISCareDTO.TestMaster
{
    public class TestDataSearchResponse
    {
        public string TestCode { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public string SpecimenType { get; set; } = string.Empty;
        public string ReferenceUnits { get; set; } = string.Empty;
        public string Discipline { get; set; } = string.Empty;
        public int MRP { get; set; }
        public int B2CRates { get; set; }
        public decimal LabRates { get; set; }
        public string ReportingStyle { get; set; } = string.Empty;
        public string PrintAs { get; set; } = string.Empty;
        public string AliasName { get; set; } = string.Empty;
        public string ReportTemplateTame { get; set; } = string.Empty;
        public string SubDiscipline { get; set; } = string.Empty;
    }
}
