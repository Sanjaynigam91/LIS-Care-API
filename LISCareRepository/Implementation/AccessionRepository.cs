using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.SampleAccession;
using LISCareDTO.SampleManagement;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Implementation
{
    public class AccessionRepository : IAccessionRepository
    {
        private LISCareDbContext dbContext;
        private readonly ILogger<AccessionRepository> logger;

        public AccessionRepository(LISCareDbContext dbContext, ILogger<AccessionRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        /// <summary>
        /// used to get pending sample for accession
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="barcode"></param>
        /// <param name="centerCode"></param>
        /// <param name="patientName"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<PendingAccessionResponse>>> GetPendingSampleForAccession(DateTime startDate, DateTime endDate, string? barcode, string? centerCode, string? patientName, string partnerId)
        {
            logger.LogInformation($"GetPendingSampleForAccession, method execution started at :{DateTime.Now}");
            bool isDataFound = false;
            var response = new APIResponseModel<List<PendingAccessionResponse>>
            {
                Data = []
            };

            try
            {
                if (string.IsNullOrEmpty(partnerId))
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = "PartnerId cannot be null or empty.";
                    response.Data = [];
                }
                else
                {
                    if (dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await dbContext.Database.OpenConnectionAsync();

                    using var cmd = dbContext.Database.GetDbConnection().CreateCommand();
                    logger.LogInformation($"UspAccessionPendingSamples, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspAccessionPendingSamples;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamStartdate, startDate));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamEnddate, endDate));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamBarcode, barcode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, centerCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientName, patientName));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId));

                    using var reader = await cmd.ExecuteReaderAsync();
                    logger.LogInformation($"UspAccessionPendingSamples, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        isDataFound = true;
                        PendingAccessionResponse pendingAccession=new PendingAccessionResponse();

                        pendingAccession.RegisterDate = reader[ConstantResource.CreatedOn] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader[ConstantResource.CreatedOn]);
                        pendingAccession.WorkOrderDate = reader[ConstantResource.WOEDate] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader[ConstantResource.WOEDate]);
                        pendingAccession.PatientName = reader[ConstantResource.PatientName] as string ?? string.Empty;
                        pendingAccession.ReferredBy = reader[ConstantResource.ReferBy] as string ?? string.Empty;
                        pendingAccession.SampleStatus = reader[ConstantResource.SampleStatus] as string ?? string.Empty;
                        pendingAccession.RejectedDetails = reader[ConstantResource.RejectedDetails] as string ?? string.Empty;
                        pendingAccession.VisitId = reader[ConstantResource.VisitId] == DBNull.Value ? 0 : Convert.ToInt32(reader[ConstantResource.VisitId]);
                        pendingAccession.ProjectId = reader[ConstantResource.ProjectId] == DBNull.Value ? 0 : Convert.ToInt32(reader[ConstantResource.ProjectId]);
                        pendingAccession.PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty;
                        pendingAccession.CenterName = reader[ConstantResource.CenterName] as string ?? string.Empty;
                        pendingAccession.CenterCode = reader[ConstantResource.CenterCode] as string ?? string.Empty;
                        pendingAccession.Barcode = reader[ConstantResource.Barcode] as string ?? string.Empty;
                        pendingAccession.SampleType = reader[ConstantResource.SampleType] as string ?? string.Empty;
                        pendingAccession.TestRequested = reader[ConstantResource.TestRequested] as string ?? string.Empty;
                      
                        response.Data.Add(pendingAccession);

                    }
                    if (isDataFound)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "All samples retrieved successfully for sample accession.";
                        logger.LogInformation($"All samples retrieved successfully for sample accession at :{DateTime.Now}");
                    }
                    else
                    {
                        response.Status = false;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.ResponseMessage = "No samples found for sample accession.";
                        logger.LogInformation($"No samples found for sample accession at :{DateTime.Now}");
                    }
                }


            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                logger.LogInformation($"GetPendingSampleForAccession, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await dbContext.Database.CloseConnectionAsync();
            }
            logger.LogInformation($"GetPendingSampleForAccession, method execution completed at :{DateTime.Now}");
            return response;
        }
       
        /// <summary>
        /// used to get sample types by visitId
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<int>> GetLastImported(DateTime woeDate, string partnerId)
        {
            logger.LogInformation($"GetLastImported, method execution started at :{DateTime.Now}");
            bool isDataFound = false;
            var response = new APIResponseModel<int>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = 0
            };

            try
            {
                if (string.IsNullOrEmpty(partnerId))
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = "PartnerId cannot be null or empty.";
                    response.Data =0;
                }
                else
                {
                    if (dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await dbContext.Database.OpenConnectionAsync();

                    using var cmd = dbContext.Database.GetDbConnection().CreateCommand();
                    logger.LogInformation($"UspGetLastImported, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetLastImported;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamWoeDate, woeDate));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId));

                    using var reader = await cmd.ExecuteReaderAsync();
                    logger.LogInformation($"UspGetLastImported, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        isDataFound = true;
                        response.Data= reader[ConstantResource.LastImported] == DBNull.Value ? 0 : Convert.ToInt32(reader[ConstantResource.LastImported]);
                    }
                    if (isDataFound)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "received last imported successfully!";
                        logger.LogInformation($"received last imported successfully at :{DateTime.Now}");
                    }
                    else
                    {
                        response.Status = false;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.ResponseMessage = "No Last Imported found.";
                        logger.LogInformation($"No Last Imported found at :{DateTime.Now}");
                    }
                }


            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                logger.LogInformation($"GetLastImported, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await dbContext.Database.CloseConnectionAsync();
            }
            logger.LogInformation($"GetLastImported, method execution completed at :{DateTime.Now}");
            return response;
        }
      
        /// <summary>
        /// used to get sample types by visitId
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<SampleTypeResponse>>> GetSampleTypesByVisitid(int visitId, string partnerId)
        {
            logger.LogInformation($"GetSampleTypesByVisitid, method execution started at :{DateTime.Now}");
            bool isDataFound = false;
            var response = new APIResponseModel<List<SampleTypeResponse>>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = []
            };

            try
            {
                if (string.IsNullOrEmpty(partnerId))
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = "PartnerId cannot be null or empty.";
                    response.Data = [];
                }
                else
                {
                    if (dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await dbContext.Database.OpenConnectionAsync();

                    using var cmd = dbContext.Database.GetDbConnection().CreateCommand();
                    logger.LogInformation($"UspGetSampleType, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetSampleType;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamVisitId, visitId));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId));

                    using var reader = await cmd.ExecuteReaderAsync();
                    logger.LogInformation($"UspGetSampleType, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        SampleTypeResponse typeResponse=new SampleTypeResponse();
                        isDataFound = true;
                        typeResponse.SampleType = reader[ConstantResource.SampleTypes] as string ?? string.Empty;
                        response.Data.Add(typeResponse);
                    }
                    if (isDataFound)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "received sample type successfully!";
                        logger.LogInformation($"received sample type successfully at :{DateTime.Now}");
                    }
                    else
                    {
                        response.Status = false;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.ResponseMessage = "No sample type found.";
                        logger.LogInformation($"No sample type found at :{DateTime.Now}");
                    }
                }


            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                logger.LogInformation($"GetSampleTypesByVisitid, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await dbContext.Database.CloseConnectionAsync();
            }
            logger.LogInformation($"GetSampleTypesByVisitid, method execution completed at :{DateTime.Now}");
            return response;
        }
    }
}
