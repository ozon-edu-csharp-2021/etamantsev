using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.HttpModels.RequestModel;
using OzonEdu.MerchandiseService.HttpModels.ResponseModels;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Contracts;
using OzonEdu.MerchandiseService.Infrastructure.Repository;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices
{
    public class MerchRequestDomainService : IMerchRequestDomainService
    {
        private readonly IMerchRequestRepository _repository;

        public MerchRequestDomainService(IMerchRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MerchItemResponseModel>> GetAllMerchAsync(CancellationToken token)
        {
            return (await _repository.GetAllAsync(token))
                .SelectMany(e => e.Items)
                .GroupBy(e => new { e.Sku, e.Name })
                .Select(e => new MerchItemResponseModel
                {
                    Id = e.Key.Sku.Value,
                    Name = e.Key.Name.Value
                });
        }
        
        public async Task<IssuanceInfoResponseModel> GetIssuanceInfoAsync(long merchRequestId, CancellationToken token)
        {
            var merchRequest =  await _repository.Find(merchRequestId, token);
            return merchRequest == null
                ? null
                : new IssuanceInfoResponseModel
                {
                    IssuanceOn = merchRequest.IssuanceOn.Value,
                    Performer = merchRequest.Performer.Name.Value,
                    MerchRequestId = merchRequest.Id,
                    Merch = merchRequest.Items.Select(e => new MerchItemResponseModel
                    {
                        Id = e.Sku.Value,
                        Name = e.Name.Value
                    }).ToList()
                };
        }

        /// <summary>
        ///     Обработка события - когда на склад пришла новая поставка, надо зарезервировать.
        /// <remarks>
        ///     Запрос к сервису склада - для резерва должен происходить внутри эвента ReserveSkuDomainEvent(RequestItem.ChangeStatus).
        /// </remarks>
        /// </summary>
        /// <param name="newSupply"></param>
        public async Task ProcessNewMerchSupply(List<NewMerchSupplyModel> newSupply, CancellationToken token)
        {
            var newSupplySkuIds =
                newSupply
                    .Select(e => e.SkuId)
                    .ToHashSet();

            var requestsWithNotExistsMerch = (
                    await _repository.GetAllAsync())
                .Where(e => e.Items.Any(i => 
                    i.Status.Equals(RequestItemStatus.OutOfStock) &&
                    newSupplySkuIds.Contains(i.Sku.Value)))
                .ToList();

            var skuQuantityMap = newSupply
                .ToDictionary(
                    e => e.SkuId,
                    e => e.Quantity);

            foreach (var requestWithNotExistsMerch in requestsWithNotExistsMerch)
            {
                var supply = new List<RequestItem>();
                foreach (var requestItem in requestWithNotExistsMerch
                    .Items.Where(e => e.Status.Equals(RequestItemStatus.OutOfStock)))
                {
                    if (skuQuantityMap.TryGetValue(requestItem.Sku.Value, out var quantityInStock))
                    {
                        if (requestItem.Quantity.Value <= quantityInStock)
                        {
                            supply.Add(new RequestItem(
                                requestItem.Sku,
                                requestItem.Quantity,
                                RequestItemStatus.Reserved,
                                requestItem.Name));
                            skuQuantityMap[requestItem.Sku.Value] = quantityInStock - requestItem.Quantity.Value;
                        }
                    }
                }

                requestWithNotExistsMerch.RefreshMerchStatuses(supply);
                await _repository.UpdateAsync(requestWithNotExistsMerch, token);
            }

            await _repository.UnitOfWork.SaveChangesAsync(token);
        }
    }
}