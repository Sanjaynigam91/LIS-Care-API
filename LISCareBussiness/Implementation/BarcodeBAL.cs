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
    public class BarcodeBAL(IBarcodeRepository barcodeRepository):IBarcode
    {
        private readonly IBarcodeRepository _barcodeRepository=barcodeRepository;

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
    }
}
