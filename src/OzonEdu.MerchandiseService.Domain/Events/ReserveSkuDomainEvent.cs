using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;

namespace OzonEdu.MerchandiseService.Domain.Events
{
    /// <summary>
    ///     Эвент - зарезервировать мерч на складе.
    /// </summary>
    public class ReserveSkuDomainEvent : INotification
    {
        public ReserveSkuDomainEvent(Sku sku, Quantity quantity)
        {
            Sku = sku;
            Quantity = quantity;
        }

        public Sku Sku { get; }

        public Quantity Quantity { get; }
    }
}