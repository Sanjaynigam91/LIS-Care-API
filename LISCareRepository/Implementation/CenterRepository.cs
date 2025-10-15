using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.AnalyzerMaster;
using LISCareDTO.CenterMaster;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Implementation
{
    public class CenterRepository(LISCareDbContext dbContext) : ICenterRepository
    {
        private readonly LISCareDbContext _dbContext = dbContext;
        /// <summary>
        /// used to create new center
        /// </summary>
        /// <param name="centerRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> CreateNewCenter(CenterRequest centerRequest)
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
                if (!string.IsNullOrEmpty(centerRequest.CenterCode))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspAddNewCenter;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, centerRequest.CenterCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterName, centerRequest.CenterName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterInchargeName, centerRequest.CenterInchargeName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSalesIncharge, centerRequest.SalesIncharge));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterAddress, centerRequest.CenterAddress));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPinCode, centerRequest.Pincode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterMobileNumber, centerRequest.MobileNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAlternateContactNo, centerRequest.AlternateContactNo));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmailId, centerRequest.EmailId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRateType, centerRequest.RateType));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterStatus, centerRequest.CenterStatus));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIntroducedBy, centerRequest.IntroducedBy));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCreditLimit, centerRequest.CreditLimit));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsAutoBarcode, centerRequest.IsAutoBarcode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, centerRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCreateBy, centerRequest.CreatedBy));

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
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
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
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            return response;
        }
        /// <summary>
        /// used to delete center by center code
        /// </summary>
        /// <param name="centerCode"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> DeleteCenter(string? centerCode, string? partnerId)
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
                if (!string.IsNullOrEmpty(centerCode))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspDeleteCenterByCenterCode;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, centerCode));
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
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
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
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            return response;
        }

        /// <summary>
        /// used to get all center details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="centerStatus"></param>
        /// <param name="searchBy"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<CenterResponse>>> GetAllCenterDetails(string? partnerId, string? centerStatus = "", string? searchBy = "")
        {
            var response = new APIResponseModel<List<CenterResponse>>
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
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await _dbContext.Database.OpenConnectionAsync();

                    using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = ConstantResource.UspGetAllCenters;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterStatus, centerStatus));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamSearchBy, searchBy));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new CenterResponse
                        {
                           
                            CenterCode = reader[ConstantResource.CenterCode] as string ?? string.Empty,
                            CenterName = reader[ConstantResource.CenterName] as string ?? string.Empty,
                            CenterInchargeName = reader[ConstantResource.CenterInchargeName] as string ?? string.Empty,
                            SalesIncharge = reader[ConstantResource.SalesIncharge] as string ?? string.Empty,
                            CenterAddress = reader[ConstantResource.CenterAddress] as string ?? string.Empty,
                            Pincode = reader[ConstantResource.PinCode] as string ?? string.Empty,
                            MobileNumber = reader[ConstantResource.CenterMobileNumber] as string ?? string.Empty,
                            EmailId = reader[ConstantResource.EmailId] as string ?? string.Empty,
                            AlternateContactNo = reader[ConstantResource.AlternateContactNo] as string ?? string.Empty,
                            RateType = reader[ConstantResource.RateType] as string ?? string.Empty,
                            CenterStatus = reader[ConstantResource.CenterStatus] != DBNull.Value
                            && Convert.ToBoolean(reader[ConstantResource.CenterStatus]),
                            IntroducedBy = reader[ConstantResource.IntroducedBy] as string ?? string.Empty,
                            CreditLimit = reader[ConstantResource.CreditLimit] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.CreditLimit]): 0,
                            IsAutoBarcode = reader[ConstantResource.IsAutoBarcode] != DBNull.Value
                            && Convert.ToBoolean(reader[ConstantResource.IsAutoBarcode]),
                            City = reader[ConstantResource.City] as string ?? string.Empty,
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Cetner details retrieved successfully.";
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
                await _dbContext.Database.CloseConnectionAsync();
            }

            return response;
        }
        /// <summary>
        /// used to get center details by center code
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="centerCode"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<CenterResponse>>> GetCenterByCenterCode(string? partnerId, string? centerCode)
        {
            var response = new APIResponseModel<List<CenterResponse>>
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
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await _dbContext.Database.OpenConnectionAsync();

                    using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = ConstantResource.UspGetCenterDetailsByCenterCode;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, centerCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new CenterResponse
                        {

                            CenterCode = reader[ConstantResource.CenterCode] as string ?? string.Empty,
                            CenterName = reader[ConstantResource.CenterName] as string ?? string.Empty,
                            CenterInchargeName = reader[ConstantResource.CenterInchargeName] as string ?? string.Empty,
                            SalesIncharge = reader[ConstantResource.SalesIncharge] as string ?? string.Empty,
                            CenterAddress = reader[ConstantResource.CenterAddress] as string ?? string.Empty,
                            Pincode = reader[ConstantResource.PinCode] as string ?? string.Empty,
                            MobileNumber = reader[ConstantResource.CenterMobileNumber] as string ?? string.Empty,
                            EmailId = reader[ConstantResource.EmailId] as string ?? string.Empty,
                            AlternateContactNo = reader[ConstantResource.AlternateContactNo] as string ?? string.Empty,
                            RateType = reader[ConstantResource.RateType] as string ?? string.Empty,
                            CenterStatus = reader[ConstantResource.CenterStatus] != DBNull.Value
                            && Convert.ToBoolean(reader[ConstantResource.CenterStatus]),
                            IntroducedBy = reader[ConstantResource.IntroducedBy] as string ?? string.Empty,
                            CreditLimit = reader[ConstantResource.CreditLimit] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.CreditLimit]) : 0,
                            IsAutoBarcode = reader[ConstantResource.IsAutoBarcode] != DBNull.Value
                            && Convert.ToBoolean(reader[ConstantResource.IsAutoBarcode]),
                            City = reader[ConstantResource.City] as string ?? string.Empty,
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Cetner details retrieved successfully.";
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
                await _dbContext.Database.CloseConnectionAsync();
            }

            return response;
        }

        public async Task<APIResponseModel<List<CentreCustomRateResponse>>> GetCentreCustomRates(string? opType, string? centerCode, string? partnerId, string? testCode)
        {
            var response = new APIResponseModel<List<CentreCustomRateResponse>>
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
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await _dbContext.Database.OpenConnectionAsync();

                    using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = ConstantResource.USPGetCentreCustomRates;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamOpType,
                    string.IsNullOrEmpty(opType) ? (object)DBNull.Value : opType));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode,
                    string.IsNullOrEmpty(centerCode) ? (object)DBNull.Value : centerCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId,
                    string.IsNullOrEmpty(partnerId) ? (object)DBNull.Value : partnerId));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode,
                    string.IsNullOrEmpty(testCode) ? (object)DBNull.Value : testCode));

                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new CentreCustomRateResponse
                        {
                            MappingId = reader[ConstantResource.MappingId] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.MappingId]): 0,
                            CenterCode = reader[ConstantResource.CentreCode] as string ?? string.Empty,
                            CenterName = reader[ConstantResource.CentreName] as string ?? string.Empty,
                            TestCode = reader[ConstantResource.TestCode] as string ?? string.Empty,
                            TestName = reader[ConstantResource.CentreTestName] as string ?? string.Empty,
                            CustomRate = reader[ConstantResource.AgreedRate] != DBNull.Value
                            ? Convert.ToDecimal(reader[ConstantResource.AgreedRate]) : 0,
                            Mrp= reader[ConstantResource.MRP] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.MRP]) : 0,
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Custom rates retrieved successfully.";
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
                await _dbContext.Database.CloseConnectionAsync();
            }

            return response;
        }

        /// <summary>
        /// used to get sales incharge details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<SalesInchargeResponse>>> GetSalesInchargeDetails(string? partnerId)
        {
            var response = new APIResponseModel<List<SalesInchargeResponse>>
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
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await _dbContext.Database.OpenConnectionAsync();

                    using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = ConstantResource.UspGetSalesInCharge;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new SalesInchargeResponse
                        {
                            SalesInchargeId = reader[ConstantResource.EmployeeId] as string ?? string.Empty,
                            SalesInchargeName = reader[ConstantResource.EmployeeName] as string ?? string.Empty
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Sales Incharge details retrieved successfully.";
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
                await _dbContext.Database.CloseConnectionAsync();
            }

            return response;
        }
        /// <summary>
        /// used to import center rates
        /// </summary>
        /// <param name="centerRates"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> ImportCentreTestRates(CenterRatesRequest centerRates)
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
                if (!string.IsNullOrEmpty(centerRates.CenterCode))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspInsertCenterTestRates;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, centerRates.CenterCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, centerRates.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode, centerRates.TestCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamBillRate, centerRates.BillRate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRateCreatedBy, centerRates.CreatedBy));

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
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
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
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            return response;
        }

        /// <summary>
        /// used to update center
        /// </summary>
        /// <param name="centerRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateCenter(CenterRequest centerRequest)
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
                if (!string.IsNullOrEmpty(centerRequest.CenterCode))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspUpdateCenter;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, centerRequest.CenterCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterName, centerRequest.CenterName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterInchargeName, centerRequest.CenterInchargeName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSalesIncharge, centerRequest.SalesIncharge));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterAddress, centerRequest.CenterAddress));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamPinCode, centerRequest.Pincode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterMobileNumber, centerRequest.MobileNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAlternateContactNo, centerRequest.AlternateContactNo));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmailId, centerRequest.EmailId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRateType, centerRequest.RateType));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterStatus, centerRequest.CenterStatus));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIntroducedBy, centerRequest.IntroducedBy));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCreditLimit, centerRequest.CreditLimit));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsAutoBarcode, centerRequest.IsAutoBarcode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, centerRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamUpdateBy, centerRequest.ModifiedBy));

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
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
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
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            return response;
        }
        /// <summary>
        /// used to update all test rate for the center
        /// </summary>
        /// <param name="centerRates"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateCentersRates(CenterRatesRequest centerRates)
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
                if (!string.IsNullOrEmpty(centerRates.CenterCode))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResource.UspUpdateAllTestCenterRates;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCenterCode, centerRates.CenterCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, centerRates.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamTestCode, centerRates.TestCode));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamBillRate, centerRates.BillRate));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRateCreatedBy, centerRates.CreatedBy));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamModifiedBy, centerRates.UpdatedBy));

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
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
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
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            return response;
        }
    }
}
