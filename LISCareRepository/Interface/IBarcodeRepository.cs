using LISCareDTO;
using LISCareDTO.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareRepository.Interface
{
    public interface IBarcodeRepository
    {
        /// <summary>
        /// used to get all barcode details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<BarcodeResponse>>> GetAllBarcodeDetails(string partnerId);
        /// <summary>
        /// used to generate barcodes in bulk
        /// </summary>
        /// <param name="SequenceStart"></param>
        /// <param name="SequenceEnd"></param>
        /// <returns></returns>
        Task<byte[]> GenerateBarcodes(int SequenceStart, int SequenceEnd);
    }
}
