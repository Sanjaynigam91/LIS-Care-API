using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareDTO.SampleManagement
{
    public class SamplePendingCollectionResponse
    {
        public DateTime RegisteredDate { get; set; }= DateTime.MinValue;
        public string ReferedDoctor { get; set; }= string.Empty;
        public int TotalTubes { get; set; }= 0;
        public string SampleType { get; set; }= string.Empty;
        public string Barcode { get; set; }= string.Empty;
        public string NewBarcode { get; set; }= string.Empty;
        public DateTime SampleCollectionTime { get; set; }= DateTime.MinValue;
        public string WorkOrderStatus { get; set; }= string.Empty;
        public string PartnerId { get; set; }= string.Empty;
        public string PatientCode { get; set; }= string.Empty;
        public Guid PatientId { get; set; }= Guid.Empty;
        public string Lab { get; set; }= string.Empty;
        public int SampleId { get; set; }= 0;
        public bool IsSpecimenCollected { get; set; }= false;
        public string ActualBarcode { get; set; }= string.Empty;
      
    }

    public class  RequestedTest
    {
        public string TestCode { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public string SpecimenType { get; set; } = string.Empty;

    }
}
