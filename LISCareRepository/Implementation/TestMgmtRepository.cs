﻿using LISCareDataAccess.LISCareDbContext;
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

namespace LISCareRepository.Implementation
{
    public class TestMgmtRepository : ITestMgmtRepository
    {
        private IConfiguration _configuration;
        private LISCareDbContext _dbContext;

        public TestMgmtRepository(IConfiguration configuration, LISCareDbContext dbContext)
        {
            _configuration= configuration;
            _dbContext= dbContext;
        }

        public APIResponseModel<object> DeleteTestByTestCode(string partnerId, string testCode)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
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
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value),
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = ConstantResource.DelTestSuccess;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = false;
                        response.ResponseMessage = ConstantResource.Failed;

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
                    testDepartment.TestDepartment = reader[ConstantResource.TestDepartment] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestDepartment]) : string.Empty;
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
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamTestName, searchRequest.testName.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamIsActive, searchRequest.isActive));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamDeptOrDiscipline, searchRequest.deptOrDiscipline.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamisProcessedAt, searchRequest.isProcessedAt.Trim()));
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TestDataSearchResponse testSearch = new TestDataSearchResponse();
                    testSearch.testCode = reader[ConstantResource.TestCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestCode]) : string.Empty;
                    testSearch.testName = reader[ConstantResource.TestName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestName]) : string.Empty;
                    testSearch.specimenType = reader[ConstantResource.SpecimenType] != DBNull.Value ? Convert.ToString(reader[ConstantResource.SpecimenType]) : string.Empty;
                    testSearch.referenceUnits = reader[ConstantResource.ReferenceUnits] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReferenceUnits]) : string.Empty;
                    testSearch.discipline = reader[ConstantResource.Discipline] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Discipline]) : string.Empty;
                    testSearch.MRP = reader[ConstantResource.MRP] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.MRP]) : 0;
                    testSearch.B2CRates = reader[ConstantResource.B2CRates] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.B2CRates]) : 0;
                    testSearch.labRates = reader[ConstantResource.LabRates] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.LabRates]) : 0;
                    testSearch.reportingStyle = reader[ConstantResource.ReportingStyle] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReportingStyle]) : string.Empty;
                    testSearch.printAs = reader[ConstantResource.PrintAs] != DBNull.Value ? Convert.ToString(reader[ConstantResource.PrintAs]) : string.Empty;
                    testSearch.aliasName = reader[ConstantResource.AliasName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.AliasName]) : string.Empty;
                    testSearch.reportTemplateTame = reader[ConstantResource.ReportTemplateName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReportTemplateName]) : string.Empty;
                    testSearch.subDiscipline = reader[ConstantResource.SubDiscipline] != DBNull.Value ? Convert.ToString(reader[ConstantResource.SubDiscipline]) : string.Empty;
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
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamIsActive, searchRequest.isActive));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamDeptOrDiscipline, searchRequest.deptOrDiscipline.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamisProcessedAt, searchRequest.isProcessedAt.Trim()));
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TestDataSearchResponse testSearch = new TestDataSearchResponse();
                    testSearch.testCode = reader[ConstantResource.TestCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestCode]) : string.Empty;
                    testSearch.testName = reader[ConstantResource.TestName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestName]) : string.Empty;
                    testSearch.specimenType = reader[ConstantResource.SpecimenType] != DBNull.Value ? Convert.ToString(reader[ConstantResource.SpecimenType]) : string.Empty;
                    testSearch.referenceUnits = reader[ConstantResource.ReferenceUnits] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReferenceUnits]) : string.Empty;
                    testSearch.discipline = reader[ConstantResource.Discipline] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Discipline]) : string.Empty;
                    testSearch.MRP = reader[ConstantResource.MRP] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.MRP]) : 0;
                    testSearch.B2CRates = reader[ConstantResource.B2CRates] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.B2CRates]) : 0;
                    testSearch.labRates = reader[ConstantResource.LabRates] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.LabRates]) : 0;
                    testSearch.reportingStyle = reader[ConstantResource.ReportingStyle] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReportingStyle]) : string.Empty;
                    testSearch.printAs = reader[ConstantResource.PrintAs] != DBNull.Value ? Convert.ToString(reader[ConstantResource.PrintAs]) : string.Empty;
                    testSearch.aliasName = reader[ConstantResource.AliasName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.AliasName]) : string.Empty;
                    testSearch.reportTemplateTame = reader[ConstantResource.ReportTemplateName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReportTemplateName]) : string.Empty;
                    testSearch.subDiscipline = reader[ConstantResource.SubDiscipline] != DBNull.Value ? Convert.ToString(reader[ConstantResource.SubDiscipline]) : string.Empty;
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
                   
                    testData.partnerId = reader[ConstantResource.PartnerId] != DBNull.Value ? Convert.ToString(reader[ConstantResource.PartnerId]) : string.Empty;
                    testData.testCode = reader[ConstantResource.TestCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestCode]) : string.Empty;
                    testData.testName = reader[ConstantResource.TestName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestName]) : string.Empty;
                    testData.specimenType = reader[ConstantResource.SpecimenType] != DBNull.Value ? Convert.ToString(reader[ConstantResource.SpecimenType]) : string.Empty;
                    testData.containerType = reader[ConstantResource.ContainerType] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ContainerType]) : string.Empty;
                    testData.specimenVolume = reader[ConstantResource.SpecimenVolume] != DBNull.Value ? Convert.ToString(reader[ConstantResource.SpecimenVolume]) : string.Empty;
                    testData.transportConditions = reader[ConstantResource.TransportConditions] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TransportConditions]) : string.Empty;
                    testData.discipline = reader[ConstantResource.Discipline] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Discipline]) : string.Empty;
                    testData.subDiscipline = reader[ConstantResource.SubDiscipline] != DBNull.Value ? Convert.ToString(reader[ConstantResource.SubDiscipline]) : string.Empty;
                    testData.methodology = reader[ConstantResource.Methodology] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Methodology]) : string.Empty;
                    testData.analyzerName = reader[ConstantResource.AnalyzerName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.AnalyzerName]) : string.Empty;
                    testData.isAutomated = reader[ConstantResource.IsAutomated] != DBNull.Value ? Convert.ToBoolean(reader[ConstantResource.IsAutomated]) : false;
                    testData.isCalculated = reader[ConstantResource.IsCalculated] != DBNull.Value ? Convert.ToBoolean(reader[ConstantResource.IsCalculated]) : false;
                    testData.MRP = reader[ConstantResource.MRP] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.MRP]) : 0;
                    testData.isActive = reader[ConstantResource.IsActive] != DBNull.Value ? Convert.ToBoolean(reader[ConstantResource.IsActive]) : false;
                    testData.normalRangeOneline = reader[ConstantResource.NormalRangeOneline] != DBNull.Value ? Convert.ToString(reader[ConstantResource.NormalRangeOneline]) : string.Empty;
                    testData.reportTemplateName = reader[ConstantResource.ReportTemplateName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReportTemplateName]) : string.Empty;
                    testData.reportingDecimals = reader[ConstantResource.ReportingDecimals] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.ReportingDecimals]) :0;
                    testData.referenceUnits = reader[ConstantResource.ReferenceUnits] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReferenceUnits]) : string.Empty;
                    testData.B2CRates = reader[ConstantResource.B2CRates] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.B2CRates]) : 0;
                    testData.reportingStyle = reader[ConstantResource.ReportingStyle] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReportingStyle]) : string.Empty;
                    testData.scheduledDays = reader[ConstantResource.ScheduledDays] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ScheduledDays]) : string.Empty;
                    testData.isReserved = reader[ConstantResource.IsReserved] != DBNull.Value ? Convert.ToString(reader[ConstantResource.IsReserved]) : string.Empty;
                    testData.isOutlab = reader[ConstantResource.IsOutlab] != DBNull.Value ? Convert.ToBoolean(reader[ConstantResource.IsOutlab]) : false;
                    testData.outlabCode = reader[ConstantResource.OutlabCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.OutlabCode]) : string.Empty;
                    testData.reportPrintOrder = reader[ConstantResource.ReportPrintOrder] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.ReportPrintOrder]) : 0;
                    testData.reportSection = reader[ConstantResource.ReportSection] != DBNull.Value ? Convert.ToString(reader[ConstantResource.ReportSection]) : string.Empty;
                    testData.labRates = reader[ConstantResource.LabRates] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.LabRates]) : 0;     
                    testData.lowestAllowed = reader[ConstantResource.LowestAllowed] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.LowestAllowed]) : 0;     
                    testData.highestAllowed = reader[ConstantResource.HighestAllowed] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.HighestAllowed]) : 0;     
                    testData.technology = reader[ConstantResource.Technology] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Technology]) : string.Empty;     
                    testData.printAs = reader[ConstantResource.PrintAs] != DBNull.Value ? Convert.ToString(reader[ConstantResource.PrintAs]) : string.Empty;
                    testData.cptCode = reader[ConstantResource.CptCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.CptCode]) : string.Empty;
                    testData.calculatedValue = reader[ConstantResource.CalculatedValue] != DBNull.Value ? Convert.ToString(reader[ConstantResource.CalculatedValue]) : string.Empty;
                    testData.aliasName = reader[ConstantResource.AliasName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.AliasName]) : string.Empty;
                    testData.recordId = reader[ConstantResource.RecordId] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.RecordId]) : 0;
                    testData.normalRangeFooter = reader[ConstantResource.NormalRangeFooter] != DBNull.Value ? Convert.ToString(reader[ConstantResource.NormalRangeFooter]) : string.Empty;
                    testData.departmentWiseNumbers = reader[ConstantResource.DepartmentWiseNumbers] != DBNull.Value ? Convert.ToString(reader[ConstantResource.DepartmentWiseNumbers]) : string.Empty;
                    testData.testShortName = reader[ConstantResource.TestShortName] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestShortName]) : string.Empty;
                    testData.modality = reader[ConstantResource.Modality] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Modality]) : string.Empty;
                    testData.defaultFilmCount = reader[ConstantResource.DefaultFilmCount] != DBNull.Value ? Convert.ToString(reader[ConstantResource.DefaultFilmCount]) : string.Empty;
                    testData.defaultContrastML = reader[ConstantResource.DefaultContrastML] != DBNull.Value ? Convert.ToString(reader[ConstantResource.DefaultContrastML]) : string.Empty;
                    testData.testProfitRate = reader[ConstantResource.TestProfitRate] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.TestProfitRate]) : 0;
                    testData.labTestCode = reader[ConstantResource.LabTestCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.LabTestCode]) : string.Empty;
                    testData.testApplicable = reader[ConstantResource.TestApplicable] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestApplicable]) : string.Empty;
                    testData.isLMP = reader[ConstantResource.IsLMP] != DBNull.Value ? Convert.ToBoolean(reader[ConstantResource.IsLMP]) : false;
                    testData.oldtestCode = reader[ConstantResource.OldtestCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.OldtestCode]) : string.Empty;
                    testData.isNABLApplicable = reader[ConstantResource.IsNABLApplicable] != DBNull.Value ? Convert.ToBoolean(reader[ConstantResource.IsNABLApplicable]) :false;
                    
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

        public ReferalRangeResponse GetReferalRangeValue(string partnerId, string testCode)
        {
            ReferalRangeResponse referalRange = new ReferalRangeResponse();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetReferalRangesByTestCode;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode, testCode.Trim()));
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    referalRange.partnerId = reader[ConstantResource.PartnerId] != DBNull.Value ? Convert.ToString(reader[ConstantResource.PartnerId]) : string.Empty;
                    referalRange.testCode = reader[ConstantResource.TestCode] != DBNull.Value ? Convert.ToString(reader[ConstantResource.TestCode]) : string.Empty;
                    referalRange.referralId = reader[ConstantResource.ReferralId] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.ReferralId]) : 0;
                    referalRange.gender = reader[ConstantResource.Gender] != DBNull.Value ? Convert.ToString(reader[ConstantResource.Gender]) : string.Empty;
                    referalRange.lowRange = reader[ConstantResource.LowRange] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.LowRange]) : 0;
                    referalRange.highRange = reader[ConstantResource.HighRange] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.HighRange]) : 0;
                    referalRange.normalRange = reader[ConstantResource.NormalRange] != DBNull.Value ? Convert.ToString(reader[ConstantResource.NormalRange]) : string.Empty;
                    referalRange.ageFrom = reader[ConstantResource.AgeFrom] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.AgeFrom]) : 0;
                    referalRange.ageTo = reader[ConstantResource.AgeTo] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.AgeTo]) : 0;
                    referalRange.isPregnant = reader[ConstantResource.IsPregnant] != DBNull.Value ? Convert.ToBoolean(reader[ConstantResource.IsPregnant]) : false;
                    referalRange.lowCriticalValue = reader[ConstantResource.CriticalValue] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.CriticalValue]) : 0;
                    referalRange.ageUnits = reader[ConstantResource.AgeUnits] != DBNull.Value ? Convert.ToString(reader[ConstantResource.AgeUnits]) : string.Empty;
                    referalRange.highCriticalValue = reader[ConstantResource.HighCriticalValue] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResource.HighCriticalValue]) : 0;
                    referalRange.labTest = reader[ConstantResource.LabTest] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.LabTest]) : 0;

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
            return referalRange;
        }

    }
}
