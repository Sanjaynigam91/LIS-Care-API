using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.Reporting;
using LISCareDTO.SampleCollectionPlace;
using LISCareDTO.SampleManagement;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Implementation
{
    public class ReportingRepository : IReportingRepository
    {
        private LISCareDbContext dbContext;
        private readonly ILogger<ReportingRepository> logger;
        public ReportingRepository(LISCareDbContext dbContext, ILogger<ReportingRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        /// <summary>
        /// used to retrieve pending patients based on various filters for test entry
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<PendingPatientResponse>>> RetrievePendingPatients(string partnerId, DateTime startDate, DateTime endDate, string? barcode,
            string? department, string? patientName, string? centerCode, string reportStatus)
        {
            logger.LogInformation($"RetrievePendingPatients, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<PendingPatientResponse>>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = []
            };
            try
            {
                if (dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    dbContext.Database.OpenConnection();
                var cmd = dbContext.Database.GetDbConnection().CreateCommand();
                logger.LogInformation($"UspResultsMasterRetrieveDetails, execution started at :{DateTime.Now}");
                cmd.CommandText = ConstantResource.UspResultsMasterRetrieveDetails;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamStartdate, startDate));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamEnddate, endDate));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamBarcode, barcode));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamDepartment, department));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientName, patientName));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, centerCode));
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamReportStatus, reportStatus.Trim()));
                using var reader = await cmd.ExecuteReaderAsync();
                logger.LogInformation($"UspResultsMasterRetrieveDetails, execution completed at :{DateTime.Now}");
                while (reader.Read())
                {
                    PendingPatientResponse pendingPatient = new PendingPatientResponse();

                    pendingPatient.WorkOrderDate = Convert.ToString(reader[ConstantResource.WorkOrderDate]) ?? string.Empty;
                    pendingPatient.CenterCode = Convert.ToString(reader[ConstantResource.CenterCode]) ?? string.Empty;
                    pendingPatient.PatientName = Convert.ToString(reader[ConstantResource.PatientName]) ?? string.Empty;
                    pendingPatient.PatientCode = Convert.ToString(reader[ConstantResource.PatientCode]) ?? string.Empty;
                    pendingPatient.Departments = Convert.ToString(reader[ConstantResource.Departments]) ?? string.Empty;
                    pendingPatient.BarcodeIds = Convert.ToString(reader[ConstantResource.BarcodeIds]) ?? string.Empty;
                    pendingPatient.TestProfiles = Convert.ToString(reader[ConstantResource.TestProfiles]) ?? string.Empty;
                    pendingPatient.NewWorkOrderDate = Convert.ToDateTime(reader[ConstantResource.NewWorkOrderDate]);
                    pendingPatient.ReferredBy = Convert.ToString(reader[ConstantResource.ReferredByDr]) ?? string.Empty;
                    pendingPatient.ClinicFileNumber = Convert.ToInt32(reader[ConstantResource.ClinicFileNumber]);
                    pendingPatient.VisitId = Convert.ToInt32(reader[ConstantResource.VisitId]);
                    pendingPatient.CenterName = Convert.ToString(reader[ConstantResource.CenterName]) ?? string.Empty;
                    pendingPatient.SampleType = Convert.ToString(reader[ConstantResource.SampleType]) ?? string.Empty;
                    response.Data.Add(pendingPatient);
                }

                if (response.Data.Count > 0)
                {
                    response.Status = true;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.ResponseMessage = $"RetrievePendingPatients, retrieved {response.Data.Count} records.";
                    logger.LogInformation($"RetrievePendingPatients, retrieved {response.Data.Count} records.");
                }
                else
                {
                    response.Status = false;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.ResponseMessage = $"RetrievePendingPatients, No data available.";
                    logger.LogInformation($"RetrievePendingPatients, No data available.");
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                dbContext.Database.GetDbConnection().Close();
            }
            logger.LogInformation($"RetrievePendingPatients, method execution completed at :{DateTime.Now}");
            return response;
        }
    }
}
