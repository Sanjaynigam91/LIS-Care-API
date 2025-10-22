using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.ClientMaster
{
    public class ClientRequest
    {
        public Guid ClientId { get; set; }
        public string PartnerId { get; set; } = string.Empty;
        public string ClientCode { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;  
        public string Speciality {  get; set; } = string.Empty;
        public string LicenseNumber {  get; set; } = string.Empty;
        public string ClientType { get; set; } = string.Empty;
        public string EmailId {  get; set; } = string.Empty;
        public string MobileNumber {  get; set; } = string.Empty;
        public string Address {  get; set; } = string.Empty;
        public string CentreCode {  get; set; } = string.Empty;
        public bool ClientStatus { get; set; } = false;


    }
}
