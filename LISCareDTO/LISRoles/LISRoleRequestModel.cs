namespace LISCareDTO.LISRoles
{
    public class LISRoleRequestModel
    {
        public required string RoleCode { get; set; }
        public required string RoleName { get; set; }
        public required string RoleType { get; set; }
        public string? Department { get; set; }
        public required string RoleStatus { get; set; }
    }
}
