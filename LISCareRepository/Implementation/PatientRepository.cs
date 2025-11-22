using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
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
    public class PatientRepository: IPatientRepository
    {
        private readonly LISCareDbContext dbContext;
        private readonly ILogger<PatientRepository> logger;
        public PatientRepository(LISCareDbContext dbContext,ILogger<PatientRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
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
                            SampleCode= reader[ConstantResource.TestCode] as string ?? string.Empty,
                            SampleName= reader[ConstantResource.SampleName] as string ?? string.Empty,
                            ReportingLeadTime= reader[ConstantResource.ReportingLeadTime] as string ?? string.Empty,
                            BillRate= reader[ConstantResource.SampleBillRate] == DBNull.Value ? 0
                            : Convert.ToDecimal(reader[ConstantResource.SampleBillRate]),
                            IsProfile= reader[ConstantResource.IsProfile] == DBNull.Value ? false
                            : Convert.ToBoolean(reader[ConstantResource.IsProfile]),
                            SampleType= reader[ConstantResource.SpecimenType] as string ?? string.Empty,
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
                            ? pid: 0,
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
    }
}
