namespace LISCareUtility
{
    public class ConstantResource
    {

        public ConstantResource() 
        {
        }   
        #region Route Files
        public const string APIRoute = "api";
        #endregion

        #region API Name

        #endregion

        #region Web Controller Action Parameter
        public const string SignUpAction= "/SignUpAction";
        #endregion

        #region API Parameters

        #region User Module
        public const string UserSignUp = "UserSignUp";
        public const string AddUsers = "AddUsers";
        public const string GetAllUsers = "GetAllUsers";
        public const string GetUserById = "GetUserById";
        public const string Login = "Login";
        public const string GetAllLISUsers = "GetAllLISUsers";
        public const string GetUserTypeByOwnerId = "GetUserTypeByOwnerId";
        public const string GetUserRolesByUserType = "GetUserRolesByUserType";
        public const string SaveUserInformation = "SaveUserInformation";
        public const string UpdateUserInformation = "UpdateUserInformation";
        public const string GetAreadyExistsUser = "GetAreadyExistsUser";
        public const string GetUserRolesByDepartment = "GetUserRolesByDepartment";
        public const string GetUserDetailsByUserCode = "GetUserDetailsByUserCode";
        public const string SearchUser = "SearchUser";
        public const string SearchAllUser = "SearchAllUser";
        public const string GetAllDepartments = "GetAllDepartments";
        public const string DeleteUserById = "DeleteUserById";
        public const string AddSampleCollectedPlace = "AddSampleCollectedPlace";
        public const string DeleteSamplePlace = "DeleteSamplePlace";

        #endregion

        #region Meta Data Module
        public const string GetAllTag = "GetAllTag";
        public const string GetAllMasterListByCategory = "GetAllMasterListByCategory";
        public const string CreateNewMasterList = "CreateNewMasterList";
        public const string UpdateMasterList = "UpdateMasterList";
        public const string DeleteMasterList = "DeleteMasterList";
        public const string CreateNewMetaDataTag = "CreateNewMetaDataTag";
        public const string UpdateMetaDataTag = "UpdateMetaDataTag";
        public const string GetMetaTagById = "GetMetaTagById";
        public const string GetReportTemplates = "GetReportTemplates";
        public const string GetReportingStyle = "GetReportingStyle";
        public const string GetSpecimenType = "GetSpecimenType";
        public const string GetDepartments = "GetDepartments";
        public const string GetSubTestDepartments = "GetSubTestDepartments";


        #endregion

        #region LIS Role Module
        public const string GetAllLISRoles = "GetAllLISRoles";
        public const string GetLISRoleType = "GetLISRoleType";
        public const string AddLISRole = "AddLISRole";
        public const string UpdateLISRole = "UpdateLISRole";
        public const string GetLISRoleByRoleId = "GetLISRoleByRoleId";
        public const string DeleteRoleById = "DeleteRoleById";

        #endregion

        #region Global Role access module
        public const string GetAllRoleType = "GetAllRoleType";
        public const string GetRoleByRoleType = "GetRoleByRoleType";
        public const string GetPageHeadersByRoleId = "GetPageHeadersByRoleId";
        public const string PageAccessPermission = "PageAccessPermission";
        public const string SaveAccessPermission = "SaveAccessPermission";
        public const string GetLabRoles = "GetLabRoles";
        public const string SaveLisPageDetails = "SaveLisPageDetails";
        public const string GetAllLisPages = "GetAllLisPages";
        public const string GetPageEntity = "GetPageEntity";
        public const string GetAllPageByEntity = "GetAllPageByEntity";
        public const string GetAllCriteria = "GetAllCriteria";
        public const string GetPageDetailsById = "GetPageDetailsById";
        public const string UpdateLisPage = "UpdateLisPage";
        public const string DeleteLisPage = "DeleteLisPage";
        public const string SeachLisPages = "SeachLisPages";

        #endregion

