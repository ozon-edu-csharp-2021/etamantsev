using System;

namespace OzonEdu.MerchandiseService.HttpModels
{
    public class IssuanceInfoResponseModel
    {
        public DateTimeOffset IssuanceOn { get; set; }

        public string Performer { get; set; }

        public long MerchId { get; set; }

        public string Merch { get; set; }
    }
}