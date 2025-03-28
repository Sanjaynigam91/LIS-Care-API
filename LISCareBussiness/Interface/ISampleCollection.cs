﻿using LISCareDTO;
﻿using LISCareDTO.SampleCollectionPlace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LISCareBussiness.Interface
{
    public interface ISampleCollection
    {
        List<SampleCollectedAtResponse> GetSampleCollectedPlace(string partnerId);
        APIResponseModel<object> AddSampleCollectedPlaces(SampleCollectedRequest sampleCollected);
        APIResponseModel<object> RemoveSamplePlace(int recordId, string partnerId);
    }
}
