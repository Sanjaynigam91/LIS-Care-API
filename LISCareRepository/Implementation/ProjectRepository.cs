using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.ClientMaster;
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
    public class ProjectRepository : IProjectRepository
    {
        private readonly LISCareDbContext _dbContext;
        private readonly ILogger<ProjectRepository> _logger;

        public ProjectRepository(LISCareDbContext dbContext, ILogger<ProjectRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// used to add new project
        /// </summary>
        /// <param name="projectRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> AddProject(ProjectRequest projectRequest)
        {
            _logger.LogInformation($"AddProject method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(projectRequest.PartnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspAddNewProject execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspAddNewProject;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProjectName, projectRequest.ProjectName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamContactNo, projectRequest.ContactNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamContactPerson, projectRequest.ContactPerson));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmail, projectRequest.Email));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAlternateEmail, projectRequest.AlternateEmail));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProjectAddress, projectRequest.ProjectAddress));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReferedBy, projectRequest.ReferedBy));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProjectStatus, projectRequest.ProjectStatus));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, projectRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamValidFrom, projectRequest.ValidFrom));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamValidTo, projectRequest.ValidTo));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRateType, projectRequest.RateType));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReceiptPrefix, projectRequest.ReceiptPrefix));

                    _logger.LogInformation($"UspAddNewProject execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspAddNewProject execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspAddNewProject execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspAddNewProject execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspAddNewProject execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"AddProject method execution completed at :{DateTime.Now}");
            return response;
        }
        /// <summary>
        /// used to delete existing project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> DeleteProject(int projectId, string partnerId)
        {
            _logger.LogInformation($"DeleteProject method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (projectId > 0)
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspDeleteProjectById execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspDeleteProjectById;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProjectId, projectId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId));

                    _logger.LogInformation($"UspDeleteProjectById execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspDeleteProjectById execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspDeleteProjectById execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspDeleteProjectById execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspDeleteProjectById execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"DeleteProject method execution completed at :{DateTime.Now}");
            return response;
        }
        /// <summary>
        /// used to get all projects
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="projectStatus"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<ProjectResponse>>> GetAllProjects(string partnerId, bool? projectStatus, string? projectName)
        {
            _logger.LogInformation($"GetAllProjects, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<List<ProjectResponse>>
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
                    _logger.LogInformation($"UspGetAllProjects, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetAllProjects;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamProjectStatus, projectStatus));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamProjectName, projectName));

                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"UspGetAllProjects, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new ProjectResponse
                        {
                            ProjectId = reader[ConstantResource.ProjectId] == DBNull.Value ? 0 
                            : Convert.ToInt32(reader[ConstantResource.ProjectId]),
                            ProjectName = reader[ConstantResource.ProjectName] as string ?? string.Empty,
                            ContactNumber = reader[ConstantResource.ContactNo] as string ?? string.Empty,
                            ContactPerson = reader[ConstantResource.ContactPerson] as string ?? string.Empty,
                            Email = reader[ConstantResource.ProjectEmail] as string ?? string.Empty,
                            AlternateEmail = reader[ConstantResource.AlternateEmail] as string ?? string.Empty,
                            ProjectAddress = reader[ConstantResource.ProjectAddress] as string ?? string.Empty,
                            ReferedBy = reader[ConstantResource.ReferedBy] as string ?? string.Empty,
                            CreatedOn = reader[ConstantResource.CreatedOn] == DBNull.Value? DateTime.MinValue
                            : Convert.ToDateTime(reader[ConstantResource.CreatedOn]),
                            ProjectStatus = reader[ConstantResource.ProjectStatus] != DBNull.Value
                            && Convert.ToBoolean(reader[ConstantResource.ProjectStatus]),
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                            ValidFrom = reader[ConstantResource.ValidFrom] == DBNull.Value ? DateTime.MinValue
                            : Convert.ToDateTime(reader[ConstantResource.ValidFrom]),
                            ValidTo = reader[ConstantResource.ValidTo] == DBNull.Value ? DateTime.MinValue
                            : Convert.ToDateTime(reader[ConstantResource.ValidTo]),
                            RateType = reader[ConstantResource.RateType] as string ?? string.Empty,
                            ReceiptPrefix = reader[ConstantResource.ReceiptPrefix] as string ?? string.Empty,
                            PatientCount = reader[ConstantResource.PatientCount] == DBNull.Value ? 0 : 
                            Convert.ToInt32(reader[ConstantResource.PatientCount]),
                            PatientCountLastUpdatedOn = reader[ConstantResource.PatientCountLastUpdatedOn] == DBNull.Value ? DateTime.MinValue
                            : Convert.ToDateTime(reader[ConstantResource.PatientCountLastUpdatedOn]),
                        });
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "All project details retrieved successfully.";
                        _logger.LogInformation($"All project details retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"GetAllProjects, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"GetAllProjects, method execution completed at :{DateTime.Now}");
            return response;
        }
        /// <summary>
        /// used to get project by id
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<ProjectResponse>> GetProjectById(string partnerId, int projectId)
        {
            _logger.LogInformation($"GetProjectById, method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<ProjectResponse>
            {
                Data = new ProjectResponse()
            };

            try
            {
                if (projectId>0)
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = "Project Id should be greater than zero.";
                }
                else
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        await _dbContext.Database.OpenConnectionAsync();

                    using var cmd = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspGetProjectsById, execution started at :{DateTime.Now}");
                    cmd.CommandText = ConstantResource.UspGetProjectsById;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamProjectId, projectId));
   

                    using var reader = await cmd.ExecuteReaderAsync();
                    _logger.LogInformation($"UspGetProjectsById, execution completed at :{DateTime.Now}");
                    while (await reader.ReadAsync())
                    {
                        ProjectResponse projectResponse = new ProjectResponse();
                        projectResponse.ProjectId = reader[ConstantResource.ProjectId] == DBNull.Value ? 0
                        : Convert.ToInt32(reader[ConstantResource.ProjectId]);
                        projectResponse.ProjectName = reader[ConstantResource.ProjectName] as string ?? string.Empty;
                        projectResponse.ContactNumber = reader[ConstantResource.ContactNo] as string ?? string.Empty;
                        projectResponse.ContactPerson = reader[ConstantResource.ContactPerson] as string ?? string.Empty;
                        projectResponse.Email = reader[ConstantResource.ProjectEmail] as string ?? string.Empty;
                        projectResponse.AlternateEmail = reader[ConstantResource.AlternateEmail] as string ?? string.Empty;
                        projectResponse.ProjectAddress = reader[ConstantResource.ProjectAddress] as string ?? string.Empty;
                        projectResponse.ReferedBy = reader[ConstantResource.ReferedBy] as string ?? string.Empty;
                        projectResponse.CreatedOn = reader[ConstantResource.CreatedOn] == DBNull.Value ? DateTime.MinValue
                        : Convert.ToDateTime(reader[ConstantResource.CreatedOn]);
                        projectResponse.ProjectStatus = reader[ConstantResource.ProjectStatus] != DBNull.Value
                        && Convert.ToBoolean(reader[ConstantResource.ProjectStatus]);
                        projectResponse.PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty;
                        projectResponse.ValidFrom = reader[ConstantResource.ValidFrom] == DBNull.Value ? DateTime.MinValue
                        : Convert.ToDateTime(reader[ConstantResource.ValidFrom]);
                        projectResponse.ValidTo = reader[ConstantResource.ValidTo] == DBNull.Value ? DateTime.MinValue
                        : Convert.ToDateTime(reader[ConstantResource.ValidTo]);
                        projectResponse.RateType = reader[ConstantResource.RateType] as string ?? string.Empty;
                        projectResponse.ReceiptPrefix = reader[ConstantResource.ReceiptPrefix] as string ?? string.Empty;
                        projectResponse.PatientCount = reader[ConstantResource.PatientCount] == DBNull.Value ? 0 :
                        Convert.ToInt32(reader[ConstantResource.PatientCount]);
                        projectResponse.PatientCountLastUpdatedOn = reader[ConstantResource.PatientCountLastUpdatedOn] == DBNull.Value ? DateTime.MinValue
                        : Convert.ToDateTime(reader[ConstantResource.PatientCountLastUpdatedOn]);

                        response.Data = projectResponse;
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = "Project details retrieved successfully.";
                        _logger.LogInformation($"Project details retrieved successfully at :{DateTime.Now}");
                    }
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"GetProjectById, method execution failed at :{DateTime.Now} due to {ex.Message}");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }
            _logger.LogInformation($"GetProjectById, method execution completed at :{DateTime.Now}");
            return response;
        }
        /// <summary>
        /// used to update existing project
        /// </summary>
        /// <param name="projectRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateProject(ProjectRequest projectRequest)
        {
            _logger.LogInformation($"UpdateProject method execution started at :{DateTime.Now}");
            var response = new APIResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Status = false,
                ResponseMessage = ConstantResource.Failed,
                Data = string.Empty
            };
            try
            {
                if (!string.IsNullOrEmpty(projectRequest.PartnerId))
                {
                    if (_dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                        _dbContext.Database.OpenConnection();
                    var command = _dbContext.Database.GetDbConnection().CreateCommand();
                    _logger.LogInformation($"UspUpdateProject execution started at :{DateTime.Now}");
                    command.CommandText = ConstantResource.UspUpdateProject;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProjectId, projectRequest.ProjectId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProjectName, projectRequest.ProjectName));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamContactNo, projectRequest.ContactNumber));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamContactPerson, projectRequest.ContactPerson));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamEmail, projectRequest.Email));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamAlternateEmail, projectRequest.AlternateEmail));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProjectAddress, projectRequest.ProjectAddress));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReferedBy, projectRequest.ReferedBy));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamProjectStatus, projectRequest.ProjectStatus));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, projectRequest.PartnerId));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamValidFrom, projectRequest.ValidFrom));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamValidTo, projectRequest.ValidTo));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamRateType, projectRequest.RateType));
                    command.Parameters.Add(new SqlParameter(ConstantResource.ParamReceiptPrefix, projectRequest.ReceiptPrefix));

                    _logger.LogInformation($"UspUpdateProject execution completed at :{DateTime.Now}");
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
                        _logger.LogInformation($"UspUpdateProject execution successfully completed with response: {response} at :{DateTime.Now}");
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = parameterModel.IsError;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                        _logger.LogInformation($"UspUpdateProject execution failed with response: {response} at :{DateTime.Now}");
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Status = false;
                    response.ResponseMessage = ConstantResource.CenterCodeEmpty;
                    _logger.LogInformation($"UspUpdateProject execution failed with response: {response} at :{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Status = false;
                response.ResponseMessage = ex.Message;
                _logger.LogInformation($"UspUpdateProject execution failed with response {ex.Message} at :{DateTime.Now}");
            }
            finally
            {
                _dbContext.Database.GetDbConnection().Close();
            }
            response.Data = string.Empty;
            _logger.LogInformation($"UpdateProject method execution completed at :{DateTime.Now}");
            return response;
        }
    }
}
