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
    }
}
