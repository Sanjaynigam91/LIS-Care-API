using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.LISRoles;
using LISCareRepository.Interface;
using Microsoft.Extensions.Configuration;

namespace LISCareBussiness.Implementation
{
    public class LISRoleBAL : ILISRole
    {
        private readonly IConfiguration _configuration;
        private readonly ILISRoleRepository _lISRoleRepository;
        public LISRoleBAL(IConfiguration configuration, ILISRoleRepository lISRoleRepository)
        {
            _configuration = configuration;
            _lISRoleRepository = lISRoleRepository;
        }
        /// <summary>
        /// This method is used to add new LIS role
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> AddNewLISRole(LISRoleRequestModel lISRoleRequest)
        {
            _ = new APIResponseModel<object>();
            APIResponseModel<object>? response;
            try
            {
                response = _lISRoleRepository.AddNewLISRole(lISRoleRequest);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// This method is used to get All LIS Roles
        /// </summary>
        /// <returns>List<LISRoleResponseModel></returns>
        public List<LISRoleResponseModel> GetAllLISRoles()
        {
            _ = new List<LISRoleResponseModel>();
            List<LISRoleResponseModel>? response;
            try
            {
                response = _lISRoleRepository.GetAllLISRoles();
            }
            catch
            {
                throw;
            }
            return response;
        }
        /// <summary>
        /// This method is used to get LIS role by roleId
        /// </summary>
        /// <returns>List<LISRoleResponseModel></returns>
        public LISRoleResponseModel GetLISRoleByRoleId(int roleId)
        {
            _ = new LISRoleResponseModel();
            LISRoleResponseModel? response;
            try
            {
                response = _lISRoleRepository.GetLISRoleByRoleId(roleId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// This method is used to get all LIS Role Type
        /// </summary>
        /// <returns>List<LISRoleTypeResponseModel></returns>
        public List<LISRoleTypeResponseModel> GetLISRoleType()
        {
            _ = new List<LISRoleTypeResponseModel>();
            List<LISRoleTypeResponseModel>? response;
            try
            {
                response = _lISRoleRepository.GetLISRoleType();
            }
            catch
            {
                throw;
            }
            return response;
        }

        public APIResponseModel<object> RoleDeletedById(int roleId)
        {
            _ = new APIResponseModel<object>();
            APIResponseModel<object>? response;
            try
            {
                response = _lISRoleRepository.RoleDeletedById(roleId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// This method is used to Update LIS role
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> UpdateLISRole(LISRoleUpdateRequestModel lISRoleUpdate)
        {
            _ = new APIResponseModel<object>();
            APIResponseModel<object>? response;
            try
            {
                response = _lISRoleRepository.UpdateLISRole(lISRoleUpdate);
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