        #region Sample Collection Module
        public const string GetSampleCollectedPlaces = "GetSampleCollectedPlaces";
        public const string GetLabTestInfo = "GetLabTestInfo";
        public const string GetTestDepartments = "GetTestDepartments";
        public const string GetTestByTestCode = "GetTestByTestCode";
        public const string DeleteTest = "DeleteTest";
        public const string SearchTests = "SearchTests";
        public const string GetReferalRangeByTestCode = "GetReferalRangeByTestCode";
        public const string GetSpecialValueByTestCode = "GetSpecialValueByTestCode";
        public const string GetCenterRateByTestCode = "GetCenterRateByTestCode";
        public const string CreateTest = "CreateTest";
        public const string UpdateTest = "UpdateTest";
        public const string SaveUpdateReferralRanges = "SaveUpdateReferralRanges";
        public const string DeleteReferralRanges = "DeleteReferralRanges";

        #endregion


        #endregion

        #region Constant Values
        public const string LISCareDbConnection = "LISCareDbConnection";
        public const string TokenModel = "TokenModel";
        public static readonly string ACLImages = "ACLImages";
        public static readonly string Images = "images";
        public static readonly string ImagePath = "/images";
        #endregion

        #region SQL Parameters
        public const string FirstName = "@FirstName";
        public const string LastName = "@LastName";
        public const string Email = "@Email";
        public const string MobileNumber = "@MobileNumber";
        public const string Password = "@Password";
        public const string RoleId = "@RoleId";
        public const string IsError = "@IsError";
        public const string IsSuccess = "@IsSuccess";
        public const string ErrorMsg = "@ErrorMsg";
        public const string UserName = "@UserName";
        public const string UserUniqueId = "@UserId";
        public const string OwnerId = "@OwnerId";
        public const string Usertype = "@usertype";
        public const string ParamUserCode = "@user_code";
        public const string ParamUserName = "@userName";
        public const string ParamUsertype = "@usertype";
        public const string ParamAccountstatus = "@accountstatus";
        public const string ParamRolesAssigned= "@RolesAssigned";
        public const string ParamUserClassificationId = "@user_classification_id";
        public const string ParamAuthenticationMode = "@authenticationMode";
        public const string ParamAccountId = "@AccountId";
        public const string ParamCreatedBy = "@CreatedBy";
        public const string ParamDepartment = "@Department";
        public const string ParamDepartmentId = "@DepartmentId";
        public const string ParamOwnerId = "@ownerId";
        public const string ParamCategory = "@Category";
        public const string ParamItemType = "@ItemType";
        public const string ParamItemDescription = "@ItemDescription";
        public const string ParamRecordId = "@RecordId";
        public const string ParamTagCode = "@tagCode";
        public const string ParamTagDescription = "@tagDescription";
        public const string ParamTagStatus = "@TagStatus";
        public const string ParamRoleCode = "@RoleCode";
        public const string ParamRoleName = "@RoleName";
        public const string ParamRoleStatus = "@RoleStatus";
        public const string ParamRoleType = "@RoleType";
        public const string ParamRoleId = "@RoleId";
        public const string ParamVisibility = "@Visibility";
        public const string ParamRead = "@IsReadEnabled";
        public const string ParamWrite = "@IsWriteEnabled";
        public const string ParamApprove = "@IsApproveEnabled";
        public const string ParamSpermission = "@IsSpecialPermssion";
        public const string ParamMenuid = "@MenuId";
        public const string ParmPartnerId = "@PartnerId";
        public const string ParmUserStatus = "@UserStatus";
        public const string ParmRoleType = "@RoleType";
        public const string ParmDepartmentId = "@DepartmentId";
        public const string ParmUserLogo = "@UserLogo";
        public const string ParmCreatedById = "@CreatedById";
        public const string ParamUserLogoPrefix = "@UserLogoPrefix";
        public const string ParamModifiedById = "@ModifiedById";
        public const string ParamDeletedById = "@DeletedById";
        public const string ParamTagId = "@TagId";
        public const string ParamNavigationId = "@NavigationId";
        public const string ParamPageName = "@PageName";
        public const string ParamPageEntity = "@PageEntity";
        public const string ParamCriteria = "@Criteria";
        public const string ParamTestStatus = "@TestStatus";
        public const string ParamPageId = "@PageId";
        public const string ParamSamplePlace = "@SamplePlace";
        public const string ParamUpdatedBy = "@UpdatedBy";
        public const string ParamTestName = "@TestName";
        public const string ParamDeptOrDiscipline = "@DeptOrDiscipline";
        public const string ParamIsProcessedAt = "@IsProcessedAt";
        public const string ParamTestCode = "@TestCode";
        public const string ParamSubDepartment = "@SubDepartment";
        public const string ParamMethodology = "@Methodology";
        public const string ParamSpecimenType = "@SpecimenType";
        public const string ParamReferenceUnits = "@ReferenceUnits";
        public const string ParamReportingStyle = "@ReportingStyle";
        public const string ParamReportTemplateName = "@ReportTemplateName";
        public const string ParamReportingDecimals = "@ReportingDecimals";
        public const string ParamIsOutlab = "@IsOutlab";
        public const string ParamPrintSequence = "@PrintSequence";
        public const string ParamIsReserved = "@IsReserved";
        public const string ParamTestShortName = "@TestShortName";
        public const string ParamPatientRate = "@PatientRate";
        public const string ParamClientRate = "@ClientRate";
        public const string ParamLabRate= "@LabRate";
        public const string ParamStatus= "@Status";
        public const string ParamAnalyzerName= "@AnalyzerName";
        public const string ParamIsAutomated= "@IsAutomated";
        public const string ParamIsCalculated= "@IsCalculated";
        public const string ParamLabTestCode= "@LabTestCode";
        public const string ParamTestApplicable= "@TestApplicable";
        public const string ParamIsLMP= "@IsLMP";
        public const string ParamIsNABLApplicable= "@IsNABLApplicable";
        public const string ParamReferelRangeComments= "@ReferelRangeComments";

       

