using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.ClientMaster;
using LISCareDTO.FrontDesk;
using LISCareDTO.Projects;
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
    public class PatientRepository : IPatientRepository
    {
        private readonly LISCareDbContext dbContext;
        private readonly ILogger<PatientRepository> logger;
        public PatientRepository(LISCareDbContext dbContext, ILogger<PatientRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        /// <summary>
        /// used to add tests requested by patient
        /// </summary>
        /// <param name="patientRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> AddTestsRequested(PatientTestRequest testRequest)
        {
            logger.LogInformation($"AddTestsRequested method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(testRequest.PartnerId) || testRequest.PatientId != Guid.Empty)
                {
                    if (dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        dbContext.Database.OpenConnection();
                    var command = dbContext.Database.GetDbConnection().CreateCommand();
                    logger.LogInformation($"UspPatientRegistrationCreateTests execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspPatientRegistrationCreateTests;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientId, testRequest.PatientId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode, testRequest.TestCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsProfile, testRequest.IsProfile));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSpecimenType, testRequest.SpecimenType));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, testRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamOriginalPrice, testRequest.OriginalPrice));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPrice, testRequest.Price));

                    logger.LogInformation($"UspPatientRegistrationCreateTests execution completed at :{DateTime.Now}");
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
                        logger.LogInformation($"UspPatientRegistrationCreateTests execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        logger.LogInformation($"UspPatientRegistrationCreateTests execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    logger.LogInformation($"UspPatientRegistrationCreateTests execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                logger.LogInformation($"UspPatientRegistrationCreateTests execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            logger.LogInformation($"AddTestsRequested method execution completed at :{DateTime.Now}");
            return response;
        }

        /// <summary>
        /// used to add or update patients
        /// </summary>
        /// <param name="patientRequests"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> AddUpdatePatients(PatientRequest patientRequest)
        {
            logger.LogInformation($"AddUpdatePatients method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(patientRequest.PartnerId))
                {
                    if (dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        dbContext.Database.OpenConnection();
                    var command = dbContext.Database.GetDbConnection().CreateCommand();
                    logger.LogInformation($"UspPatientRegistration execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspPatientRegistration;
                    command.CommandType = CommandType.StoredProcedure;

                    if (patientRequest.IsAddPatient == true)
                    {
                        command.Parameters.Add(new SqlParameter(ConstantResource.ParamOpType, ConstantResource.AddNewPatient));
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter(ConstantResource.ParamOpType, ConstantResource.UpdatePatientRecord));
                    }
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientId, patientRequest.PatientId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientCode, patientRequest.PatientCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTittle, patientRequest.Title));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamGender, patientRequest.Gender));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientName, patientRequest.PatientName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAge, patientRequest.Age));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAgeType, patientRequest.AgeType));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmailId, patientRequest.EmailId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamMobileNumber, patientRequest.MobileNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, patientRequest.CenterCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReferredDoctor, patientRequest.ReferredDoctor));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReferredLab, patientRequest.ReferredLab));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsProject, patientRequest.IsProject));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProjectId, patientRequest.ProjectId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamLabInstruction, patientRequest.LabInstruction));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReferalNumber, patientRequest.ReferalNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSampleCollectedAt, patientRequest.SampleCollectedAt));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsPregnant, patientRequest.IsPregnant));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPregnancyWeek, patientRequest.PregnancyWeeks));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPaymentType, patientRequest.PaymentType));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTotalOriginalAmount, patientRequest.TotalOriginalAmount));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamBillAmount, patientRequest.BillAmount));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReceivedAmount, patientRequest.ReceivedAmount));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamBalanceAmount, patientRequest.BalanceAmount));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamDiscountAmount, patientRequest.DiscountAmount));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAgreedRatesBilling, patientRequest.AgreedRatesBilling));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamDiscountStatus, patientRequest.DiscountStatus));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamDiscountRemarks, patientRequest.DiscountRemarks));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientType, patientRequest.PatientType));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEnteredBy, patientRequest.EnteredBy));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamNationality, patientRequest.Nationality));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicalHistory, patientRequest.ClinicalHistory));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicalRemarks, patientRequest.ClinicalRemarks));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsPercentage, patientRequest.IsPercentage));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamInvoiceReceiptNo, patientRequest.InvoiceReceiptNo));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsReportUploaded, patientRequest.IsReportUploaded));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCreatedBy, patientRequest.CreatedBy));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamUpdatedBy, patientRequest.UpdatedBy));

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, patientRequest.PartnerId));
                    logger.LogInformation($"UspPatientRegistration execution completed at :{DateTime.Now}");
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
                        logger.LogInformation($"UspPatientRegistration execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        logger.LogInformation($"UspPatientRegistration execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    logger.LogInformation($"UspPatientRegistration execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                logger.LogInformation($"UspPatientRegistration execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            logger.LogInformation($"AddUpdatePatients method execution completed at :{DateTime.Now}");
            return response;
        }

        /// <summary>
        /// used to get all samples
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="centerCode"></param>
        /// <param name="projectCode"></param>
        /// <param name="testCode"></param>
        /// <param name="TestApplicable"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<TestSampleResponse>>> GetAllSamples(string partnerId, string? centerCode, int projectCode = 0, string? testCode = null, string? testApplicable = null)
        {
            logger.LogInformation($"GetAllSamples, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<TestSampleResponse>>
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
                    logger.LogInformation($"UspGetAllApprovedTests, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetAllApprovedTests;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, centerCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamProjectCode, projectCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamSampleCode, testCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamTestApplicable, testApplicable));

                    using var reader = await cmd.ExecuteReaderAsync();
                    logger.LogInformation($"UspGetAllApprovedTests, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new TestSampleResponse
                        {
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                            SampleCode = reader[ConstantResource.TestCode] as string ?? string.Empty,
                            SampleName = reader[ConstantResource.SampleName] as string ?? string.Empty,
                            ReportingLeadTime = reader[ConstantResource.ReportingLeadTime] as string ?? string.Empty,
                            BillRate = reader[ConstantResource.SampleBillRate] == DBNull.Value ? 0
                            : Convert.ToDecimal(reader[ConstantResource.SampleBillRate]),
                            IsProfile = reader[ConstantResource.IsProfile] == DBNull.Value ? false
                            : Convert.ToBoolean(reader[ConstantResource.IsProfile]),
                            SampleType = reader[ConstantResource.SpecimenType] as string ?? string.Empty,
                            MRP = reader[ConstantResource.MRP] == DBNull.Value ? 0
                            : Convert.ToDecimal(reader[ConstantResource.MRP]),
                            CptCodes = reader[ConstantResource.SampleCptCodes] as string ?? string.Empty,
                            LabRate = reader[ConstantResource.LabRates] == DBNull.Value ? 0
                            : Convert.ToDecimal(reader[ConstantResource.LabRates]),
                            IsRestricted = reader[ConstantResource.IsRestricted] == DBNull.Value ? false
                            : Convert.ToBoolean(reader[ConstantResource.IsRestricted]),
                            PrintAs = reader[ConstantResource.PrintAs] as string ?? string.Empty,
                            Disipline = reader[ConstantResource.Discipline] as string ?? string.Empty,
                            CenterCode = reader[ConstantResource.CenterCode] as string ?? string.Empty,
                            ProjectId = int.TryParse(reader[ConstantResource.ProjectCode]?.ToString(), out var pid)
                            ? pid : 0,
                            IsProject = reader[ConstantResource.IsProject] as string ?? string.Empty,
                            IsCustomRate = reader[ConstantResource.IsCustomRate] as string ?? string.Empty,
                            IsLMP = reader[ConstantResource.IsLMP] == DBNull.Value ? false
                            : Convert.ToBoolean(reader[ConstantResource.IsLMP]),

                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "All sample details retrieved successfully.";
                        logger.LogInformation($"All sample details retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                logger.LogInformation($"GetAllSamples, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await dbContext.Database.CloseConnectionAsync();
            }
            logger.LogInformation($"GetAllSamples, method execution completed at :{DateTime.Now}");
            return response;
        }

        /// <summary>
        /// used to get patient requested test details 
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<TestSampleResponse>>> GetPatientsRequestedTestDetails(Guid patientId, string partnerId)
        {
            logger.LogInformation($"GetPatientsRequestedTestDetails, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<TestSampleResponse>>
            {
                Data = []
            };

            try
            {
                if (string.IsNullOrWhiteSpace(partnerId) && patientId != Guid.Empty)
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
                    logger.LogInformation($"UspRetrieveMasterTests, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspRetrieveMasterTests;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientId, patientId));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));


                    using var reader = await cmd.ExecuteReaderAsync();
                    logger.LogInformation($"UspRetrieveMasterTests, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new TestSampleResponse
                        {
                            SampleCode = reader[ConstantResource.TestCode] as string ?? string.Empty,
                            SampleName = reader[ConstantResource.SampleName] as string ?? string.Empty,
                            ReportingLeadTime = reader[ConstantResource.ReportingLeadTime] as string ?? string.Empty,
                            MRP = reader[ConstantResource.MRP] == DBNull.Value ? 0
                            : Convert.ToDecimal(reader[ConstantResource.MRP]),
                            IsProfile = reader[ConstantResource.IsProfile] == DBNull.Value ? false
                            : Convert.ToBoolean(reader[ConstantResource.IsProfile]),
                            RequestId = reader[ConstantResource.RequestId] == DBNull.Value ? Guid.Empty : Guid.Empty,
                            SampleType = reader[ConstantResource.SpecimenType] as string ?? string.Empty,
                            PatientSpecimenId = reader[ConstantResource.PatientSpecimenId] == DBNull.Value ? 0
                            : Convert.ToInt32(reader[ConstantResource.PatientSpecimenId]),
                            RateType = reader[ConstantResource.RateType] as string ?? string.Empty,
                            BillRate = reader[ConstantResource.Price] == DBNull.Value ? 0
                            : Convert.ToDecimal(reader[ConstantResource.Price]),
                            CenterCode = reader[ConstantResource.CenterCode] as string ?? string.Empty,
                            ImportStatus = reader[ConstantResource.ImportStatus] == DBNull.Value ? false
                            : Convert.ToBoolean(reader[ConstantResource.ImportStatus]),
                            IsRejected = reader[ConstantResource.IsRejected] == DBNull.Value ? false
                            : Convert.ToBoolean(reader[ConstantResource.IsRejected]),
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "All sample details retrieved successfully.";
                        logger.LogInformation($"All sample details retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                logger.LogInformation($"GetPatientsRequestedTestDetails, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await dbContext.Database.CloseConnectionAsync();
            }
            logger.LogInformation($"GetPatientsRequestedTestDetails, method execution completed at :{DateTime.Now}");
            return response;
        }
    }
}
