using System;

namespace OzonEdu.MerchandiseService.Models
{
    public class IssuanceInfo
    {
        public IssuanceInfo(
            DateTimeOffset issuanceOn,
            string performer,
            long merchId,
            string merchName)
        {
            IssuanceOn = issuanceOn;
            Performer = performer;
            MerchId = merchId;
            MerchName = merchName;
        }

        public DateTimeOffset IssuanceOn { get; }

        public string Performer { get; }

        public long MerchId { get; }

        public string MerchName { get; }
    }
}