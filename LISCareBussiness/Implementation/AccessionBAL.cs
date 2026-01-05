using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.SampleAccession;
using LISCareDTO.SampleManagement;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class AccessionBAL : IAccession
    {
        private readonly IAccessionRepository accessionRepository;

        public AccessionBAL(IAccessionRepository accessionRepository)
        {
            this.accessionRepository = accessionRepository;
        }

        /// <summary>
        /// used to accept the selected sample
        /// </summary>
        /// <param name="acceptSample"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<AcceptSampleResponse>> AcceptSelectedSample(AcceptSampleRequest acceptSample)
        {
            APIResponseModel<AcceptSampleResponse> response;
            try
            {
                response = await accessionRepository.AcceptSelectedSample(acceptSample);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public async Task<APIResponseModel<BarcodeResponse>> CreateBarcode(int visitId, string? sampleType, string partnerId)
        {
            APIResponseModel<BarcodeResponse> response;
            try
            {
                response = await accessionRepository.CreateBarcode(visitId, sampleType, partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Used to get last imported
        /// </summary>
        /// <param name="woeDate"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<int>> GetLastImported(DateTime woeDate, string partnerId)
        {
            APIResponseModel<int> response;
            try
            {
                response = await accessionRepository.GetLastImported(woeDate, partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public async Task<APIResponseModel<PatientInfoResponse>> GetPatientInfoByBarcode(int visitId, string? sampleType, string partnerId)
        {
            APIResponseModel<PatientInfoResponse> response;
            try
            {
                response = await accessionRepository.GetPatientInfoByBarcode(visitId, sampleType, partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// used to get pending sample for accession
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="barcode"></param>
        /// <param name="centerCode"></param>
        /// <param name="patientName"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<PendingAccessionResponse>>> GetPendingSampleForAccession(DateTime startDate, DateTime endDate, string? barcode, string? centerCode, string? patientName, string partnerId)
        {
            APIResponseModel<List<PendingAccessionResponse>> response;
            try
            {
                response = await accessionRepository.GetPendingSampleForAccession(startDate, endDate, barcode, centerCode, patientName, partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// used to get sample types by visitId
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<SampleTypeResponse>>> GetSampleTypesByVisitid(int visitId, string partnerId)
        {
            APIResponseModel<List<SampleTypeResponse>> response;
            try
            {
                response = await accessionRepository.GetSampleTypesByVisitid(visitId, partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// used to get test details by barcode
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="sampleType"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<SampleAccessionTestResponse>>> GetTestDetailsByBarcode(string barcode, string? sampleType, string partnerId)
        {
            APIResponseModel<List<SampleAccessionTestResponse>> response;
            try
            {
                response = await accessionRepository.GetTestDetailsByBarcode(barcode, sampleType, partnerId);
            }
            catch
            {
                throw;
            }
            return response;
        }
    }
}
