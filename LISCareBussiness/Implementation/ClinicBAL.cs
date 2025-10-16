using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.ClinicMaster;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class ClinicBAL(IClinicRepository clinicRepository) : IClinc
    {
        private readonly IClinicRepository _clinicRepository= clinicRepository;
        public async Task<APIResponseModel<List<ClinicResponse>>> GetAllClinicDetails(string? partnerId, string? centerCode, string? clinicStatus, string? searchBy = "")
        {
            try
            {
                return await _clinicRepository.GetAllClinicDetails(partnerId, centerCode,clinicStatus, searchBy);
            }
            catch
            {
                throw;
            }
        }
    }
}
