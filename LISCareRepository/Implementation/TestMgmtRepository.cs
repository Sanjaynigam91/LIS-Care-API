using LISCareDataAccess.LISCareDbContext;
using LISCareDTO.SampleCollectionPlace;
using LISCareDTO.TestMaster;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;
using LISCareDTO;
using System.Net;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace LISCareRepository.Implementation
{
    public class TestMgmtRepository : ITestMgmtRepository
    {
        private IConfiguration _configuration;
        private LISCareDbContext _dbContext;

        public TestMgmtRepository(IConfiguration configuration, LISCareDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public APIResponseModel<object> DeleteTestByTestCode(string partnerId, string testCode)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(partnerId.ToString()) && !string.IsNullOrEmpty(testCode.ToString()))
                {

                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspDeleteTestRecord;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode, testCode.Trim()));


                    // output parameters
                    SqlParameter outputBitParm = new SqlParameter(ConstantResource.IsSuccess, SqlDbType.Bit)
                    {

                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorParm = new SqlParameter(ConstantResource.IsError, SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorMessageParm = new SqlParameter(ConstantResource.ErrorMsg, SqlDbType.NVarChar, 404)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputBitParm);
                    command.Parameters.Add(outputErrorParm);
                    command.Parameters.Add(outputErrorMessageParm);
                    _dbContext.Database.GetDbConnection().Open();
                    command.ExecuteScalar();
                    OutputParameterModel parameterModel = new OutputParameterModel
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value) ?? string.Empty,
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.Status = parameterModel.IsSuccess;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;

                    }
                }
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }

        public List<TestDepartmentResponse> GetTestDepartmentData(string partnerId)
        {
            List<TestDepartmentResponse> response = new List<TestDepartmentResponse>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspRetrieveTestDepartments;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TestDepartmentResponse testDepartment = new TestDepartmentResponse();
                    testDepartment.TestDepartment = reader[ConstantResource.TestDepartment] != DBNull.Value
                        ? Convert.ToString(reader[ConstantResource.TestDepartment]) ?? string.Empty
                        : string.Empty;
                    response.Add(testDepartment);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }

        public List<TestDataSearchResponse> GetTestDetails(TestMasterSearchRequest searchRequest)
        {
            List<TestDataSearchResponse> response = new List<TestDataSearchResponse>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetTestMasterData;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, searchRequest.partnerId.Trim()));
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TestDataSearchResponse testSearch = new TestDataSearchResponse();
                    testSearch.TestCode = reader[ConstantResource.Test_Code] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Test_Code]) ?? string.Empty : string.Empty;
                    testSearch.TestName = reader[ConstantResource.TestName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestName]) ?? string.Empty : string.Empty;
                    testSearch.SpecimenType = reader[ConstantResource.SpecimenType] != DBNull.Value ? Convert.ToString(reader[ConstantResource.SpecimenType]) ?? string.Empty : string.Empty;
                    testSearch.ReferenceUnits = reader[ConstantResource.ReferenceUnits] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReferenceUnits]) ?? string.Empty : string.Empty;
                    testSearch.Discipline = reader[ConstantResource.Discipline] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Discipline]) ?? string.Empty : string.Empty;
                    testSearch.ReportingStyle = reader[ConstantResource.ReportingStyle] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReportingStyle]) ?? string.Empty : string.Empty;
                    testSearch.PrintAs = reader[ConstantResource.PrintAs] != DBNull.Value ? Convert.ToString(reader[ConstantResource.PrintAs]) ?? string.Empty : string.Empty;
                    testSearch.AliasName = reader[ConstantResource.AliasName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.AliasName]) ?? string.Empty : string.Empty;
                    testSearch.ReportTemplateTame = reader[ConstantResource.ReportTemplateName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReportTemplateName]) ?? string.Empty : string.Empty;
                    testSearch.SubDiscipline = reader[ConstantResource.SubDiscipline] != DBNull.Value ? Convert.ToString(reader[ConstantResource.SubDiscipline]) ?? string.Empty : string.Empty;
                    testSearch.MRP = reader[ConstantResource.MRP] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.MRP]) : 0;
                    testSearch.B2CRates = reader[ConstantResource.B2CRates] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.B2CRates]) : 0;
                    testSearch.LabRates = reader[ConstantResource.LabRates] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.LabRates]) : 0;
                    response.Add(testSearch);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }

        public List<TestDataSearchResponse> SearchTestDetails(TestMasterSearchRequest searchRequest)
        {
            List<TestDataSearchResponse> response = new List<TestDataSearchResponse>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspSearchTestData;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, searchRequest.partnerId.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamTestName, searchRequest.testName.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamTestStatus, searchRequest.isActive));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamDeptOrDiscipline, searchRequest.deptOrDiscipline.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamIsProcessedAt, searchRequest.isProcessedAt.Trim()));
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TestDataSearchResponse testSearch = new TestDataSearchResponse();
                    testSearch.TestCode = reader[ConstantResource.TestCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestCode]) ?? string.Empty : string.Empty;
                    testSearch.TestName = reader[ConstantResource.TestName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestName]) ?? string.Empty : string.Empty;
                    testSearch.SpecimenType = reader[ConstantResource.SpecimenType] != DBNull.Value ? Convert.ToString(reader[ConstantResource.SpecimenType]) ?? string.Empty : string.Empty;
                    testSearch.ReferenceUnits = reader[ConstantResource.ReferenceUnits] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReferenceUnits]) ?? string.Empty : string.Empty;
                    testSearch.Discipline = reader[ConstantResource.Discipline] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Discipline]) ?? string.Empty : string.Empty;
                    testSearch.ReportingStyle = reader[ConstantResource.ReportingStyle] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReportingStyle]) ?? string.Empty : string.Empty;
                    testSearch.PrintAs = reader[ConstantResource.PrintAs] != DBNull.Value ? Convert.ToString(reader[ConstantResource.PrintAs]) ?? string.Empty : string.Empty;
                    testSearch.AliasName = reader[ConstantResource.AliasName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.AliasName]) ?? string.Empty : string.Empty;
                    testSearch.ReportTemplateTame = reader[ConstantResource.ReportTemplateName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReportTemplateName]) ?? string.Empty : string.Empty;
                    testSearch.SubDiscipline = reader[ConstantResource.SubDiscipline] != DBNull.Value ? Convert.ToString(reader[ConstantResource.SubDiscipline]) ?? string.Empty : string.Empty;
                    response.Add(testSearch);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }

        public TestDataResponse ViewTestData(string partnerId, string testCode)
        {
            TestDataResponse testData = new TestDataResponse();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspRetrieveTestByTestCode;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode, testCode.Trim()));
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    testData.partnerId = reader[ConstantResource.PartnerId] != DBNull.Value ? Convert.ToString(reader[ConstantResource.PartnerId]) ?? string.Empty : string.Empty;
                    testData.testCode = reader[ConstantResource.Test_Code] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Test_Code]) ?? string.Empty : string.Empty;
                    testData.testName = reader[ConstantResource.TestName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestName]) ?? string.Empty : string.Empty;
                    testData.specimenType = reader[ConstantResource.SpecimenType] != DBNull.Value ? Convert.ToString(reader[ConstantResource.SpecimenType]) ?? string.Empty : string.Empty;
                    testData.containerType = reader[ConstantResource.ContainerType] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ContainerType]) ?? string.Empty : string.Empty;
                    testData.specimenVolume = reader[ConstantResource.SpecimenVolume] != DBNull.Value ? Convert.ToString(reader[ConstantResource.SpecimenVolume]) ?? string.Empty : string.Empty;
                    testData.transportConditions = reader[ConstantResource.TransportConditions] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TransportConditions]) ?? string.Empty : string.Empty;
                    testData.discipline = reader[ConstantResource.Discipline] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Discipline]) ?? string.Empty : string.Empty;
                    testData.subDiscipline = reader[ConstantResource.SubDiscipline] != DBNull.Value ? Convert.ToString(reader[ConstantResource.SubDiscipline]) ?? string.Empty : string.Empty;
                    testData.methodology = reader[ConstantResource.Methodology] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Methodology]) ?? string.Empty : string.Empty;
                    testData.analyzerName = reader[ConstantResource.AnalyzerName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.AnalyzerName]) ?? string.Empty : string.Empty;
                    testData.isAutomated = reader[ConstantResource.IsAutomated] != DBNull.Value ? Convert.ToBoolean(reader[ConstantResource.IsAutomated]) : false;
                    testData.isCalculated = reader[ConstantResource.IsCalculated] != DBNull.Value ? Convert.ToBoolean(reader[ConstantResource.IsCalculated]) : false;
                    testData.MRP = reader[ConstantResource.MRP] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.MRP]) : 0;
                    testData.isActive = reader[ConstantResource.TestStatus] != DBNull.Value ? Convert.ToBoolean(reader[ConstantResource.TestStatus]) : false;
                    testData.normalRangeOneline = reader[ConstantResource.NormalRangeOneline] != DBNull.Value ? Convert.ToString(reader[ConstantResource.NormalRangeOneline]) ?? string.Empty : string.Empty;
                    testData.reportTemplateName = reader[ConstantResource.ReportTemplateName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReportTemplateName]) ?? string.Empty : string.Empty;
                    testData.reportingDecimals = reader[ConstantResource.ReportingDecimals] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.ReportingDecimals]) : 0;
                    testData.referenceUnits = reader[ConstantResource.ReferenceUnits] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReferenceUnits]) ?? string.Empty : string.Empty;
                    testData.B2CRates = reader[ConstantResource.B2CRates] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.B2CRates]) : 0;
                    testData.reportingStyle = reader[ConstantResource.ReportingStyle] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReportingStyle]) ?? string.Empty : string.Empty;
                    testData.scheduledDays = reader[ConstantResource.ScheduledDays] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ScheduledDays]) ?? string.Empty : string.Empty;
                    testData.isReserved = reader[ConstantResource.IsReserved] != DBNull.Value ? Convert.ToString(reader[ConstantResource.IsReserved]) ?? string.Empty : string.Empty;
                    testData.isOutlab = reader[ConstantResource.IsOutlab] != DBNull.Value ? Convert.ToBoolean(reader[ConstantResource.IsOutlab]) : false;
                    testData.outlabCode = reader[ConstantResource.OutlabCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.OutlabCode]) ?? string.Empty : string.Empty;
                    testData.reportPrintOrder = reader[ConstantResource.ReportPrintOrder] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.ReportPrintOrder]) : 0;
                    testData.reportSection = reader[ConstantResource.ReportSection] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReportSection]) ?? string.Empty : string.Empty;
                    testData.labRates = reader[ConstantResource.LabRates] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.LabRates]) : 0;
                    testData.lowestAllowed = reader[ConstantResource.LowestAllowed] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.LowestAllowed]) : 0;
                    testData.highestAllowed = reader[ConstantResource.HighestAllowed] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.HighestAllowed]) : 0;
                    testData.technology = reader[ConstantResource.Technology] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Technology]) ?? string.Empty : string.Empty;
                    testData.printAs = reader[ConstantResource.PrintAs] != DBNull.Value ? Convert.ToString(reader[ConstantResource.PrintAs]) ?? string.Empty : string.Empty;
                    testData.cptCode = reader[ConstantResource.CptCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.CptCode]) ?? string.Empty : string.Empty;
                    testData.calculatedValue = reader[ConstantResource.CalculatedValue] != DBNull.Value ? Convert.ToString(reader[ConstantResource.CalculatedValue]) ?? string.Empty : string.Empty;
                    testData.aliasName = reader[ConstantResource.AliasName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.AliasName]) ?? string.Empty : string.Empty;
                    testData.recordId = reader[ConstantResource.RecordId] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.RecordId]) : 0;
                    testData.normalRangeFooter = reader[ConstantResource.NormalRangeFooter] != DBNull.Value ? Convert.ToString(reader[ConstantResource.NormalRangeFooter]) ?? string.Empty : string.Empty;
                    testData.departmentWiseNumbers = reader[ConstantResource.DepartmentWiseNumbers] != DBNull.Value ? Convert.ToString(reader[ConstantResource.DepartmentWiseNumbers]) ?? string.Empty : string.Empty;
                    testData.testShortName = reader[ConstantResource.TestShortName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestShortName]) ?? string.Empty : string.Empty;
                    testData.modality = reader[ConstantResource.Modality] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Modality]) ?? string.Empty : string.Empty;
                    testData.defaultFilmCount = reader[ConstantResource.DefaultFilmCount] != DBNull.Value ? Convert.ToString(reader[ConstantResource.DefaultFilmCount]) ?? string.Empty : string.Empty;
                    testData.defaultContrastML = reader[ConstantResource.DefaultContrastML] != DBNull.Value ? Convert.ToString(reader[ConstantResource.DefaultContrastML]) ?? string.Empty : string.Empty;
                    testData.testProfitRate = reader[ConstantResource.TestProfitRate] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.TestProfitRate]) : 0;
                    testData.labTestCode = reader[ConstantResource.LabTestCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.LabTestCode]) ?? string.Empty : string.Empty;
                    testData.testApplicable = reader[ConstantResource.TestApplicable] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestApplicable]) ?? string.Empty : string.Empty;
                    testData.isLMP = reader[ConstantResource.IsLMP] != DBNull.Value ? Convert.ToBoolean(reader[ConstantResource.IsLMP]) : false;
                    testData.oldtestCode = reader[ConstantResource.OldtestCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.OldtestCode]) ?? string.Empty : string.Empty;
                    testData.isNABLApplicable = reader[ConstantResource.IsNABLApplicable] != DBNull.Value ? Convert.ToBoolean(reader[ConstantResource.IsNABLApplicable]) : false;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            return testData;
        }

        public async Task<ReferalRangeResponse> GetReferalRangeValueAsync(string partnerId, string testCode)
        {
            var referalRange = new ReferalRangeResponse();

            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    await _dbContext.Database.OpenConnectionAsync();

                using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetReferalRangesByTestCode;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode, testCode.Trim()));

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    referalRange.partnerId = reader[ConstantResource.PartnerId] != DBNull.Value ? reader[ConstantResource.PartnerId]?.ToString() ?? string.Empty : string.Empty;
                    referalRange.testCode = reader[ConstantResource.TestCode] != DBNull.Value ? reader[ConstantResource.TestCode]?.ToString() ?? string.Empty : string.Empty;
                    referalRange.referralId = reader[ConstantResource.ReferralId] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.ReferralId]) : 0;
                    referalRange.gender = reader[ConstantResource.Gender] != DBNull.Value ? reader[ConstantResource.Gender]?.ToString() ?? string.Empty : string.Empty;
                    referalRange.lowRange = reader[ConstantResource.LowRange] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.LowRange]) : 0;
                    referalRange.highRange = reader[ConstantResource.HighRange] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.HighRange]) : 0;
                    referalRange.normalRange = reader[ConstantResource.NormalRange] != DBNull.Value ? reader[ConstantResource.NormalRange]?.ToString() ?? string.Empty : string.Empty;
                    referalRange.ageFrom = reader[ConstantResource.AgeFrom] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.AgeFrom]) : 0;
                    referalRange.ageTo = reader[ConstantResource.AgeTo] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.AgeTo]) : 0;
                    referalRange.isPregnant = reader[ConstantResource.IsPregnant] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.IsPregnant]);
                    referalRange.lowCriticalValue = reader[ConstantResource.LowCriticalValue] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.LowCriticalValue]) : 0;
                    referalRange.ageUnits = reader[ConstantResource.AgeUnits] != DBNull.Value ? reader[ConstantResource.AgeUnits]?.ToString() ?? string.Empty : string.Empty;
                    referalRange.highCriticalValue = reader[ConstantResource.HighCriticalValue] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.HighCriticalValue]) : 0;
                    referalRange.labTest = reader[ConstantResource.LabTest] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.LabTest]) : 0;
                }
            }
            catch (Exception ex)
            {
                // log if needed
                throw;
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }

            return referalRange;
        }

        public List<SpecialValueResponse> GetSpecialValue(string partnerId, string testCode)
        {
            List<SpecialValueResponse> specialValue = new List<SpecialValueResponse>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetSpecialValues;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode, testCode.Trim()));
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SpecialValueResponse special = new SpecialValueResponse();
                    special.partnerId = reader[ConstantResource.PartnerId] != DBNull.Value ? Convert.ToString(reader[ConstantResource.PartnerId]) ?? string.Empty : string.Empty;
                    special.testCode = reader[ConstantResource.Test_Code] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Test_Code]) ?? string.Empty : string.Empty;
                    special.testName = reader[ConstantResource.TestName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestName]) ?? string.Empty : string.Empty;
                    special.allowedValue = reader[ConstantResource.AllowedValue] != DBNull.Value ? Convert.ToString(reader[ConstantResource.AllowedValue]) ?? string.Empty : string.Empty;
                    special.isAbnormal = reader[ConstantResource.IsAbnormal] != DBNull.Value ? Convert.ToBoolean(reader[ConstantResource.IsAbnormal]) : false;
                    special.recordId = reader[ConstantResource.RecordId] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.RecordId]) : 0;
                    specialValue.Add(special);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            return specialValue;
        }

        public List<CenterRateResponse> GetCenterRates(string partnerId, string testCode)
        {
            List<CenterRateResponse> centerRatesResponse = new List<CenterRateResponse>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspRetrieveAllCCRates;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode, testCode.Trim()));
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CenterRateResponse centerRate = new CenterRateResponse();
                    centerRate.partnerCode = reader[ConstantResource.PartnerCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.PartnerCode]) ?? string.Empty : string.Empty;
                    centerRate.testCode = reader[ConstantResource.TestCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestCode]) ?? string.Empty : string.Empty;
                    centerRate.partnerName = reader[ConstantResource.PartnerName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.PartnerName]) ?? string.Empty : string.Empty;
                    centerRate.billRate = reader[ConstantResource.BillRate] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.BillRate]) : 0;
                    centerRatesResponse.Add(centerRate);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            return centerRatesResponse;
        }

        public async Task<APIResponseModel<object>> SaveTestDetailsAsync(TestMasterRequest testMasterRequest)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };

            try
            {
                if (testMasterRequest != null)
                {
                    await using var connection = _dbContext.Database.GetDbConnection();
                    await connection.OpenAsync();

                    await using var command = connection.CreateCommand();
                    command.CommandText = ConstantResource.UspSaveTestMasterDetails;
                    command.CommandType = CommandType.StoredProcedure;

                    // input parameters
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, testMasterRequest.partnerId?.Trim() ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode, testMasterRequest.testCode?.Trim() ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestName, testMasterRequest.testName?.Trim() ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamDepartment, testMasterRequest.department?.Trim() ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSubDepartment, testMasterRequest.subDepartment?.Trim() ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamMethodology, testMasterRequest.methodology?.Trim() ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSpecimenType, testMasterRequest.specimenType?.Trim() ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReferenceUnits, testMasterRequest.referenceUnits?.Trim() ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReportingStyle, testMasterRequest.reportingStyle?.Trim() ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReportTemplateName, testMasterRequest.reportTemplateName?.Trim() ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReportingDecimals, testMasterRequest.reportingDecimals));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsOutlab, testMasterRequest.isOutlab));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPrintSequence, testMasterRequest.printSequence));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsReserved, testMasterRequest.isReserved?.Trim() ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestShortName, testMasterRequest.testShortName?.Trim() ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientRate, testMasterRequest.patientRate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClientRate, testMasterRequest.clientRate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabRate, testMasterRequest.labRate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamStatus, testMasterRequest.status));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAnalyzerName, testMasterRequest.analyzerName ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsAutomated, testMasterRequest.isAutomated));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsCalculated, testMasterRequest.isCalculated));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabTestCode, testMasterRequest.labTestCode?.Trim() ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestApplicable, testMasterRequest.testApplicable?.Trim() ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsLMP, testMasterRequest.isLMP));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsNABLApplicable, testMasterRequest.isNABLApplicable));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReferelRangeComments, testMasterRequest.referalRangeComments ?? string.Empty));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamUpdatedBy, testMasterRequest.updatedBy));

                    // output parameters
                    var outputBitParm = new SqlParameter(ConstantResource.IsSuccess, SqlDbType.Bit) { Direction = ParameterDirection.Output };
                    var outputErrorParm = new SqlParameter(ConstantResource.IsError, SqlDbType.Bit) { Direction = ParameterDirection.Output };
                    var outputErrorMessageParm = new SqlParameter(ConstantResource.ErrorMsg, SqlDbType.NVarChar, 404) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(outputBitParm);
                    command.Parameters.Add(outputErrorParm);
                    command.Parameters.Add(outputErrorMessageParm);

                    // async execution
                    await command.ExecuteScalarAsync();

                    var parameterModel = new OutputParameterModel
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value) ?? string.Empty,
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    response.ResponseMessage = parameterModel.ErrorMessage;
                    response.Status = parameterModel.IsSuccess;
                    response.StatusCode = parameterModel.IsSuccess
                                            ? (int)HttpStatusCode.OK
                                            : (int)HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponseModel<object>> UpdateTestDetails(TestMasterRequest testMasterRequest)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(testMasterRequest.ToString()))
                {
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspUpdateTestDetails;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, testMasterRequest.partnerId.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode, testMasterRequest.testCode.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestName, testMasterRequest.testName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamDepartment, testMasterRequest.department.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSubDepartment, testMasterRequest.subDepartment.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamMethodology, testMasterRequest.methodology.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSpecimenType, testMasterRequest.specimenType.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReferenceUnits, testMasterRequest.referenceUnits.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReportingStyle, testMasterRequest.reportingStyle.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReportTemplateName, testMasterRequest.reportTemplateName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReportingDecimals, testMasterRequest.reportingDecimals));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsOutlab, testMasterRequest.isOutlab));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPrintSequence, testMasterRequest.printSequence));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsReserved, testMasterRequest.isReserved.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestShortName, testMasterRequest.testShortName.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientRate, testMasterRequest.patientRate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClientRate, testMasterRequest.clientRate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabRate, testMasterRequest.labRate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamStatus, testMasterRequest.status));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAnalyzerName, testMasterRequest.analyzerName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsAutomated, testMasterRequest.isAutomated));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsCalculated, testMasterRequest.isCalculated));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabTestCode, testMasterRequest.labTestCode.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestApplicable, testMasterRequest.testApplicable.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsLMP, testMasterRequest.isLMP));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsNABLApplicable, testMasterRequest.isNABLApplicable));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReferelRangeComments, testMasterRequest.referalRangeComments));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamUpdatedBy, testMasterRequest.updatedBy));

                    // output parameters
                    SqlParameter outputBitParm = new SqlParameter(ConstantResource.IsSuccess, SqlDbType.Bit)
                    {

                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorParm = new SqlParameter(ConstantResource.IsError, SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorMessageParm = new SqlParameter(ConstantResource.ErrorMsg, SqlDbType.NVarChar, 404)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputBitParm);
                    command.Parameters.Add(outputErrorParm);
                    command.Parameters.Add(outputErrorMessageParm);
                    _dbContext.Database.GetDbConnection().Open();

                    // async execution
                    await command.ExecuteScalarAsync();

                    var parameterModel = new OutputParameterModel
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value) ?? string.Empty,
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    response.ResponseMessage = parameterModel.ErrorMessage;
                    response.Status = parameterModel.IsSuccess;
                    response.StatusCode = parameterModel.IsSuccess
                                            ? (int)HttpStatusCode.OK
                                            : (int)HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }
        /// <summary>
        /// Used to Save or Update Referral Ranges
        /// </summary>
        /// <param name="referralRangesRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> SaveUpdateReferralRanges(ReferralRangesRequest referralRangesRequest)
        {
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(referralRangesRequest.ToString()))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspReferralRangesSaveUpdateChanges;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamOpType, referralRangesRequest.OpType.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReferralId, referralRangesRequest.ReferralId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRefTestCode, referralRangesRequest.TestCode.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLowRange, referralRangesRequest.LowRange));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamHighRange, referralRangesRequest.HighRange));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamNormalRange, referralRangesRequest.NormalRange.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAgeFrom, referralRangesRequest.AgeFrom));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAgeTo, referralRangesRequest.AgeTo));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamGender, referralRangesRequest.Gender.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsPregnant, referralRangesRequest.IsPregnant));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLowCriticalValue, referralRangesRequest.LowCriticalValue));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, referralRangesRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamUpdatedBy, referralRangesRequest.UpdatedBy));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamHighCriticalValue, referralRangesRequest.HighCriticalValue));

                    // output parameters
                    SqlParameter outputBitParm = new SqlParameter(ConstantResource.IsSuccess, SqlDbType.Bit)
                    {

                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorParm = new SqlParameter(ConstantResource.IsError, SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorMessageParm = new SqlParameter(ConstantResource.ErrorMsg, SqlDbType.NVarChar, 404)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputBitParm);
                    command.Parameters.Add(outputErrorParm);
                    command.Parameters.Add(outputErrorMessageParm);

                    await command.ExecuteScalarAsync();
                    OutputParameterModel parameterModel = new OutputParameterModel
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value) ?? string.Empty,
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Status = true;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        response.Status = false;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Status = false;
                response.ResponseMessage = ex.Message;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            return response;
        }
        /// <summary>
        /// Delete Referral Ranges by Referral Id
        /// </summary>
        /// <param name="referralId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> DeleteReferralRanges(int referralId)
        {
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (referralId > 0)
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspDeleteReferralRanges;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReferralId, referralId));

                    // output parameters
                    SqlParameter outputBitParm = new SqlParameter(ConstantResource.IsSuccess, SqlDbType.Bit)
                    {

                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorParm = new SqlParameter(ConstantResource.IsError, SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorMessageParm = new SqlParameter(ConstantResource.ErrorMsg, SqlDbType.NVarChar, 404)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputBitParm);
                    command.Parameters.Add(outputErrorParm);
                    command.Parameters.Add(outputErrorMessageParm);

                    await command.ExecuteScalarAsync();
                    OutputParameterModel parameterModel = new OutputParameterModel
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value) ?? string.Empty,
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Status = parameterModel.IsSuccess;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.GreaterThanZero;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Status = false;
                response.ResponseMessage = ex.Message;
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            return response;
        }
    }
}
