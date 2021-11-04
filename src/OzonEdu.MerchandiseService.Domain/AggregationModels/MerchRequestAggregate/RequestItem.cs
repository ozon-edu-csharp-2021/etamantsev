using System;
using OzonEdu.MerchandiseService.Domain.Events;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    /// <summary>
    ///     Позиция в заявке.
    /// </summary>
    public class RequestItem : Entity
    {
        public RequestItem(Sku sku, Quantity quantity, RequestItemStatus status, Name name)
        {
            Sku = sku ?? throw new ArgumentNullException(nameof(sku));
            SetQuantity(quantity);
            Status = status ?? throw new ArgumentNullException(nameof(status));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        ///     Идентификатор мерча на складе.
        /// </summary>
        public Sku Sku { get; }

        public Name Name { get; }

        /// <summary>
        ///     Кол-во.
        /// </summary>
        public Quantity Quantity { get; private set; }

        /// <summary>
        ///     Статус по конкретному мерчу из заявки.
        /// </summary>
        public RequestItemStatus Status { get; private set; }

        public void ChangeStatus(RequestItemStatus newStatus)
        {
            if (!newStatus.Equals(Status))
                this.AddDomainEvent(new ReserveSkuDomainEvent(Sku, Quantity));

            Status = newStatus;
        }

        private void SetQuantity(Quantity value)
        {
            if (value is null)
                throw new ArgumentNullException($"{nameof(value)} is null");

            if (value.Value < 0)
                throw new NegativeValueException($"{nameof(value)} value is negative");
            
            if (value.Value == 0)
                throw new NegativeValueException($"{nameof(value)} value cant be zero");

            Quantity = value;
        }
    }
}