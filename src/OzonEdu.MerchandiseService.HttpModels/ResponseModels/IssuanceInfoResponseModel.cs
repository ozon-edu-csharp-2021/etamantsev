using System;
using System.Collections.Generic;

namespace OzonEdu.MerchandiseService.HttpModels.ResponseModels
{
    public class IssuanceInfoResponseModel
    {
        public DateTimeOffset? IssuanceOn { get; set; }

        public string Performer { get; set; }

        public long MerchRequestId { get; set; }

        public List<MerchItemResponseModel> Merch { get; set; }
    }
}