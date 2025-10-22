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
        public const string GetAllProfileDetails = "GetAllProfileDetails";
        public const string GetAllAnalyzers = "GetAllAnalyzers";
        public const string GetAllSuppliers = "GetAllSuppliers";
        public const string GetAnalyzerDetailById = "GetAnalyzerDetailById";
        public const string GetAnalyzerTestMappings = "GetAnalyzerTestMappings";
        public const string AddNewAnalyzer = "AddNewAnalyzer";
        public const string UpdateAnalyzer = "UpdateAnalyzer";
        public const string DeleteAnalyzer = "DeleteAnalyzer";
        public const string GetMappingByMappingId = "GetMappingByMappingId";
        public const string AddTestMapping = "AddTestMapping";
        public const string UpdateTestMapping = "UpdateTestMapping";
        public const string DeleteTestMapping = "DeleteTestMapping";
        public const string GetAllCenters = "GetAllCenters";
        public const string GetSalesIncharge = "GetSalesIncharge";
        public const string AddNewCenter = "AddNewCenter";
        public const string UpdateCenter = "UpdateCenter";
        public const string DeleteCenter = "DeleteCenter";
        public const string GetCenterByCenterCode = "GetCenterByCenterCode";
        public const string GetCentreCustomRates = "GetCentreCustomRates";
        public const string UpdateCenterRates = "UpdateCenterRates";
        public const string ImportCenterRates = "ImportCenterRates";
        public const string GetAllClinics = "GetAllClinics";
        public const string AddNewClinic = "AddNewClinic";
        public const string UpdateClinic = "UpdateClinic";
        public const string DeleteClinic = "DeleteClinic";
        public const string GetClinicById = "GetClinicById";
        public const string GetAllClients = "GetAllClients";
        public const string GetClientById = "GetClientById";

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
        public const string SaveUpdateSpecialValues = "SaveUpdateSpecialValues";
        public const string DeleteSpecialValue = "DeleteSpecialValue";
        public const string DeleteProfileByProfileCode = "DeleteProfileByProfileCode";
        public const string GetProfileByProfileCode = "GetProfileByProfileCode";
        public const string GetAllMappedTests = "GetAllMappedTests";
        public const string CreateProfile = "CreateProfile";
        public const string UpdateTestProfile = "UpdateTestProfile";
        public const string DeleteMappedTest = "DeleteMappedTest";
        public const string SaveMappingTest = "SaveMappingTest";
        public const string UpdateAllMappings = "UpdateAllMappings";

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
        public const string ParamAllowedvalue = "@Allowedvalue";
        public const string ParamIsAbnormal = "@IsAbnormal";
        public const string ParamProfileCode = "@ProfileCode";
        public const string ParamProfileName = "@ProfileName";
        public const string ParamProfileStatus = "@ProfileStatus";
        public const string ParamSampleTypes = "@SampleTypes";
        public const string ParamIsAvailableForAll = "@IsAvailableForAll";
        public const string ParamIsProfileOutLab = "@IsProfileOutLab";
        public const string ParamProfileFooter = "@ProfileFooter";
        public const string ParamIsNABApplicable= "@IsNABApplicable";
        public const string ParamMappingId= "@MappingId";
        public const string ParamSectionName= "@SectionName";
        public const string ParamPrintOrder= "@PrintOrder";
        public const string ParamGroupHeader= "@GroupHeader";
        public const string ParamAnalyzerNameOrShortCode = "@AnalyzerNameOrShortCode";
        public const string ParamAnalyzerId = "@AnalyzerId";
        public const string ParamAnalyzerShortCode = "@AnalyzerShortCode";
        public const string ParamSupplierCode = "@SupplierCode";
        public const string ParamPurchasedValue = "@PurchasedValue";
        public const string ParamWarrantyEndDate = "@WarrantyEndDate";
        public const string ParamEngineerContactNo = "@EngineerContactNo";
        public const string ParamAssetCode = "@AssetCode";
        public const string ParamAnalyzerTestCode = "@AnalyzerTestCode";
        public const string ParamCenterStatus = "@CenterStatus";
        public const string ParamSearchBy = "@SearchBy";
        public const string ParamCenterCode = "@CenterCode";
        public const string ParamCenterName = "@CenterName";
        public const string ParamCenterInchargeName = "@CenterInchargeName";
        public const string ParamSalesIncharge = "@SalesIncharge";
        public const string ParamCenterAddress = "@CenterAddress";
        public const string ParamPinCode = "@PinCode";
        public const string ParamCenterMobileNumber = "@MobileNumber";
        public const string ParamAlternateContactNo = "@AlternateContactNo";
        public const string ParamEmailId = "@EmailId";
        public const string ParamRateType = "@RateType";
        public const string ParamIntroducedBy = "@IntroducedBy";
        public const string ParamCreditLimit = "@CreditLimit";
        public const string ParamIsAutoBarcode = "@IsAutoBarcode";
        public const string ParamCreateBy = "@CreateBy";
        public const string ParamUpdateBy = "@UpdateBy";
        public const string ParamBillRate = "@BillRate";
        public const string ParamModifiedBy = "@ModifiedBy";
        public const string ParamRateCreatedBy = "@CreatedBy";
        public const string ParamClinicStatus = "@ClinicStatus";
        public const string ParamCentreCode = "@CentreCode";
        public const string ParamClinicId = "@ClinicId";
        public const string ParamClinicName ="@ClinicName";
        public const string ParamClinicIncharge ="@ClinicIncharge";
        public const string ParamClinicDoctorCode ="@ClinicDoctorCode";
        public const string ParamClinicAddress = "@ClinicAddress";
        public const string ParamClientStatus = "@ClientStatus";
        public const string ParamClientId = "@ClientId";



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
        public const string ProfileCode = "ProfileCode";
        public const string ProfileStatus = "ProfileStatus";
        public const string ProfileB2CRates = "B2CRates";
        public const string SampleTypes = "SampleTypes";
        public const string ProfileLabRates = "Labrates";
        public const string TatHrs = "TatHrs";
        public const string CptCodes = "CptCodes";
        public const string PrintSequence = "PrintSequence";
        public const string IsRestricted = "IsRestricted";
        public const string SubProfilesCount = "SubProfilesCount";
        public const string ProfileNormalRangeFooter = "NormalRangeFooter";
        public const string ProfileProfitRate = "ProfileProfitRate";
        public const string LabTestCodes = "LabTestCodes";
        public const string IsProfileOutLab = "IsProfileOutLab";
        public const string ProfileName = "ProfileName";
        public const string ProfileLabTestCode = "LabTestCode";
        public const string TestsMappingId = "testsmappingid";
        public const string ProfileReportTemplateName = "reporttemplatename";
        public const string PrintOrder = "ReportPrintOrder";
        public const string SectionName = "ReportLogicalSection";
        public const string GroupHeader = "groupheader";
        public const string CanPrintSeparately = "CanPrintSeparately";
        public const string MappedTestName = "TestName";
        public const string IsAvailableForAll = "IsAvailableForAll";
        public const string ProfileTemplateName = "reporttemplatename";
        public const string AnalyzerId = "AnalyzerId";
        public const string AnalyzerNames = "AnalyzerName";
        public const string AnalyzerShortCode = "AnalyzerShortCode";
        public const string Status = "Status";
        public const string SupplierCode = "SupplierCode";
        public const string PurchasedValue = "PurchasedValue";
        public const string WarrantyEndDate = "WarrantyEndDate";
        public const string EngineerContactNo = "EngineerContactNo";
        public const string AssetCode = "AssetCode";
        public const string SupplierName = "CompanyName";
        public const string MappingId = "MappingId";
        public const string AnalyzerTestCode = "Analyzer_TestCode";
        public const string AnalyzerLabTestCode = "Lab_TestCode";
        public const string IsProfileCode = "IsProfileCode";
        public const string SampleType = "SampleType";
        public const string CenterCode = "CenterCode";
        public const string CenterName = "CenterrName";
        public const string CenterInchargeName = "CenterInchargeName";
        public const string SalesIncharge = "SalesIncharge";
        public const string CenterAddress = "CenterAddress";
        public const string PinCode = "PinCode";
        public const string CenterMobileNumber = "MobileNumber";
        public const string AlternateContactNo = "AlternateContactNo";
        public const string EmailId = "EmailId";
        public const string RateType = "RateType";
        public const string CenterStatus = "CenterStatus";
        public const string IntroducedBy = "IntroducedBy";
        public const string IsAutoBarcode = "IsAutoBarcode";
        public const string City = "City";
        public const string CreditLimit = "CreditLimit";
        public const string EmployeeId = "EmployeeId";
        public const string EmployeeName = "EmployeeName";
        public const string CentreCode = "CentreCode";
        public const string CentreName = "CentreName";
        public const string AgreedRate = "AgreedRate";
        public const string CentreTestName = "TestName";
        public const string ClinicId = "ClinicId";
        public const string ClinicCode = "ClinicCode";
        public const string ClinicName = "ClinicName";
        public const string ClinicIncharge = "ClinicIncharge";
        public const string ClinicDoctorCode = "ClinicDoctorCode";
        public const string ClinicAddress = "ClinicAddress";
        public const string ClinicStatus = "ClinicStatus";
        public const string UpdatedOn = "UpdatedOn";
        public const string UpdateBy = "UpdateBy";
        public const string ClientId = "ClientId";
        public const string ClientCode = "ClientCode";
        public const string ClientName = "ClientName";
        public const string IsAccessEnabled = "IsAccessEnabled";
        public const string ClientStatus = "ClientStatus";
        public const string LicenseNumber = "LicenseNumber";
        public const string DiscountPercentage = "DiscountPercentage";
        public const string Speciality = "Speciality";
        public const string ClientType = "ClientType";
        public const string BillingType = "BillingType";
        public const string AddressInfo = "AddressInfo";



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
        public const string UspInsertCenterTestRates = "Usp_InsertCenterTestRates";


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

        #region Test & Profile Master data
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
        public const string UspAllowedValuesSaveUpdateChanges = "Usp_AllowedValues_SaveUpdateChanges";
        public const string UspDeleteAllowedValue = "Usp_DeleteAllowedValue";
        public const string UspRetrieveProfileDetails = "Usp_Retrieve_ProfileDetails";
        public const string UspDeleteProfileDetails = "Usp_Delete_ProfileDetails";
        public const string UspGetProfileDetailsByProfileCode = "Usp_GetProfileDetails_ByProfileCode";
        public const string UspProfileMappingRetrieve = "usp_ProfileMapping_Retrieve";
        public const string USPSaveProfileDetails = "USP_SaveProfileDetails";
        public const string USPUpdateProfileDetails = "USP_UpdateProfileDetails";
        public const string UspDeleteMappingDetails = "Usp_DeleteMappingDetails";
        public const string UspTestMappingForProfile = "Usp_TestMappingForProfile";
        public const string UspUpdateTestMapping = "Usp_UpdateTestMapping";

        #endregion

        #region Analyzer Master 
        public const string USPGetAllAnalyzers = "USP_GetAllAnalyzers";
        public const string USPGetAllSuppliers = "USP_GetAllSuppliers";
        public const string USPGetAnalyzerDetailsById = "USP_GetAnalyzerDetailsById";
        public const string UspGetAnalyzerTestMapping = "Usp_GetAnalyzerTestMapping";
        public const string UspAddNewLISAnalyzer = "Usp_AddNewLISAnalyzer";
        public const string UspUpdateLISAnalyzerDetails = "Usp_UpdateLISAnalyzerDetails";
        public const string UspDeleteAnalyzerById = "Usp_DeleteAnalyzerById";
        public const string UspGetAnalyzerTestMappingById = "Usp_GetAnalyzerTestMappings";
        public const string UspSaveAnalyzerTestMapping = "Usp_SaveAnalyzerTestMapping";
        public const string UspUpdateAnalyzerTestMapping = "Usp_UpdateAnalyzerTestMapping";
        public const string UspDeleteMappingById = "Usp_DeleteMappingById";

        #endregion

        #region Center master
        public const string UspGetAllCenters = "Usp_GetAllCenters";
        public const string UspGetSalesInCharge = "Usp_GetSalesInCharge";
        public const string UspGetCenterDetailsByCenterCode = "Usp_GetCenterDetailsBy_CenterCode";
        public const string UspAddNewCenter = "Usp_AddNewCenter";
        public const string UspUpdateCenter = "Usp_UpdateCenter";
        public const string UspDeleteCenterByCenterCode = "Usp_DeleteCenterBy_CenterCode";
        public const string USPGetCentreCustomRates = "Get_CentreCustomRates";
        public const string UspUpdateAllTestCenterRates = "Usp_UpdateAllTestCenterRates";

        #endregion

        #region Clinic Master
        public const string UspGetClinicDetails = "Usp_GetClinicDetails";
        public const string UspAddNewClinicDetail = "Usp_AddNewClinicDetail";
        public const string UspUpdateClinicDetail = "Usp_UpdateClinicDetail";
        public const string UspDeleteClinicById = "Usp_DeleteClinicById";
        public const string UspGetClinicDetailsById = "Usp_GetClinicDetailsById";
        #endregion

        #region Client master
        public const string UspGetAllClients = "Usp_GetAllClients";
        public const string UspGetClientById = "Usp_GetClientById";

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
        public const string ParamLowCriticalValue = "@LowCriticalValue";
        public const string ParamHighCriticalValue = "@HighCriticalValue";
        public const string GreaterThanZero = "Referral Id must be greater than zero.";
        public const string ProfileCodeEmpty = "Profile code should not be empty or blank.";
        public const string MappingIdEmpty = "Mapping Id should not be empty or blank.";
        public readonly static string InvaidMappingRequest = "Invaid Mapping Request";
        public readonly static string InvalidUsernamePassword = "Invalid username or password";
        public const string CenterCodeEmpty = "Center code should not be empty or blank.";


        #endregion
    }
}
