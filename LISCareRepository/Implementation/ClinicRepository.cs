using LISCareDataAccess.LISCareDbContext;
using LISCareDTO;
using LISCareDTO.CenterMaster;
using LISCareDTO.ClinicMaster;
using LISCareRepository.Interface;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Implementation
{
    public class ClinicRepository(LISCareDbContext dbContext):IClinicRepository
    {
        private readonly LISCareDbContext _dbContext = dbContext;

        public async Task<APIResponseModel<List<ClinicResponse>>> GetAllClinicDetails(string? partnerId, string? centerCode, string? clinicStatus, string? searchBy = "")
        {
            var response = new APIResponseModel<List<ClinicResponse>>
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
                    cmd.CommandText = ConstantResource.UspGetClinicDetails;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamCentreCode, centerCode));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamClinicStatus, clinicStatus));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParamSearchBy, searchBy));
                    cmd.Parameters.Add(new SqlParameter(ConstantResource.ParmPartnerId, partnerId.Trim()));

                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new ClinicResponse
                        {
                            ClinicId = reader[ConstantResource.ClinicId] != DBNull.Value
                            ? Convert.ToInt32(reader[ConstantResource.ClinicId]) : 0,
                            ClinicCode = reader[ConstantResource.ClinicCode] as string ?? string.Empty,
                            ClinicName = reader[ConstantResource.ClinicName] as string ?? string.Empty,
                            ClinicIncharge = reader[ConstantResource.ClinicIncharge] as string ?? string.Empty,
                            MobileNumber = reader[ConstantResource.CenterMobileNumber] as string ?? string.Empty,
                            EmailId = reader[ConstantResource.EmailId] as string ?? string.Empty,
                            AlternateContactNo = reader[ConstantResource.AlternateContactNo] as string ?? string.Empty,
                            RateType = reader[ConstantResource.RateType] as string ?? string.Empty,
                            ClinicDoctorCode = reader[ConstantResource.ClinicDoctorCode] as string ?? string.Empty,
                            CenterCode = reader[ConstantResource.CenterCode] as string ?? string.Empty,
                            ClinicAddress = reader[ConstantResource.ClinicAddress] as string ?? string.Empty,
                            ClinicStatus = reader[ConstantResource.ClinicStatus] != DBNull.Value
                            && Convert.ToBoolean(reader[ConstantResource.ClinicStatus]),
                            PartnerId = reader[ConstantResource.PartnerId] as string ?? string.Empty,
                            CreatedBy = reader[ConstantResource.CreatedBy] as string ?? string.Empty,
                            CreatedOn = reader[ConstantResource.CreatedOn] != DBNull.Value
                            ? Convert.ToDateTime(reader[ConstantResource.CreatedOn])
                            : DateTime.MinValue,
                            UpdatedOn = reader[ConstantResource.CreatedOn] != DBNull.Value
                            ? Convert.ToDateTime(reader[ConstantResource.CreatedOn])
                            : DateTime.MinValue,
                            UpdatedBy = reader[ConstantResource.UpdateBy] as string ?? string.Empty,

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
    }
}
