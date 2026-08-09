using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.AnalyzerMaster;
using LISCareDTO.RejectedSample;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Implementation
{
    public class RejectionRepository : IRejectionRepository
    {
        private LISCareDbContext dbContext;
        private readonly ILogger<ReportingRepository> logger;

        public RejectionRepository(LISCareDbContext dbContext, ILogger<ReportingRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<APIResponseModel<string>> RejectTestsBeforeAccession(int patientSpecimenId, string testCode, string rejectionReason, string rejectedBy, string partnerId)
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
                if (patientSpecimenId > 0 && !string.IsNullOrEmpty(partnerId))
                {
                    if (dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        dbContext.Database.OpenConnection();
                    var command = dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspAccessionRejectTests;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientSpecimenId, patientSpecimenId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode, testCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRejectionReason, rejectionReason));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRejectedBy, rejectedBy));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId));

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
                    response.ResponseMessage = ConstantResource.ProfileCodeEmpty;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
            }
            finally
            {
                dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            return response;
        }

        public async Task<APIResponseModel<List<RejectedSample>>> GetRejectedSamples(string partnerId, DateTime startDate, DateTime endDate, string barcode, string patientNameOrCode, string centerCode)
        {
            var response = new APIResponseModel<List<RejectedSample>>
            {
                Data = []
            };

            try
            {
                if (string.IsNullOrWhiteSpace(partnerId))
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = "PartnerId cannot be null or empty.";
                }
                else
                {
                    if (dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await dbContext.Database.OpenConnectionAsync();

                    using var cmd = dbContext.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = ConstantResource.UspGetRejectedSampleSummary;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamStartdate, startDate));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamEnddate, endDate));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamBarcode, barcode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientName, patientNameOrCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, centerCode));


                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new RejectedSample
                        {
                            RejectedDate = reader[ConstantResource.RejectedDate] != DBNull.Value
                            ? Convert.ToDateTime(reader[ConstantResource.RejectedDate]).ToString("yyyyMMdd")
                            : DateTime.Now.ToString("yyyyMMdd"),
                            PatientName = reader[ConstantResource.PatientName] as string ?? string.Empty,
                            ReferredDoctor = reader[ConstantResource.ReferredDoctor] as string ?? string.Empty,
                            VisitId = reader[ConstantResource.VisitId] != DBNull.Value ? Convert.ToInt32(reader[ConstantResource.VisitId]) : 0,
                            CenterCode = reader[ConstantResource.CenterCode] as string ?? string.Empty,
                            TestName = reader[ConstantResource.MappedTestName] as string ?? string.Empty,
                            PatientCode = reader[ConstantResource.PatientCode] as string ?? string.Empty,
                            Barcode = reader[ConstantResource.Barcode] as string ?? string.Empty,
                            RejectionReasons = reader[ConstantResource.RejectionReasons] as string ?? string.Empty,
                            ReferredLab = reader[ConstantResource.ReferredLab] as string ?? string.Empty,
                            TestCode = reader[ConstantResource.TestCode] as string ?? string.Empty,
                            CenterName = reader[ConstantResource.CenterrName] as string ?? string.Empty
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Rejected samples retrieved successfully.";
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                // Optionally log the exception here
            }
            finally
            {
                await dbContext.Database.CloseConnectionAsync();
            }

            return response;
        }

    }
}
