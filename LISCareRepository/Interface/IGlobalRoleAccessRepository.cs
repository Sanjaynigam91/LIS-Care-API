using LISCareDTO;
using LISCareDTO.GlobalRoleAccess;
using LISCareDTO.LISRoles;

namespace LISCareRepository.Interface
{
    public interface IGlobalRoleAccessRepository
    {
        /// <summary>
        /// This interface is used to get all Role Type
        /// </summary>
        /// <returns>List<RoleTypeResponseModel></returns>
        List<RoleTypeResponseModel> GetAllRoleType();
        /// <summary>
        /// This interface is used to get roles by role type
        /// </summary>
        /// <returns>List<RoleResponseModel></returns>
        List<RoleResponseModel> GetRolesByRoleType(string roleType);
        /// <summary>
        /// This interface is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<PageHeaderResponseModel></returns>
        List<PageHeaderResponseModel> GetAllPageHeaders(int roleId, string partnerId);
        /// <summary>
        /// This interface is used to update role permission
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        APIResponseModel<object> UpdatePageAccess(RolePermissionRequestModel rolePermission);
        /// <summary>
        /// This interface is used to Save All Page Access
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        APIResponseModel<object> SaveAllPageAccess(RolePermissionRequestModel rolePermission);
        /// <summary>
        /// This interface is used to get all LIS Role
        /// </summary>
        /// <returns>List<LISRoleTypeResponseModel></returns>
        List<LabRolesResponse> GetAllLabRole();

        /// <summary>
        /// This interface is used to Save All Lis Page Access
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        APIResponseModel<object> SaveAllLisPageAccess(LisPageRequestModel lisPageRequest);

        /// <summary>
        /// This interface is used to get all pages by PartnerId
        /// </summary>
        /// <returns>List<RoleResponseModel></returns>
        List<LisPageResponseModel> GetAllLisPages(string partnerId);

        /// <summary>
        /// This interface is used to get all page Entity by partnerId
        /// </summary>
        /// <returns>List<RoleResponseModel></returns>
        List<PageEntityModel> GetAllPageEntity(string partnerId);
        /// <summary>
        /// This interface is used to get all pages by page Entity and partnerId
        /// </summary>
        /// <returns>List<RoleResponseModel></returns>
        List<LisPageModel> GetAllPage(string partnerId,string pageEntity);
        /// <summary>
        /// This interface is used to Get All Criteria
        /// </summary>
        /// <returns>List<LISRoleTypeResponseModel></returns>
        List<LisCareCriteriaModel> GetAllCriteria();

        /// <summary>
        /// This interface is used to get page details by PageId an PartnerId
        /// </summary>
        /// <returns>List<RoleResponseModel></returns>
        LisPageResponseModel GetPageDetailsById(string pageId,string partnerId);

        /// <summary>
        /// This interface is used to update lis page details
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        APIResponseModel<object> UpdatePageDetails(LisPageUpdateRequestModel lisPageUpdate);

        /// <summary>
        /// This interface is used to delete Lis Page by PageId
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        APIResponseModel<object> DeleteLisPageById(string pageId);
        /// <summary>
        /// This interface is used to Search Lis Pages
        /// </summary>
        /// <returns>List<RoleResponseModel></returns>
        List<LisPageResponseModel> SearchLisPages(LisPageSearchRequestModel lisPageSearch);

    }
}
