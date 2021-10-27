using System;

namespace OzonEdu.MerchandiseService.Models
{
    public class IssuanceInfo
    {
        public IssuanceInfo(
            DateTimeOffset issuanceOn,
            string performer,
            long merchId,
            string merch)
        {
            IssuanceOn = issuanceOn;
            Performer = performer;
            MerchId = merchId;
            Merch = merch;
        }

        public DateTimeOffset IssuanceOn { get; }

        public string Performer { get; }

        public long MerchId { get; }

        public string Merch { get; }
    }
}