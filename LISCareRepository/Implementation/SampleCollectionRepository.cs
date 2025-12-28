using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.SampleCollectionPlace;
using LISCareDTO.SampleManagement;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using System.Net;


namespace LISCareRepository.Implementation
{
    public class SampleCollectionRepository : ISampleCollectionRepository
    {
        private LISCareDbContext dbContext;
        private readonly ILogger<SampleCollectionRepository> logger;

        public SampleCollectionRepository(LISCareDbContext dbContext, ILogger<SampleCollectionRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        public List<SampleCollectedAtResponse> GetSampleCollectedPlace(string partnerId)
        {
            List<SampleCollectedAtResponse> response = new List<SampleCollectedAtResponse>();
            try
            {
                if (dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    dbContext.Database.OpenConnection();
                var cmd = dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResource.UspGetAllSampleCollectedPlaces;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SampleCollectedAtResponse sample = new SampleCollectedAtResponse();
                    sample.PartnerId = Convert.ToString(reader[ConstantResource.PartnerId]) ?? string.Empty;
                    sample.SampleCollectedPlaceName = Convert.ToString(reader[ConstantResource.SampleCollectedAt]) ?? string.Empty;
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
                dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }

        public APIResponseModel<object> AddSampleCollectedPlaces(SampleCollectedRequest sampleCollected)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = (int)(HttpStatusCode.BadRequest),
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(sampleCollected.ToString()))
                {
                    var command = dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspAddSampleCollectedPlaces;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, sampleCollected.PartnerId.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSamplePlace, sampleCollected.SampleColletedPlace.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamUpdatedBy, sampleCollected.UpdatedBy.Trim()));

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
                    dbContext.Database.GetDbConnection().Open();
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
                dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }

