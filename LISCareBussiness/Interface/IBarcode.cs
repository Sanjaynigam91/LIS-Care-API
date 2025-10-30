using LISCareDTO;
using LISCareDTO.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Interface
{
    public interface IBarcode
    {
        /// <summary>
        /// used to get all 
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<BarcodeResponse>>> GetAllBarcodeDetails(string partnerId);
    }
}
