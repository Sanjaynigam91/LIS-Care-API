namespace LISCareDTO.GlobalRoleAccess
{
    public class RolePermissionRequestModel
    {
        public string MenuId { get; set; }
        public int RoleId { get; set; }
        public bool Visibility { get; set; }
        public bool IsReadEnabled { get; set; }
        public bool IsWriteEnabled { get; set; }
        public bool IsApproveEnabled { get; set; }
        public bool IsSpecialPermssion { get; set; }
        public string PartnerId { get; set; }

    }
}
