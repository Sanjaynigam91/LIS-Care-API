using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.ClinicMaster
{
    public class ClinicRequest
    {
        public int ClinicId { get; set; }
        public string ClinicCode { get; set; } = string.Empty;
        public required string ClinicName { get; set; }
        public string ClinicIncharge { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string AlterContactNumber { get; set; } = string.Empty;
        public required string ClinicDoctorCode { get; set; }
        public required string CenterCode { get; set; }
        public string ClinicAddress { get; set; } = string.Empty;
        public string RateType {  get; set; } = string.Empty;
        public bool ClinicStatus {  get; set; } = false;
        public string CreatedBy { get; set; } =string.Empty;
        public string UpdatedBy { get; set;} =string.Empty;
        public string PartnerId {  get; set; } =string.Empty;
    }
}
