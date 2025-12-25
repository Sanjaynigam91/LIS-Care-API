namespace LISCareDTO
{
    public class LoginResponseModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string RoleType { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public bool IsMobileVerified { get; set; }
        public bool IsEmailVerified { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expires_in {  get; set; }
        public string UserStatus { get; set; } = string.Empty;
        public string PartnerId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }=string.Empty;
        public int DepartmentId { get;set; }
        public string UserLogo { get; set; } = string.Empty;
        public string CenterCode { get; set; }= string.Empty;




    }
}