        #endregion

        #region ConstantValuse

        #region User Module
        public const string UserId = "UserId";
        public const string FullName = "FullName";
        public const string UserPassword = "Password";
        public const string UserRoleId = "RoleId";
        public const string UserEmail = "Email";
        public const string PhoneNumber = "MobileNumber";
        public const string RoleName = "RoleName";
        public const string IsMobileVerified = "IsMobileVerified";
        public const string IsEmailVerified = "IsEmailVerified";
        public const string tokenExpirationTime = "30";
        public const string UserCode = "User_Code";
        public const string LISUserName = "UserName";
        public const string CreatedOn = "CreatedOn";
        public const string CreatedBy = "CreatedBy";
        public const string UserType = "UserType";
        public const string AccountStatus = "AccountStatus";
        public const string Default_Url = "Default_Url";
        public const string AssignedRole = "AssignedRole";
        public const string LastLogintime = "LastLogintime";
        public const string CurrentLogintime = "CurrentLogintime";
        public const string AccountId = "AccountId";
        public const string UserClassificationId = "User_Classification_Id";
        public const string AuthenticationMode = "AuthenticationMode";
        public const string Department = "Department";
        public const string IsBlocked = "IsBlocked";
        public const string LISUserLastName = "LastName";
        public const string LISUserFirstName = "FirstName";
        public const string LISUserRoleId = "RoleId";
        public const string ItemDescription = "ItemDescription";
        public const string ItemType = "ItemType";
        public const string Category = "Category";
        public const string RoleCode = "RoleCode";
        public const string RoleType = "RoleType";
        public const string RoleStatus = "RoleStatus";
        public const string UserDepartment = "UserDepartment";
        public const string UserCategory = "UserCategory";
        public const string UserStatus = "UserStatus";
        public const string PartnerId = "PartnerId";
        public const string DepartmentId = "DepartmentId";
        public const string DepartmentName = "DepartmentName";
        public const string UserLogo = "UserLogo";
        public const string UserLogoPrefix = "UserLogoPrefix";
        public const string PageName = "PageName";
        public const string PageEntity = "PageEntity";
        public const string Criteria = "Criteria";
        public const string TestStatus = "TestStatus";
        public const string SampleCollectedAt = "SampleCollectedAt";
        public const string TestCode = "TestCode";
        public const string Test_Code = "test_code";
        public const string TestName = "test_name";
        public const string SpecimenType = "specimen_type";
        public const string ContainerType = "Container_Type";
        public const string SpecimenVolume = "Specimen_Volume";
        public const string TransportConditions = "Transport_Conditions";
        public const string ReferenceUnits = "Reference_units";
        public const string Discipline = "Discipline";
        public const string MRP = "MRP";
        public const string B2CRates = "B2C_Rates";
        public const string LabRates = "lab_rates";
        public const string ReportingStyle = "reporting_Style";
        public const string PrintAs = "PrintAs";
        public const string AliasName = "AliasName";
        public const string ReportTemplateName = "report_template_name";
        public const string SubDiscipline = "Sub_Discipline";
        public const string TestDepartment = "TestDepartment";
        public const string Methodology = "Methodology";
        public const string AnalyzerName = "Analyzer_Name";
        public const string IsAutomated = "IsAutomated";
        public const string IsCalculated = "IsCalculated";
        public const string ReportingLeadTime = "Reporting_Lead_Time";
        public const string NormalRangeOneline = "normal_range_oneline";
        public const string ReportingDecimals = "Reporting_Decimals";
        public const string ScheduledDays = "Scheduled_Days";
        public const string IsReserved = "IsReserved";
        public const string IsOutlab = "IsOutlab";
        public const string OutlabCode = "Outlab_Code";
        public const string ReportPrintOrder = "report_print_order";
        public const string ReportSection = "ReportSection";
        public const string LowestAllowed = "LowestAllowed";
        public const string HighestAllowed = "HighestAllowed";
        public const string Technology = "technology";
        public const string CptCode = "CptCode";
        public const string CalculatedValue = "CalculatedValue";
        public const string NormalRangeFooter = "normal_range_footer";
        public const string DepartmentWiseNumbers = "DepartmentWiseNumbers";
        public const string TestShortName = "TestShortName";
        public const string Modality = "Modality";
        public const string DefaultFilmCount = "DefaultFilmCount";
        public const string DefaultContrastML = "DefaultContrastML";
        public const string TestProfitRate = "TestProfitRate";
        public const string LabTestCode = "LabTestCode";
        public const string TestApplicable = "TestApplicable";
        public const string IsLMP = "IsLMP";
        public const string OldtestCode = "Oldtest_code";
        public const string IsNABLApplicable = "IsNABLApplicable";
        public const string ReferralId = "NormalRangeId";
        public const string Gender = "Gender";
        public const string LowRange = "LowRange";
        public const string HighRange = "HighRange";
        public const string NormalRange = "NormalRange";
        public const string AgeFrom = "AgeFrom";
        public const string AgeTo = "AgeTo";
        public const string IsPregnant = "isPregnant";
        public const string LowCriticalValue = "LowCriticalValue";
        public const string AgeUnits = "AgeUnits";
        public const string HighCriticalValue = "HighCriticalValue";
        public const string LabTest = "LabTest";
        public const string AllowedValue = "Allowed_value";
        public const string IsAbnormal = "IsAbnormal";
        public const string PartnerCode = "PartnerCode";
        public const string PartnerName = "PartnerName";
        public const string BillRate = "Bill_Rate";
        public const string RoletypeId = "RoletypeId";



