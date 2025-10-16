using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.ClinicMaster
{
    public class ClinicResponse
    {
        public int ClinicId { get; set; }
        public string ClinicCode { get; set; } = string.Empty;
        public string ClinicName { get; set; }=string.Empty;
        public string ClinicIncharge { get; set; }=string.Empty;
        public string EmailId { get; set; }=string.Empty;
        public string MobileNumber { get; set; }=string.Empty;
        public string AlternateContactNo { get; set; }=string.Empty;
        public string ClinicDoctorCode { get; set; }=string.Empty;
        public string CenterCode { get; set; }=string.Empty;
        public string ClinicAddress { get; set; }=string.Empty;
        public string RateType { get; set; }=string.Empty;
        public bool ClinicStatus { get; set; }=false;
        public DateTime CreatedOn { get; set; }=DateTime.Now;
        public DateTime UpdatedOn { get; set;}=DateTime.Now;
        public string CreatedBy { get; set; }=string.Empty ;
        public string UpdatedBy { get; set;}=string.Empty ;
        public string PartnerId { get; set;}=string.Empty ;

    }
}
