namespace LISCareDTO
{
    public class LoginResponseModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string RoleType { get; set; }
        public string RoleName { get; set; }
        public bool IsMobileVerified { get; set; }
        public bool IsEmailVerified { get; set; }
        public string Token {  get; set; }
        public DateTime Expires_in {  get; set; }
        public string UserStatus { get; set; }
        public string PartnerId {  get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }=string.Empty;
        public int DepartmentId { get;set; }
        public string UserLogo {  get; set; }




    }
}

