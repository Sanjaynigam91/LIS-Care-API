using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.Employee
{
    public class EmployeeResponse
    {
        public int RecordId { get; set; } = 0;
        public string EmployeeId { get; set; }=string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string EmailId {  get; set; } = string.Empty;
        public DateTime DateOfJoining { get; set; } = DateTime.Now;
        public string ContactNumber {  get; set; } = string.Empty;
        public string MobileNumber {  get; set; } = string.Empty;
        public string Department {  get; set; } = string.Empty;
        public string Designation {  get; set; } = string.Empty;
        public string Qualification {  get; set; } = string.Empty;
        public bool RecordStatus { get; set; } = false;
        public string Address {  get; set; } = string.Empty;
        public bool IsPathology { get; set; } = false;
        public string SignatureImage {  get; set; } = string.Empty;
        public string PartnerId {  get; set; } = string.Empty;
    }
}
