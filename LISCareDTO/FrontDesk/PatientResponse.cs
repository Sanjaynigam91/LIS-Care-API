using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.FrontDesk
{
    public class PatientResponse
    {
        public Guid PatientId { get; set; }
        public string PatientCode { get; set; } = string.Empty;
        public int VisitId { get; set; }
        public string RegistrationStatus { get; set; } = string.Empty;
    }
}