        #endregion

        #region Meta Data Module
        public const string TagId = "TagId";
        public const string MetaOwnerId = "OwnerId";
        public const string TagCode = "TagCode";
        public const string TagDescription = "TagDescription";
        public const string TagStatus = "TagStatus";
        public const string RecordId = "RecordId";
       
        #endregion

        #region Global Role Access
        public const string NavigationId = "NavigationId";
        public const string UrlLabel = "UrlLabel";
        public const string MessageHeader = "Message_Header";
        public const string MenuId = "MenuId";
        public const string Visibility = "Visibility";
        public const string IsReadEnabled = "IsReadEnabled";
        public const string IsWriteEnabled = "IsWriteEnabled";
        public const string IsApproveEnabled = "IsApproveEnabled";
        public const string IsSpecialPermssion = "IsSpecialPermssion";
        public const string LISOwnerId = "OwnerId";
        public const string LisPageId = "PageId";
        public const string CriteriaId = "CriteriaId";
  


        #endregion

        #endregion

        #region SQL Store Procedures

        #region User Module
        public const string UspSaveUserData = "Usp_SaveUserData";
        public const string UspGetUserDetails = "Usp_GetUserDetails";
        public const string UspValidateUserLogin = "Usp_ValidateUserLogin";
        public const string UspGetAllUserDetails = "Usp_Get_All_UserDetails";
        public const string UspGetUserDetailsByUserId = "Usp_GetUserDetailsByUserId";
        public const string UspGetAllLISUsers = "Usp_GetAllLISUsers";
        public const string UspGetUserType = "Usp_GetUserType";
        public const string UspGetUserRolesByUserType = "Usp_GetUserRolesByUserType";
        public const string UspUserSaveChanges = "Usp_UserSaveChanges";
        public const string UspUserUpdateChanges = "Usp_UserUpdateChanges";
        public const string UspGetAreadyCodeExists = "Usp_GetAreadyCodeExists";
        public const string UspGetUserRolesByDepartment = "Usp_GetUserRolesByDepartment";
        public const string UspGetUsersByUserCode = "Usp_GetUsersByUserCode";
        public const string UspSearchUser = "Usp_SearchUser";
        public const string UspGetLabRoles = "Usp_GetLabRoles";
        public const string UspSearchUserDetails = "Usp_SearchUserDetails";
        public const string UspGetAllDepartment = "Usp_GetAllDepartment";
        public const string UspSavePartnerUsers = "Usp_SavePartnerUsers";
        public const string UspDeleteUserbyId = "Usp_DeleteUserbyId";
        public const string UspDeleteRolebyId = "Usp_DeleteRolebyId";
        public const string UspSaveLisPageNavigations = "Usp_SaveLisPageNavigations";
        public const string UspGetLisPages = "Usp_GetLisPages";
        public const string UspGetPageEntity = "Usp_getPageEntity";
        public const string UspGetPagesByPageId = "Usp_GetPagesByPageId";
        public const string Usp_DeletePageDetails = "Usp_DeletePageDetails";
        public const string UspUpdatePageDetails = "Usp_UpdatePageDetails";

