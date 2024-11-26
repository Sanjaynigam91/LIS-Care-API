namespace LISCareDTO
{
    public class UserResponseModel
    {
        public string UserCode {  get; set; } 
        public string UserName { get; set; } = string.Empty;
        public string UserCategory {  get; set; } = string.Empty;
        public string AccountStatus { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string AssociatePartnerCode { get; set; } = string.Empty;

    }
}
