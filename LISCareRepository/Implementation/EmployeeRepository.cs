using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.ClientMaster;
using LISCareDTO.Employee;
using LISCareDTO.MetaData;
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
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Implementation
{
    public class EmployeeRepository(LISCareDbContext dbContext, ILogger<EmployeeRepository> logger) : IEmployeeRepository
    {
        private readonly LISCareDbContext _dbContext = dbContext;
        private readonly ILogger<EmployeeRepository> _logger = logger;

        /// <summary>
        /// used to add new employee 
        /// </summary>
        /// <param name="employeeRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> AddNewEmployee(EmployeeRequest employeeRequest)
        {
            _logger.LogInformation($"AddNewEmployee method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(employeeRequest.PartnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspAddNewEmployee execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspAddNewEmployee;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmployeeName, employeeRequest.EmployeeName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmailId, employeeRequest.EmailId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.DateOfJoining, employeeRequest.DateOfJoining));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamContactNumber, employeeRequest.ContactNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.MobileNumber, employeeRequest.MobileNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamDepartment, employeeRequest.Department));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamDesignation, employeeRequest.Designation));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamQualification, employeeRequest.Qualification));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRecordStatus, employeeRequest.RecordStatus));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAddress, employeeRequest.Address));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsPathology, employeeRequest.IsPathology));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSignatureImage, employeeRequest.SignatureImage));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, employeeRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCreateBy, employeeRequest.CreatedBy));

                    _logger.LogInformation($"UspAddNewEmployee execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspAddNewEmployee execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspAddNewEmployee execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspAddNewEmployee execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspAddNewEmployee execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"AddNewEmployee method execution completed at :{DateTime.Now}");
            return response;
        }
        
        /// <summary>
        /// used to delete employee details
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> DeleteEmployee(string employeeId, string partnerId)
        {
            _logger.LogInformation($"DeleteEmployee method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(employeeId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspDeleteEmployee execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspDeleteEmployee;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmployeeId, employeeId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId));


                    _logger.LogInformation($"UspDeleteEmployee execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspDeleteEmployee execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspDeleteEmployee execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspDeleteEmployee execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspDeleteEmployee execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"UpdateEmployee method execution completed at :{DateTime.Now}");
            return response;
        }

        /// <summary>
        /// used to get all employess
        /// </summary>
        /// <param name="empStatus"></param>
        /// <param name="department"></param>
        /// <param name="employeeName"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<EmployeeResponse>>> GetAllEmployees(string? empStatus, string? department, string? employeeName, string partnerId)
        {
            _logger.LogInformation($"GetAllEmployees, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<EmployeeResponse>>
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
                    _logger.LogInformation($"UspGetEmployees, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetEmployees;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamEmployeeStatus, empStatus));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamDepartmentName, department));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamEmployeeName, employeeName));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"UspGetEmployees, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new EmployeeResponse
                        {
                            RecordId = reader[ConstantResource.RecordId] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.RecordId])
                            : 0,

                            EmployeeId = reader[ConstantResource.EmployeeId] as string ?? string.Empty,
                            EmployeeName = reader[ConstantResource.EmployeeName] as string ?? string.Empty,
                            EmailId = reader[ConstantResource.EmailId] as string ?? string.Empty,
                            DateOfJoining = reader[ConstantResource.DateOfJoining] != DBNull.Value
                            ? Convert.ToDateTime(reader[ConstantResource.DateOfJoining])
                            : DateTime.Now,


                            ContactNumber = reader[ConstantResource.ContactNumber] as string ?? string.Empty,
                            MobileNumber = reader[ConstantResource.PhoneNumber] as string ?? string.Empty,
                            Department = reader[ConstantResource.Department] as string ?? string.Empty,
                            Designation = reader[ConstantResource.Designation] as string ?? string.Empty,
                            Qualification = reader[ConstantResource.Qualification] as string ?? string.Empty,

                            RecordStatus = reader[ConstantResource.RecordStatus] != DBNull.Value &&
                            (reader[ConstantResource.RecordStatus].ToString() == "1" ||
                            reader[ConstantResource.RecordStatus].ToString().Equals("true", StringComparison.OrdinalIgnoreCase)),

                            Address = reader[ConstantResource.Address] as string ?? string.Empty,

                            IsPathology = reader[ConstantResource.IsPathology] != DBNull.Value &&
                            (reader[ConstantResource.IsPathology].ToString() == "1" ||
                            reader[ConstantResource.IsPathology].ToString().Equals("true", StringComparison.OrdinalIgnoreCase)),

                            SignatureImage = reader[ConstantResource.SignatureImage] as string ?? string.Empty,
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                        });

                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Employee deatils retrieved successfully.";
                        _logger.LogInformation($"All employee details retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"GetAllEmployees, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"GetAllEmployees,method execution completed at :{DateTime.Now}");
            return response;
        }

        /// <summary>
        /// used to get employee by employee Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<EmployeeResponse>>> GetEmployeeById(string employeeId, string partnerId)
        {
            _logger.LogInformation($"GetEmployeeById, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<EmployeeResponse>>
            {
                Data = []
            };

            try
            {
                if (string.IsNullOrWhiteSpace(employeeId))
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = "EmployeeId cannot be null or empty.";
                    _logger.LogInformation($"EmployeeId cannot be null or empty at :{DateTime.Now}");
                }
                else
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await _dbContext.Database.OpenConnectionAsync();

                    using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspGetEmployeeByEmpId, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetEmployeeByEmpId;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamEmployeeId, employeeId.Trim()));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"UspGetEmployeeByEmpId, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new EmployeeResponse
                        {
                            RecordId = reader[ConstantResource.RecordId] != DBNull.Value
                           ? Convert.ToInt32(reader[ConstantResource.RecordId])
                           : 0,

                            EmployeeId = reader[ConstantResource.EmployeeId] as string ?? string.Empty,
                            EmployeeName = reader[ConstantResource.EmployeeName] as string ?? string.Empty,
                            EmailId = reader[ConstantResource.EmailId] as string ?? string.Empty,

                            DateOfJoining = reader[ConstantResource.DateOfJoining] != DBNull.Value
                            ? Convert.ToDateTime(reader[ConstantResource.DateOfJoining])
                            : DateTime.Now,


                            ContactNumber = reader[ConstantResource.ContactNumber] as string ?? string.Empty,
                            MobileNumber = reader[ConstantResource.PhoneNumber] as string ?? string.Empty,
                            Department = reader[ConstantResource.Department] as string ?? string.Empty,
                            Designation = reader[ConstantResource.Designation] as string ?? string.Empty,
                            Qualification = reader[ConstantResource.Qualification] as string ?? string.Empty,

                            RecordStatus = reader[ConstantResource.RecordStatus] != DBNull.Value &&
                           (reader[ConstantResource.RecordStatus].ToString() == "1" ||
                           reader[ConstantResource.RecordStatus].ToString().Equals("true", StringComparison.OrdinalIgnoreCase)),

                            Address = reader[ConstantResource.Address] as string ?? string.Empty,

                            IsPathology = reader[ConstantResource.IsPathology] != DBNull.Value &&
                           (reader[ConstantResource.IsPathology].ToString() == "1" ||
                           reader[ConstantResource.IsPathology].ToString().Equals("true", StringComparison.OrdinalIgnoreCase)),

                            SignatureImage = reader[ConstantResource.SignatureImage] as string ?? string.Empty,
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                        });

                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Employee deatils retrieved successfully.";
                        _logger.LogInformation($"Employee details retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"GetEmployeeById, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"GetEmployeeById,method execution completed at :{DateTime.Now}");
            return response;
        }

        /// <summary>
        /// used to get all department
        /// </summary>
        /// <param name="category"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<MetaDataTagsResponseModel>>> GetEmployeeDepartments(string? category, string partnerId)
        {
            _logger.LogInformation($"GetEmployeeDepartments, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<MetaDataTagsResponseModel>>
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
                    _logger.LogInformation($"UspGetEmployeeDepartments, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetEmployeeDepartments;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCategory, category));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"UspGetEmployeeDepartments, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new MetaDataTagsResponseModel
                        {
                            ItemType = reader[ConstantResource.ItemType] as string ?? string.Empty,
                            ItemDescription = reader[ConstantResource.ItemDescription] as string ?? string.Empty,
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Get Employee Departments retrieved successfully.";
                        _logger.LogInformation($"All employee departments retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"GetEmployeeDepartments, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"GetEmployeeDepartments,method execution completed at :{DateTime.Now}");
            return response;
        }

        /// <summary>
        /// used to update employee details
        /// </summary>
        /// <param name="employeeRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateEmployee(EmployeeRequest employeeRequest)
        {
            _logger.LogInformation($"UpdateEmployee method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(employeeRequest.PartnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspUpdateEmployee execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspUpdateEmployee;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmployeeId, employeeRequest.EmployeeId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmployeeName, employeeRequest.EmployeeName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmailId, employeeRequest.EmailId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.DateOfJoining, employeeRequest.DateOfJoining));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamContactNumber, employeeRequest.ContactNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.MobileNumber, employeeRequest.MobileNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamDepartment, employeeRequest.Department));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamDesignation, employeeRequest.Designation));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamQualification, employeeRequest.Qualification));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRecordStatus, employeeRequest.RecordStatus));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAddress, employeeRequest.Address));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamIsPathology, employeeRequest.IsPathology));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamSignatureImage, employeeRequest.SignatureImage));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, employeeRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamCreateBy, employeeRequest.CreatedBy));

                    _logger.LogInformation($"UspUpdateEmployee execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspUpdateEmployee execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspUpdateEmployee execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspUpdateEmployee execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspUpdateEmployee execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"UpdateEmployee method execution completed at :{DateTime.Now}");
            return response;
        }
    }
}