        #endregion

        #region Meta Data Module
        public const string UspGetAllTags = "Usp_GetAllTags";
        public const string UspGetAllTagsByCategory = "Usp_GetAllTagsByCategory";
        public const string UspCreateNewMasterList = "Usp_CreateNewMasterList";
        public const string UspUpdateMasterList = "Usp_UpdateMasterList";
        public const string UspDeleteMasterList = "Usp_DeleteMasterList";
        public const string UspCreateNewTag = "Usp_CreateNewTag";
        public const string UspUpdateTag = "Usp_UpdateTag";
        public const string UspGetTagsByTagId = "Usp_GetTagsByTagId";

        #endregion


        #region LIS Role Module
        public const string UspGetAllLISRoles = "Usp_GetAllLISRoles";
        public const string UspGetRoleType = "Usp_GetRoleType";
        public const string UspAddNewLISRole = "Usp_AddNewLISRole";
        public const string UspUpdateLISRole = "Usp_UpdateLISRole";
        public const string UspGetLISRoleByRoleId = "Usp_GetLISRoleByRoleId";
        #endregion

        #region Global Role Access Module
        public const string UspGetLISRoleType = "Usp_GetLISRoleType";
        public const string UspGetRolesByRoleType = "Usp_GetRolesByRoleType";
        public const string UspGetAllPageHeaders = "Usp_GetAllPageHeaders";
        public const string UspUpdateData = "Usp_UpdateData";
        public const string UspSaveAllRoleAceess = "usp_SaveAllRoleAceess";
        public const string UspGetAllLisPages = "Usp_GetAllLisPages";
        public const string UspGetAllCriteria = "usp_GetAllCriteria";
        public const string UspSearchLisPages = "Usp_SearchLisPages";

        #endregion

        #region Sample CollectedAt Module
        public const string UspGetAllSampleCollectedPlaces = "Usp_GetAllSampleCollectedPlaces";
        public const string UspAddSampleCollectedPlaces = "Usp_AddSampleCollectedPlaces";
        public const string UspRemoveSamplePlace = "Usp_RemoveSamplePlace"; 

        #endregion

