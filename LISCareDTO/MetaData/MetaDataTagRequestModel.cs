namespace LISCareDTO.MetaData
{
    public class MetaDataTagRequestModel
    {
        public required string TagCode {  get; set; }
        public required string TagDescription { get; set;}
        public required string TagStatus { get; set; }
        public string PartnerId {  get; set; }

    }
}
