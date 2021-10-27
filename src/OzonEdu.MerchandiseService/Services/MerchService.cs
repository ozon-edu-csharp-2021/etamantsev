using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Models;
using OzonEdu.MerchandiseService.Services.Contracts;

namespace OzonEdu.MerchandiseService.Services
{
    public class MerchService : IMerchService
    {
        private List<MerchItem> _merchItems = new List<MerchItem>();
        private List<IssuanceInfo> _issuanceInfo = new List<IssuanceInfo>();
        public MerchService()
        {
            var rnd = new Random();
            var start = new DateTimeOffset(2017, 5, 5, 23, 44, 21, 22, default);
            var now = DateTimeOffset.Now;
            Enumerable.Range(0, 100)
                .ToList()
                .ForEach(i =>
                {
                    _merchItems.Add(new MerchItem(i, $"Item_{i}"));
                    _issuanceInfo.Add(new IssuanceInfo(
                        start.AddDays(rnd.Next((now - start).Days)),
                        i % 3 == 0 ? $"Performer1" : "Performer2",
                        i,
                        $"Item_{i}"
                        ));
                });
        }


        public Task<List<MerchItem>> GetAll(CancellationToken token) => Task.FromResult(_merchItems);

        public Task<IssuanceInfo> GetIssuanceInfo(long merchId, CancellationToken token)
        {
            var stockItem = _issuanceInfo.FirstOrDefault(x => x.MerchId == merchId);
            return Task.FromResult(stockItem);
        }
    }
}