        #region Test Master data
        public const string UspGetTestMasterData = "Usp_Get_TestMaster_Data";
        public const string UspRetrieveTestDepartments = "Usp_Retrieve_Test_Departments";
        public const string UspRetrieveTestByTestCode = "Usp_Retrieve_TestData_ByTestCode";
        public const string UspDeleteTestRecord = "Usp_Delete_TestRecord";
        public const string UspSearchTestData = "Usp_Search_TestData";
        public const string UspGetReportTemplates = "Usp_Get_Report_Templates";
        public const string UspGetReportingStyle = "Usp_Get_Reporting_Style";
        public const string UspGetSpecimenType = "Usp_Get_Specimen_Type";
        public const string UspGetTestDepartments = "Usp_Get_Test_Departments";
        public const string UspGetTestSubdepartments = "Usp_Get_Test_Subdepartments";
        public const string UspGetReferalRangesByTestCode = "Usp_Get_Referal_Ranges_ByTestCode";
        public const string UspGetSpecialValues = "usp_Get_Special_values";
        public const string UspRetrieveAllCCRates = "Usp_Retrieve_AllCCRates";
        public const string UspSaveTestMasterDetails = "Usp_Save_TestMaster_Details";
        public const string UspUpdateTestDetails = "Usp_Update_TestDetails";
        public const string UspReferralRangesSaveUpdateChanges = "Usp_ReferralRanges_SaveUpdateChanges";
        public const string UspDeleteReferralRanges = "Usp_Delete_Referral_Ranges";
        #endregion

        #endregion

        #region API Response Messages
        public readonly static string Failed = "Failed";
        public readonly static string Success = "Success";
        public readonly static string InvaidUser = "Invalid User";
        public readonly static string AlreadyExistsUser = "Already Exists , Try Different User Code please ...!";
        public readonly static string RolesNotFound = "No Roles Information found";
        public readonly static string UserNotExists = "This user not exists";
        public readonly static string NoMetaDataFound = "No Metadata found";
        public readonly static string NoMetaDataTagsFound = "There is no meta data tags found";
        public readonly static string TagSuccessMsg = "New Metadata tag has been added, Meta Data";
        public readonly static string UpdateTagSuccessMsg = "Metadata tag details updated successfully, Meta Data";
        public readonly static string LISRoleSuccessMsg = "New role has been added, Role master";
        public readonly static string LISRoleUpdateMsg = "LIS role has been updated, Role master";
        public readonly static string RoleNotExists = "This role Id is not exists,Try Different role Id please ...!";
        public readonly static string PageAccessMsg = "Global Roles Updated Successfully ,Global Roles";
        public readonly static string SaveAccessMsg = "Global Roles Saved Successfully ,Global Roles";
        public readonly static string DelUserSuccess = "User Deleted Successfully ,User Master";
        public readonly static string DelRoleSuccess = "Role Deleted Successfully ,Role Master";
        public readonly static string PageSuccess = "Lis Page save Succssfully ,Lis Page Master";
        public readonly static string DelSampleSuccess = "Sample Place has been deleted sucessfully ,Sample Collected Place";
        public readonly static string AddSampleSuccess = "Sample Place has been save sucessfully ,Sample Collected Place";
        public readonly static string DelTestSuccess = "Test data Deleted Successfully ,Test Master";
        public readonly static string AddTestSuccess = "Test has been created Successfully ,Test Master";
        // Add these constants to the ConstantResource class to fix CS0117 errors
        public const string ParamOpType = "@OpType";
        public const string ParamReferralId = "@ReferralId";
        public const string ParamRefTestCode = "@RefTestCode";
        public const string ParamLowRange = "@LowRange";
        public const string ParamHighRange = "@HighRange";
        public const string ParamNormalRange = "@NormalRange";
        public const string ParamAgeFrom = "@AgeFrom";
        public const string ParamAgeTo = "@AgeTo";
        public const string ParamGender = "@Gender";
        public const string ParamIsPregnant = "@IsPregnant";
        public const string ParamCriticalValue = "@CriticalValue";
        public const string ParamHighCriticalValue = "@HighCriticalValue";
        public const string GreaterThanZero = "Referral Id must be greater than zero.";
        #endregion
    }
}
