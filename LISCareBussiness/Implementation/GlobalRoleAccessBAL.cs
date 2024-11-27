using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.GlobalRoleAccess;
using LISCareDTO.LISRoles;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using Microsoft.Extensions.Configuration;

namespace LISCareBussiness.Implementation
{
    public class GlobalRoleAccessBAL: IGlobalRoleAccess
    {
        private readonly IConfiguration _configuration;
        private readonly IGlobalRoleAccessRepository _globalRoleAccessRepository;
        public GlobalRoleAccessBAL(IConfiguration configuration, IGlobalRoleAccessRepository globalRoleAccessRepository)
        {
            _configuration = configuration;
            _globalRoleAccessRepository = globalRoleAccessRepository;
        }

        public APIResponseModel<object> DeleteLisPageById(string pageId)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = _globalRoleAccessRepository.DeleteLisPageById(pageId);
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }

        public List<LisCareCriteriaModel> GetAllCriteria()
        {
            _ = new List<LisCareCriteriaModel>();
            List<LisCareCriteriaModel>? response;
            try
            {
                response = _globalRoleAccessRepository.GetAllCriteria();
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<LabRolesResponse> GetAllLabRole()
        {
            _ = new List<LabRolesResponse>();
            List<LabRolesResponse>? response;
            try
            {
                response = _globalRoleAccessRepository.GetAllLabRole();
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<LisPageResponseModel> GetAllLisPages(string partnerId)
        {
            _ = new List<LisPageResponseModel>();
            List<LisPageResponseModel>? response;
            try
            {
                response = _globalRoleAccessRepository.GetAllLisPages(partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<LisPageModel> GetAllPage(string partnerId, string pageEntity)
        {
            _ = new List<LisPageModel>();
            List<LisPageModel>? response;
            try
            {
                response = _globalRoleAccessRepository.GetAllPage(partnerId, pageEntity);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<PageEntityModel> GetAllPageEntity(string partnerId)
        {
            _ = new List<PageEntityModel>();
            List<PageEntityModel>? response;
            try
            {
                response = _globalRoleAccessRepository.GetAllPageEntity(partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }



        /// <summary>
        /// This method is used to get all page headers by role Id
        /// </summary>
        /// <returns>List<PageHeaderResponseModel></returns>
        public List<PageHeaderResponseModel> GetAllPageHeaders(int roleId, string partnerId)
        {
            _ = new List<PageHeaderResponseModel>();
            List<PageHeaderResponseModel>? response;
            try
            {
                response = _globalRoleAccessRepository.GetAllPageHeaders(roleId, partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// This method is used to get all Role Type
        /// </summary>
        /// <returns>List<LISRoleTypeResponseModel></returns>
        public List<RoleTypeResponseModel> GetAllRoleType()
        {
            _ = new List<RoleTypeResponseModel>();
            List<RoleTypeResponseModel>? response;
            try
            {
                response = _globalRoleAccessRepository.GetAllRoleType();
            }
            catch
            {
                throw;
            }
            return response;
        }

        public LisPageResponseModel GetPageDetailsById(string pageId, string partnerId)
        {
            _ = new LisPageResponseModel();
            LisPageResponseModel? response;
            try
            {
                response = _globalRoleAccessRepository.GetPageDetailsById(pageId, partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<RoleResponseModel> GetRolesByRoleType(string roleType)
        {
            _ = new List<RoleResponseModel>();
            List<RoleResponseModel>? response;
            try
            {
                response = _globalRoleAccessRepository.GetRolesByRoleType(roleType);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public APIResponseModel<object> SaveAllLisPageAccess(LisPageRequestModel lisPageRequest)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = _globalRoleAccessRepository.SaveAllLisPageAccess(lisPageRequest);
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// This method is used to Save All Page Access
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> SaveAllPageAccess(RolePermissionRequestModel rolePermission)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = _globalRoleAccessRepository.SaveAllPageAccess(rolePermission);
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }

        public List<LisPageResponseModel> SearchLisPages(LisPageSearchRequestModel lisPageSearch)
        {
            _ = new List<LisPageResponseModel>();
            List<LisPageResponseModel>? response;
            try
            {
                response = _globalRoleAccessRepository.SearchLisPages(lisPageSearch);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// This method is used to update role permission
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> UpdatePageAccess(RolePermissionRequestModel rolePermission)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = _globalRoleAccessRepository.UpdatePageAccess(rolePermission);
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }

        public APIResponseModel<object> UpdatePageDetails(LisPageUpdateRequestModel lisPageUpdate)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = _globalRoleAccessRepository.UpdatePageDetails(lisPageUpdate);
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }
    }
}
