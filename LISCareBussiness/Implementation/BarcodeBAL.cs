using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.Barcode;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class BarcodeBAL:IBarcode
    {
        private readonly IBarcodeRepository _barcodeRepository;

        public BarcodeBAL(IBarcodeRepository barcodeRepository)
        {
            _barcodeRepository= barcodeRepository;
        }   


        /// <summary>
        /// used to generate barcodes in bulk
        /// </summary>
        /// <param name="SequenceStart"></param>
        /// <param name="SequenceEnd"></param>
        /// <returns></returns>
        public async Task<byte[]> GenerateBarcodes(int sequenceStart, int sequenceEnd)
        {
            try
            {
                return await _barcodeRepository.GenerateBarcodes(sequenceStart, sequenceEnd);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used to get all barcode details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<BarcodeResponse>>> GetAllBarcodeDetails(string partnerId)
        {
            try
            {
                return await _barcodeRepository.GetAllBarcodeDetails(partnerId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<APIResponseModel<int>> GetLastPrintedBarcode(string partnerId)
        {
            try
            {
                return await _barcodeRepository.GetLastPrintedBarcode(partnerId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<APIResponseModel<string>> SavePrintedBarcodes(BarcodeRequest barcodeRequest)
        {
            try
            {
                return await _barcodeRepository.SavePrintedBarcodes(barcodeRequest);
            }
            catch
            {
                throw;
            }
        }
    }
}
