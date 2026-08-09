using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.RejectedSample
{
    public class RejectedSample
    {
        public string RejectedDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string PatientName { get; set; } = string.Empty;
        public string ReferredDoctor { get; set; } = string.Empty;
        public int VisitId { get; set; }
        public string CenterCode { get; set; } = string.Empty;
        public string CenterName { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public string PatientCode { get; set; } = string.Empty;
        public string RejectionReasons { get; set; } = string.Empty;
        public string ReferredLab { get; set; } = string.Empty;
        public string TestCode { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;

    }

}

