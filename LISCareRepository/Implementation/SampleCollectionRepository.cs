using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.SampleCollectionPlace;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;


namespace LISCareRepository.Implementation
{
    public class SampleCollectionRepository : ISampleCollectionRepository
    {
        private IConfiguration _configuration;
        private LISCareDbContext _dbContext;

        public SampleCollectionRepository(IConfiguration configuration, LISCareDbContext _DbContext)
        {
            _configuration=configuration;
            _dbContext=_DbContext;
        }
        public List<SampleCollectedAtResponse> GetSampleCollectedPlace(string partnerId)
        {
            List<SampleCollectedAtResponse> response = new List<SampleCollectedAtResponse>();
            try
            {
                if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    _dbContext.Database.OpenConnection();
                var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetAllSampleCollectedPlaces;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SampleCollectedAtResponse sample = new SampleCollectedAtResponse();
                    sample.PartnerId = Convert.ToString(reader[ConstantResource.PartnerId]);
                    sample.SampleCollectedPlaceName = Convert.ToString(reader[ConstantResource.SampleCollectedAt]);
                    sample.RecordId = Convert.ToInt32(reader[ConstantResource.RecordId]);
                    response.Add(sample);
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
