using LISCareDTO;
using LISCareDTO.MetaData;

namespace LISCareBussiness.Interface
{
    public interface IMetaData
    {
        /// <summary>
        /// This interface is used to get Meta Data By OwnerId
        /// </summary>
        /// <returns>List<MetaDataResponseModel></returns>
        List<MetaDataResponseModel> GetAllMetaData(string partnerId);
        /// <summary>
        /// This interface is used to get Meta Data By Category
        /// </summary>
        /// <returns>List<MetaDataTagsResponseModel></returns>
        List<MetaDataTagsResponseModel> GetMetaDataTagsByCategory(string category, string partnerId);
        /// <summary>
        /// This interface is used to create new master list
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        APIResponseModel<object> CreateNewMasterList(MasterListRequestModel masterList);
        /// <summary>
        /// This interface is used to Update master list
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        APIResponseModel<object> UpdateMasterList(MasterListRequestModel masterList);
        /// <summary>
        /// This interface is used to delete master list
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        APIResponseModel<object> DeleteMasterList(int recordId);
        /// <summary>
        /// This interface is used to create new meta data tag
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        APIResponseModel<object> CreateNewMetaDataTag(MetaDataTagRequestModel metaDataTag);
        /// <summary>
        /// This interface is used to update meta data tag
        /// </summary>
        /// <returns>List<APIResponseModel></returns>
        APIResponseModel<object> UpdateTagInfo(MetaTagModel metaDataTag);
        /// <summary>
        /// This interface is used to get Meta Data Tag By TagId
        /// </summary>
        /// <returns>List<MetaDataResponseModel></returns>
        MetaDataResponseModel GetMetaDataTag(int tagId);
        /// <summary>
        /// This interface is used to get Reporting Template
        /// </summary>
        /// <returns>List<MetaDataResponseModel></returns>
        List<MetaTagResponse> GetReportTemplates(string partnerId);
        /// <summary>
        /// This interface is used to Reporting Style
        /// </summary>
        /// <returns>List<MetaDataResponseModel></returns>
        List<MetaTagResponse> GetReportingStyle(string partnerId);
        /// <summary>
        /// This interface is used to Get Specimen Type
        /// </summary>
        /// <returns>List<MetaDataResponseModel></returns>
        List<MetaTagResponse> GetSpecimenType(string partnerId);
        /// <summary>
        /// This interface is used to Get Test Departments
        /// </summary>
        /// <returns>List<MetaDataResponseModel></returns>
        List<MetaTagResponse> GetTestDepartments(string partnerId);
        /// <summary>
        /// This interface is used to Get Sub Test Departments
        /// </summary>
        /// <returns>List<MetaDataResponseModel></returns>
        List<MetaTagResponse> GetSubTestDepartments(string partnerId);

    }
}