        public APIResponseModel<object> RemoveSamplePlace(int recordId, string partnerId)
        {
            var response = new APIResponseModel<object>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (recordId > 0)
                {
                    var command = dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspRemoveSamplePlace;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter(ConstantResource.RecordId, recordId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
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
                    dbContext.Database.GetDbConnection().Open();
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
                dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }
        /// <summary>
        /// used to get pending samples for collection
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="patientCode"></param>
        /// <param name="centerCode"></param>
        /// <param name="patientName"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<SampleCollectionResponse>>> GetPatientsForCollection(DateTime startDate, DateTime endDate, string? patientCode, string? centerCode, string? patientName, string partnerId)
        {
            logger.LogInformation($"GetPatientsForCollection, method execution started at :{DateTime.Now}");
            bool isDataFound = false;
            var response = new APIResponseModel<List<SampleCollectionResponse>>
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
                    logger.LogInformation($"UspGetSampleForCollection, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetSampleForCollection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamStartdate, startDate));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamEnddate, endDate));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientCode, patientCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, centerCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId));


                    using var reader = await cmd.ExecuteReaderAsync();
                    logger.LogInformation($"UspGetSampleForCollection, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        isDataFound = true;
                        SampleCollectionResponse sampleCollection = new SampleCollectionResponse();
                        sampleCollection.PatientId = reader[ConstantResource.PatientId] != DBNull.Value ? (Guid)reader[ConstantResource.PatientId] : Guid.Empty;
                        sampleCollection.PatientCode = reader[ConstantResource.PatientCode] as string ?? string.Empty;
                        sampleCollection.PatientName = reader[ConstantResource.PatientName] as string ?? string.Empty;
                        sampleCollection.MobileNumber = reader[ConstantResource.PhoneNumber] as string ?? string.Empty;
                        sampleCollection.CenterCode = reader[ConstantResource.CenterCode] as string ?? string.Empty;
                        sampleCollection.VisitId = reader[ConstantResource.VisitId] == DBNull.Value ? 0 : Convert.ToInt32(reader[ConstantResource.VisitId]);
                        sampleCollection.WorkOrderDate = reader[ConstantResource.CreatedOn] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader[ConstantResource.CreatedOn]);
                        sampleCollection.ReferDoctor = reader[ConstantResource.ReferredDoctor] as string ?? string.Empty;
                        sampleCollection.EnteredBy = reader[ConstantResource.EnteredBy] as string ?? string.Empty;

                        response.Data.Add(sampleCollection);

                    }
                    if (isDataFound)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "All patient details retrieved successfully for sample collection.";
                        logger.LogInformation($"All patient details retrieved successfully for sample collection at :{DateTime.Now}");
                    }
                    else
                    {
                        response.Status = false;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.ResponseMessage = "No Patient details found for sample collection.";
                        logger.LogInformation($"No Patient details found for sample collection at :{DateTime.Now}");
                    }
                }


            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                logger.LogInformation($"GetPatientsForCollection, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await dbContext.Database.CloseConnectionAsync();
            }
            logger.LogInformation($"GetPatientsForCollection, method execution completed at :{DateTime.Now}");
            return response;
        }
        /// <summary>
        /// used to get samples for collection by patientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<SamplePendingCollectionResponse>>> GetSamplesForCollection(Guid patientId)
        {
            logger.LogInformation($"GetSamplesForCollection, method execution started at :{DateTime.Now}");
            bool isDataFound = false;
            var response = new APIResponseModel<List<SamplePendingCollectionResponse>>
            {
                Data = []
            };

            try
            {
                if (patientId == Guid.Empty)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = "PatientId cannot be null or empty.";
                    response.Data = [];
                }
                else
                {
                    if (dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await dbContext.Database.OpenConnectionAsync();

                    using var cmd = dbContext.Database.GetDbConnection().CreateCommand();
                    logger.LogInformation($"UspGetSamplePendingForCollection, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetSamplePendingForCollection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientId, patientId));

                    using var reader = await cmd.ExecuteReaderAsync();
                    logger.LogInformation($"UspGetSampleForCollection, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        isDataFound = true;
                        SamplePendingCollectionResponse samplePendingCollection = new SamplePendingCollectionResponse();
                        RequestedTest requestedTest = new RequestedTest();

                        samplePendingCollection.RegisteredDate = reader[ConstantResource.CreatedOn] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader[ConstantResource.CreatedOn]);
                        samplePendingCollection.ReferedDoctor = reader[ConstantResource.ReferredDoctor] as string ?? string.Empty;
                        samplePendingCollection.TotalTubes = reader[ConstantResource.TotalTubes] == DBNull.Value ? 0 : Convert.ToInt32(reader[ConstantResource.TotalTubes]);
                        samplePendingCollection.SampleType = reader[ConstantResource.SpecimenTypes] as string ?? string.Empty;
                        samplePendingCollection.Barcode = reader[ConstantResource.Barcode] as string ?? string.Empty;
                        samplePendingCollection.NewBarcode = reader[ConstantResource.NewBarcode] as string ?? string.Empty;
                        samplePendingCollection.SampleCollectionTime = reader[ConstantResource.SampleCollectionTime] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader[ConstantResource.SampleCollectionTime]);
                        samplePendingCollection.WorkOrderStatus = reader[ConstantResource.WOEStatus] as string ?? string.Empty;
                        samplePendingCollection.PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty;
                        samplePendingCollection.PatientCode = reader[ConstantResource.PatientCode] as string ?? string.Empty;
                        samplePendingCollection.PatientId = reader[ConstantResource.PatientId] != DBNull.Value ? (Guid)reader[ConstantResource.PatientId] : Guid.Empty;
                        samplePendingCollection.Lab = reader[ConstantResource.LAB] as string ?? string.Empty;
                        samplePendingCollection.SampleId = reader[ConstantResource.PatientSpecimenId] == DBNull.Value ? 0 : Convert.ToInt32(reader[ConstantResource.PatientSpecimenId]);
                        samplePendingCollection.IsSpecimenCollected = reader[ConstantResource.IsSpecimenCollected] == DBNull.Value ? false : Convert.ToBoolean(reader[ConstantResource.IsSpecimenCollected]);
                        samplePendingCollection.ActualBarcode = reader[ConstantResource.ActualBarcode] as string ?? string.Empty;

                        response.Data.Add(samplePendingCollection);

                    }
                    if (isDataFound)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "All samples retrieved successfully for sample collection.";
                        logger.LogInformation($"All samples retrieved successfully for sample collection at :{DateTime.Now}");
                    }
                    else
                    {
                        response.Status = false;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.ResponseMessage = "No samples found for sample collection.";
                        logger.LogInformation($"No samples found for sample collection at :{DateTime.Now}");
                    }
                }


            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                logger.LogInformation($"GetPatientsForCollection, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await dbContext.Database.CloseConnectionAsync();
            }
            logger.LogInformation($"GetPatientsForCollection, method execution completed at :{DateTime.Now}");
            return response;
        }
        /// <summary>
        /// used to get requested Sample for Collection
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<RequestedTest>>> GetRequestedSampleForCollection(Guid patientId, string? barcode)
        {
            logger.LogInformation($"GetRequestedSampleForCollection, method execution started at :{DateTime.Now}");
            bool isDataFound = false;
            var response = new APIResponseModel<List<RequestedTest>>
            {
                Data = []
            };

            try
            {
                if (patientId == Guid.Empty)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = "PatientId cannot be null or empty.";
                    response.Data = [];
                }
                else
                {
                    if (dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await dbContext.Database.OpenConnectionAsync();

                    using var cmd = dbContext.Database.GetDbConnection().CreateCommand();
                    logger.LogInformation($"UspGetRequsetedSampleCollection, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetRequsetedSampleCollection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientId, patientId));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamBarcode, barcode));

                    using var reader = await cmd.ExecuteReaderAsync();
                    logger.LogInformation($"UspGetRequsetedSampleCollection, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        isDataFound = true;

                        RequestedTest requestedTest = new RequestedTest();

                        requestedTest.TestCode = reader[ConstantResource.TestCode] as string ?? string.Empty;
                        requestedTest.TestName = reader[ConstantResource.MappedTestName] as string ?? string.Empty;
                        requestedTest.SpecimenType = reader[ConstantResource.SpecimenTypes] as string ?? string.Empty;

                        response.Data.Add(requestedTest);

                    }
                    if (isDataFound)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "All samples retrieved successfully for sample collection.";
                        logger.LogInformation($"All samples retrieved successfully for sample collection at :{DateTime.Now}");
                    }
                    else
                    {
                        response.Status = false;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.ResponseMessage = "No samples found for sample collection.";
                        logger.LogInformation($"No samples found for sample collection at :{DateTime.Now}");
                    }
                }


            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                logger.LogInformation($"GetRequestedSampleForCollection, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await dbContext.Database.CloseConnectionAsync();
            }
            logger.LogInformation($"GetRequestedSampleForCollection, method execution completed at :{DateTime.Now}");
            return response;
        }
        /// <summary>
        /// used to update sample status as collection done
        /// </summary>
        /// <param name="sampleCollected"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateSampleStatusAsCollectionDone(SampleRequest sampleRequest)
        {
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)(HttpStatusCode.BadRequest),
                Status = false,
                ResponseMessage = ConstantResource.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(sampleRequest.PatientId.ToString()))
                {
                    var command = dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspUpdateStatusAsSamleCollected;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamBarcode, sampleRequest.Barcode.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCollectionTime, sampleRequest.CollectionTime));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCollectedBy, sampleRequest.CollectedBy.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSpecimenType, sampleRequest.SpecimenType.Trim()));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPatientId, sampleRequest.PatientId));

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
                    dbContext.Database.GetDbConnection().Open();
                    await command.ExecuteScalarAsync();
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
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
            }
            finally
            {
                dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }
    }
}
