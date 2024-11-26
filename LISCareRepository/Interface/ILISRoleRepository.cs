using LISCareDTO;
using LISCareDTO.LISRoles;

namespace LISCareRepository.Interface
{
    public interface ILISRoleRepository
    {

        /// <summary>
        /// This interface is used to add new LIS role
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        APIResponseModel<object> AddNewLISRole(LISRoleRequestModel lISRoleRequest);
        /// <summary>
        /// This interface is used to update LIS role
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        APIResponseModel<object> UpdateLISRole(LISRoleUpdateRequestModel lISRoleUpdate);
        /// <summary>
        /// This interface is used to get All LIS Roles
        /// </summary>
        /// <returns>List<LISRoleResponseModel></returns>
        List<LISRoleResponseModel> GetAllLISRoles();
        /// <summary>
        /// This interface is used to get all LIS Role Type
        /// </summary>
        /// <returns>List<LISRoleTypeResponseModel></returns>
        List<LISRoleTypeResponseModel> GetLISRoleType();
        /// <summary>
        /// This interface is used to get LIS role by roleId
        /// </summary>
        /// <returns>List<LISRoleResponseModel></returns>
        LISRoleResponseModel GetLISRoleByRoleId(int roleId);
        // This interface used delete Roles
        APIResponseModel<object> RoleDeletedById(int roleId);
    }
}
