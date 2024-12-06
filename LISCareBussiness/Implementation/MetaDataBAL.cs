using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.MetaData;
using LISCareRepository.Interface;
using Microsoft.Extensions.Configuration;

namespace LISCareBussiness.Implementation
{
    public class MetaDataBAL : IMetaData
    {
        private readonly IConfiguration _configuration;
        private readonly IMetaDataRepository _metaDataRepository;

        public MetaDataBAL(IConfiguration configuration, IMetaDataRepository metaDataRepository)
        {
            _configuration = configuration;
            _metaDataRepository = metaDataRepository;
        }
        /// <summary>
        /// This method is used to create new master list
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> CreateNewMasterList(MasterListRequestModel masterList)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = _metaDataRepository.CreateNewMasterList(masterList);
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }
        /// <summary>
        /// This method is used to create new master list
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> CreateNewMetaDataTag(MetaDataTagRequestModel metaDataTag)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = _metaDataRepository.CreateNewMetaDataTag(metaDataTag);
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }
        /// <summary>
        /// This method is used to delete master list
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> DeleteMasterList(int recordId)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = _metaDataRepository.DeleteMasterList(recordId);
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }
        /// <summary>
        /// This Method is used to get Meta Data By OwnerId
        /// </summary>
        /// <returns>List<MetaDataResponseModel></returns>
        public List<MetaDataResponseModel> GetAllMetaData(string partnerId)
        {
            var response = new List<MetaDataResponseModel>();
            try
            {
                response = _metaDataRepository.GetAllMetaData(partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public MetaDataResponseModel GetMetaDataTag(int tagId)
        {
            var response = new MetaDataResponseModel();
            try
            {
                response = _metaDataRepository.GetMetaDataTag(tagId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// This method is used to get Meta Data By Category
        /// </summary>
        /// <returns>List<MetaDataTagsResponseModel></returns>
        public List<MetaDataTagsResponseModel> GetMetaDataTagsByCategory(string category, string partnerId)
        {
            var response = new List<MetaDataTagsResponseModel>();
            try
            {
                response = _metaDataRepository.GetMetaDataTagsByCategory(category, partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<MetaTagResponse> GetReportingStyle(string partnerId)
        {
            var response = new List<MetaTagResponse>();
            try
            {
                response = _metaDataRepository.GetReportingStyle(partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<MetaTagResponse> GetReportTemplates(string partnerId)
        {
            var response = new List<MetaTagResponse>();
            try
            {
                response = _metaDataRepository.GetReportTemplates(partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<MetaTagResponse> GetSpecimenType(string partnerId)
        {
            var response = new List<MetaTagResponse>();
            try
            {
                response = _metaDataRepository.GetSpecimenType(partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<MetaTagResponse> GetSubTestDepartments(string partnerId)
        {
            var response = new List<MetaTagResponse>();
            try
            {
                response = _metaDataRepository.GetSubTestDepartments(partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public List<MetaTagResponse> GetTestDepartments(string partnerId)
        {
            var response = new List<MetaTagResponse>();
            try
            {
                response = _metaDataRepository.GetTestDepartments(partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// This method is used to Update master list
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> UpdateMasterList(MasterListRequestModel masterList)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = _metaDataRepository.UpdateMasterList(masterList);
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }
        /// <summary>
        /// This method is used to update meta data tag
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        public APIResponseModel<object> UpdateTagInfo(MetaTagModel metaDataTag)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = _metaDataRepository.UpdateTagInfo(metaDataTag);
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            return response;
        }
    }
}
