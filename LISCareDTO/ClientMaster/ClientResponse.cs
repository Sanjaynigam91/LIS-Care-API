using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.ClientMaster
{
    public class ClientResponse
    {
        public string ClientId {  get; set; }=string.Empty;
        public string PartnerId {  get; set; }=string.Empty;
        public string ClientCode {  get; set; }=string.Empty;
        public string CenterName {  get; set; }=string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string City {  get; set; } = string.Empty;
        public bool IsAccessEnabled {  get; set; }=false;
        public string UserId {  get; set; }=string.Empty;
        public bool ClientStatus {  get; set; }=false;
        public string LicenseNumber {  get; set; }=string.Empty;
        public decimal DiscountPercentage {  get; set; }=decimal.MaxValue;
        public string AdditionalContact {  get; set; }=string.Empty;
        public string ClientType {  get; set; }=string.Empty;
        public string BillingType {  get; set; }=string.Empty;
    }
}
