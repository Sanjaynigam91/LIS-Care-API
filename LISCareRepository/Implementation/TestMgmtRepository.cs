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
                    testSearch.testCode = Convert.ToString(reader[ConstantResource.TestCode]);
                    testSearch.testName = Convert.ToString(reader[ConstantResource.TestName]);
                    testSearch.specimenType = Convert.ToString(reader[ConstantResource.SpecimenType]);
                    testSearch.referenceUnits = Convert.ToString(reader[ConstantResource.ReferenceUnits]);
                    testSearch.discipline = Convert.ToString(reader[ConstantResource.Discipline]);
                    testSearch.MRP = Convert.ToInt32(reader[ConstantResource.MRP]);
                    testSearch.B2CRates = Convert.ToInt32(reader[ConstantResource.B2CRates]);
                    testSearch.labRates = Convert.ToInt32(reader[ConstantResource.LabRates]);
                    testSearch.reportingStyle = Convert.ToString(reader[ConstantResource.ReportingStyle]);
                    testSearch.printAs = Convert.ToString(reader[ConstantResource.PrintAs]);
                    testSearch.aliasName = Convert.ToString(reader[ConstantResource.AliasName]);
                    testSearch.reportTemplateTame = Convert.ToString(reader[ConstantResource.ReportTemplateName]);
                    testSearch.subDiscipline = Convert.ToString(reader[ConstantResource.SubDiscipline]);
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
