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
                    SqlParameter outputProfileRate = new SqlParameter(ConstantResource.ParamProfitRate, SqlDbType.Decimal)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputBitParm);
                    command.Parameters.Add(outputErrorParm);
                    command.Parameters.Add(outputErrorMessageParm);
                    command.Parameters.Add(outputProfileRate);

                    await command.ExecuteScalarAsync();
                    OutputParameterModel parameterModel = new OutputParameterModel
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value) ?? string.Empty,
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                        ProfitRate = outputProfileRate.Value as decimal? ?? default
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Status = parameterModel.IsSuccess;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        response.Data = $"ProfitRate: {parameterModel.ProfitRate}";
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
                    SqlParameter outputPatientId = new SqlParameter(ConstantResource.ParamRegistrationPatientId, SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputPatientCode = new SqlParameter(ConstantResource.ParamRegistrationPatientCode, SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputVisitId = new SqlParameter(ConstantResource.ParamRegistrationVisitId, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputRegistrationStatus = new SqlParameter(ConstantResource.ParamRegistrationStatus, SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputBitParm);
                    command.Parameters.Add(outputErrorParm);
                    command.Parameters.Add(outputErrorMessageParm);
                    command.Parameters.Add(outputPatientId);
                    command.Parameters.Add(outputPatientCode);
                    command.Parameters.Add(outputVisitId);
                    command.Parameters.Add(outputRegistrationStatus);

                    await command.ExecuteScalarAsync();
                    OutputParameterModel parameterModel = new OutputParameterModel
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value) ?? string.Empty,
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                        PatientId = Guid.TryParse(Convert.ToString(outputPatientId.Value), out var guidValue) ? guidValue : Guid.Empty,
                        PatientCode = Convert.ToString(outputPatientCode.Value) ?? string.Empty,
                        VisitId = outputVisitId.Value as int? ?? default,
                        RegistrationStatus = Convert.ToString(outputRegistrationStatus.Value) ?? string.Empty
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Status = parameterModel.IsSuccess;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        response.Data = $"PatientId: {parameterModel.PatientId}, PatientCode: {parameterModel.PatientCode}, VisitId: {parameterModel.VisitId}, RegistrationStatus: {parameterModel.RegistrationStatus}";
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

        public async Task<APIResponseModel<PatientDetailResponse>> GetPatientDetails(Guid? patientId)
        {
            logger.LogInformation($"GetPatientDetails, method execution started at :{DateTime.Now}");
            bool isDataFound = false;
            var response = new APIResponseModel<PatientDetailResponse>
            {
                Status = false,
                ResponseMessage = "Error",
                Data = new PatientDetailResponse()
            };


            try
            {
                if (!patientId.HasValue || patientId.Value == Guid.Empty)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = "PatientId cannot be null or empty.";
                    response.Data = null;
                }
                else
                {
                    if (dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await dbContext.Database.OpenConnectionAsync();

                    using var cmd = dbContext.Database.GetDbConnection().CreateCommand();
                    logger.LogInformation($"UspGetPatientDetailsByPatientId, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetPatientDetailsByPatientId;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientId, patientId));


                    using var reader = await cmd.ExecuteReaderAsync();
                    logger.LogInformation($"UspGetPatientDetailsByPatientId, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        isDataFound = true;
                        PatientDetailResponse patientDetail = new PatientDetailResponse();

                        patientDetail.Title = reader[ConstantResource.Tittle] as string ?? string.Empty;
                        patientDetail.Gender = reader[ConstantResource.Gender] as string ?? string.Empty;
                        patientDetail.PatientName = reader[ConstantResource.PatientName] as string ?? string.Empty;
                        patientDetail.Age = reader[ConstantResource.Age] == DBNull.Value ? 0
                            : Convert.ToInt32(reader[ConstantResource.Age]);
                        patientDetail.AgeType = reader[ConstantResource.AgeType] as string ?? string.Empty;
                        patientDetail.EmailId = reader[ConstantResource.EmailId] as string ?? string.Empty;
                        patientDetail.MobileNumber = reader[ConstantResource.PhoneNumber] as string ?? string.Empty;
                        patientDetail.CenterCode = reader[ConstantResource.CenterCode] as string ?? string.Empty;
                        patientDetail.ReferredDoctor = reader[ConstantResource.ReferredDoctor] as string ?? string.Empty;
                        patientDetail.PatientType = reader[ConstantResource.PatientType] as string ?? string.Empty;
                        patientDetail.IsProject = reader[ConstantResource.IsProject] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.IsProject]);
                        patientDetail.ProjectId = reader[ConstantResource.ProjectId] == DBNull.Value ? 0
                        : Convert.ToInt32(reader[ConstantResource.ProjectId]);
                        patientDetail.LabInstruction = reader[ConstantResource.LabInstruction] as string ?? string.Empty;
                        patientDetail.ReferralNumber = reader[ConstantResource.ReferredLab] as string ?? string.Empty;
                        patientDetail.SampleCollectedAt = reader[ConstantResource.SampleCollectedAt] as string ?? string.Empty;
                        patientDetail.TotalOriginalAmount = reader[ConstantResource.TotalOriginalAmount] == DBNull.Value ? 0
                           : Convert.ToDecimal(reader[ConstantResource.TotalOriginalAmount]);
                        patientDetail.BillAmount = reader[ConstantResource.BillAmount] == DBNull.Value ? 0
                        : Convert.ToDecimal(reader[ConstantResource.BillAmount]);
                        patientDetail.ReceivedAmount = reader[ConstantResource.ReceivedAmount] == DBNull.Value ? 0
                            : Convert.ToDecimal(reader[ConstantResource.ReceivedAmount]);
                        patientDetail.BalanceAmount = reader[ConstantResource.BalanceAmount] == DBNull.Value ? 0
                        : Convert.ToDecimal(reader[ConstantResource.BalanceAmount]);
                        patientDetail.DiscountAmount = reader[ConstantResource.DiscountAmount] == DBNull.Value ? 0
                        : Convert.ToDecimal(reader[ConstantResource.DiscountAmount]);
                        patientDetail.IsPercentage = reader[ConstantResource.IsPercentage] != DBNull.Value
                            && Convert.ToBoolean(reader[ConstantResource.IsPercentage]);
                        patientDetail.DiscountRemarks = reader[ConstantResource.DiscountRemarks] as string ?? string.Empty;
                        patientDetail.PatientSpecimenId = reader[ConstantResource.PatientSpecimenId] == DBNull.Value ? 0
                        : Convert.ToInt32(reader[ConstantResource.PatientSpecimenId]);
                        patientDetail.Barcode = reader[ConstantResource.Barcode] as string ?? string.Empty;
                        patientDetail.CollectionTime = reader[ConstantResource.CollectionTime] == DBNull.Value ? DateTime.Now
                       : Convert.ToDateTime(reader[ConstantResource.CollectionTime]);
                        patientDetail.WOEStatus = reader[ConstantResource.WOEStatus] as string ?? string.Empty;
                        patientDetail.SpecimenType = reader[ConstantResource.SpecimenTypes] as string ?? string.Empty;
                        patientDetail.PaymentType = reader[ConstantResource.PaymentType] as string ?? string.Empty;
                        patientDetail.VisitId = reader[ConstantResource.VisitId] == DBNull.Value ? 0
                      : Convert.ToInt32(reader[ConstantResource.VisitId]);
                        patientDetail.DiscountStatus = reader[ConstantResource.DiscountStatus] as string ?? string.Empty;
                        patientDetail.PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty;
                        patientDetail.PatientId = reader[ConstantResource.PatientId] == DBNull.Value ? Guid.Empty
                            : (Guid)reader[ConstantResource.PatientId];
                        patientDetail.PatientCode = reader[ConstantResource.PatientCode] as string ?? string.Empty;
                        patientDetail.RegistrationDate = reader[ConstantResource.RegistrationDate] == DBNull.Value ? DateTime.Now
                      : Convert.ToDateTime(reader[ConstantResource.RegistrationDate]);

                        response.Data = patientDetail;

                    }
                    if (isDataFound)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "All patient details retrieved successfully.";
                        logger.LogInformation($"All patient details retrieved successfully at :{DateTime.Now}");
                    }
                    else
                    {
                        response.Status = false;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.ResponseMessage = "No Patient details found.";
                        logger.LogInformation($"No Patient details found at :{DateTime.Now}");
                    }
                }


            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                logger.LogInformation($"GetPatientSummary, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await dbContext.Database.CloseConnectionAsync();
            }
            logger.LogInformation($"GetPatientDetails, method execution completed at :{DateTime.Now}");
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

        /// <summary>
        /// used to get patient summary
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="patientName"></param>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <param name="centerCode"></param>
        /// <param name="status"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<PatientResponse>>> GetPatientSummary(string? barcode, DateTime? startDate, DateTime? endDate, string? patientName, string? patientCode, string? centerCode, string? status, string partnerId)
        {
            logger.LogInformation($"GetPatientSummary, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<PatientResponse>>
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
                    logger.LogInformation($"UspGetPatientsummary, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetPatientsummary;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamBarcode, barcode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamStartdate, startDate));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamEnddate, endDate));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientName, patientName));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientCode, patientCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, centerCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamStatus, status));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));


                    using var reader = await cmd.ExecuteReaderAsync();
                    logger.LogInformation($"UspGetPatientsummary, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new PatientResponse
                        {
                            PatientId = reader[ConstantResource.PatientId] == DBNull.Value ? Guid.Empty
                            : (Guid)reader[ConstantResource.PatientId],
                            PatientCode = reader[ConstantResource.PatientCode] as string ?? string.Empty,
                            PatientName = reader[ConstantResource.PatientName] as string ?? string.Empty,
                            WoeDate = reader[ConstantResource.WoeDate] as string ?? string.Empty,
                            CenterCode = reader[ConstantResource.CenterCode] as string ?? string.Empty,
                            CreatedOn = reader[ConstantResource.CreatedOn] == DBNull.Value ? DateTime.Now
                            : Convert.ToDateTime(reader[ConstantResource.CreatedOn]),
                            Barcode = reader[ConstantResource.Barcode] as string ?? string.Empty,
                            TotalOriginalAmount = reader[ConstantResource.TotalOriginalAmount] == DBNull.Value ? 0
                            : Convert.ToDecimal(reader[ConstantResource.TotalOriginalAmount]),
                            BillAmount = reader[ConstantResource.BillAmount] == DBNull.Value ? 0
                            : Convert.ToDecimal(reader[ConstantResource.BillAmount]),
                            ReceivedAmount = reader[ConstantResource.ReceivedAmount] == DBNull.Value ? 0
                            : Convert.ToDecimal(reader[ConstantResource.ReceivedAmount]),
                            BalanceAmount = reader[ConstantResource.BalanceAmount] == DBNull.Value ? 0
                            : Convert.ToDecimal(reader[ConstantResource.BalanceAmount]),
                            VisitId = reader[ConstantResource.VisitId] == DBNull.Value ? 0
                            : Convert.ToInt32(reader[ConstantResource.VisitId]),
                            RegistrationStatus = reader[ConstantResource.RegistrationStatus] as string ?? string.Empty,
                            ReferredDoctor = reader[ConstantResource.ReferredDoctor] as string ?? string.Empty,
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                            DiscountStatus = reader[ConstantResource.DiscountStatus] as string ?? string.Empty,
                            TestRequested = reader[ConstantResource.TestRequested] as string ?? string.Empty,
                            PatientTestType = reader[ConstantResource.PatientTestType] as string ?? string.Empty,
                            IsProject = reader[ConstantResource.IsProject] != DBNull.Value && Convert.ToBoolean(reader[ConstantResource.IsProject]),
                            InvoiceReceiptNo = reader[ConstantResource.InvoiceReceiptNo] as string ?? string.Empty,
                            RegisteredOn = reader[ConstantResource.RegisteredOn] == DBNull.Value ? DateTime.Now
                            : Convert.ToDateTime(reader[ConstantResource.RegisteredOn]),
                            ReferredLab = reader[ConstantResource.ReferredLab] as string ?? string.Empty,
                            CenterrName = reader[ConstantResource.CenterrName] as string ?? string.Empty,
                            SpecimenType = reader[ConstantResource.SpecimenTypes] as string ?? string.Empty,
                            PartnerType = reader[ConstantResource.PartnerType] as string ?? string.Empty,
                            ReferredBy = reader[ConstantResource.ReferredBy] as string ?? string.Empty,

                        });


                    }
                }
                if (response.Data.Count > 0)
                {
                    response.Status = true;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.ResponseMessage = "All patient details retrieved successfully.";
                    logger.LogInformation($"All patient details retrieved successfully at :{DateTime.Now}");
                }
                else
                {
                    response.Status = false;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.ResponseMessage = "No Patient details found.";
                    logger.LogInformation($"No Patient details found at :{DateTime.Now}");
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                logger.LogInformation($"GetPatientSummary, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await dbContext.Database.CloseConnectionAsync();
            }
            logger.LogInformation($"GetPatientSummary, method execution completed at :{DateTime.Now}");
            return response;
        }
    }
}
