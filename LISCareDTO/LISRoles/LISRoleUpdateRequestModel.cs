namespace LISCareDTO.LISRoles
{
    public class LISRoleUpdateRequestModel
    {
        public int RoleId { get; set; }
        public string? RoleCode { get; set; }
        public string? RoleName { get; set; }
        public string? RoleType { get; set; }
        public string? Department { get; set; }
        public required string RoleStatus { get; set; }
    }
}
