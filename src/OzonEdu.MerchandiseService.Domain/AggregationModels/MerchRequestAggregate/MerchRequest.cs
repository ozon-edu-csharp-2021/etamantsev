using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using OzonEdu.MerchandiseService.Domain.Events;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    /// <summary>
    ///     Заявка на мерч.
    /// </summary>
    public class MerchRequest : Entity
    {
        public MerchRequest(
            Employee performer,
            Employee recipient,
            CreatedOn createdOn,
            RequestStatus status,
            IssuanceOn issuanceOn,
            MerchPack pack,
            IEnumerable<RequestItem> items)
        {
            Performer = performer ?? throw new ArgumentNullException(nameof(performer));
            CreatedOn = createdOn ?? throw  new ArgumentNullException(nameof(createdOn));
            Pack = pack ?? throw new ArgumentNullException(nameof(pack));
            Recipient = recipient ?? throw  new ArgumentNullException(nameof(recipient));
            SetItems(items);
            SetStatus(status);
            SetIssuanceOn(issuanceOn);
        }

        /// <summary>
        ///     Ответственный менеджер.
        /// </summary>
        public Employee Performer { get; }

        /// <summary>
        ///     Получатель мерча.
        /// </summary>
        public Employee Recipient { get; }

        /// <summary>
        ///     Дата создания заявки.
        /// </summary>
        public CreatedOn CreatedOn { get; }

        /// <summary>
        ///     Статус заявки.
        /// </summary>
        public RequestStatus Status { get; private set; }

        /// <summary>
        ///     Дата выдачи.
        /// </summary>
        public IssuanceOn IssuanceOn { get; private set; }

        /// <summary>
        ///     Вид пака выдаваемого мерча.
        /// </summary>
        public MerchPack Pack { get; }

        /// <summary>
        ///     Вещи в заявке на выдачу.
        /// </summary>
        public IReadOnlyList<RequestItem> Items { get; private set; }

        /// <summary>
        ///     Обновить статусы по мерчу в заявке.
        /// </summary>
        /// <param name="ReplenishedBalances">Пополненные остатки на складе.</param>
        /// <exception cref="EmptyMerchRequestException">Пополненные остатки - пусты.</exception>
        /// <exception cref="NotValidItemsException">Список мерча невалиден.</exception>
        public void RefreshMerchStatuses(IEnumerable<RequestItem> ReplenishedBalances)
        {
            if (ReplenishedBalances == null)
                throw new EmptyMerchRequestException("Merch request is empty!");

            if (ItemsHasDublicates(ReplenishedBalances))
                throw new NotValidItemsException("New balances has dublicates by sku");

            var skuStatusMap = ReplenishedBalances
                .ToDictionary(e => e.Sku.Value, e => e.Status);

            foreach (var existItem in Items)
            {
                if (skuStatusMap.TryGetValue(existItem.Sku.Value, out var newStatus))
                {
                    existItem.ChangeStatus(newStatus);
                }
            }

            if (Items.All(e => e.Status.Equals(RequestItemStatus.Reserved)))
            {
                SetStatus(RequestStatus.Done);
                SetIssuanceOn(new IssuanceOn(DateTimeOffset.Now));
                var merchRequestCompletedEvent = new MerchRequestCompletedDomainEvent(Recipient);
                this.AddDomainEvent(merchRequestCompletedEvent);
            }
        }

        private void SetItems(IEnumerable<RequestItem> items)
        {
            if (items == null || !items.Any())
                throw new EmptyMerchRequestException("Merch request is empty!");
            
            if (ItemsHasDublicates(items))
                throw new NotValidItemsException("Merch list has dublicates");

            if (items.Any(e => e.Status.Equals(RequestItemStatus.OutOfStock)))
            {
                var OutOfStockMerchsEvent = new HrExistsOutOfStockMerchsDomainEvent(Performer);
                this.AddDomainEvent(OutOfStockMerchsEvent);

            }

            Items = items.ToImmutableArray();
        }

        private bool ItemsHasDublicates(IEnumerable<RequestItem> items)
        {
            return items.GroupBy(e => e.Sku.Value).Any(e => e.Count() > 1);
        }

        private void SetStatus(RequestStatus newStatus)
        {
            if (newStatus is null)
                throw new ArgumentNullException($"{nameof(newStatus)} is null");

            var isDoneStatus = newStatus.Equals(RequestStatus.Done);

            if (isDoneStatus && Items.Any(e => e.Status.Equals(RequestItemStatus.OutOfStock)))
                throw new NotValidRequestStatusException(
                    $@"Cannot set ""{newStatus.Name}"" status, some items are not available");

            Status = newStatus;
        }

        private void SetIssuanceOn(IssuanceOn issuanceOn)
        {
            if (issuanceOn is null)
                throw new ArgumentNullException($"{nameof(issuanceOn)} is null");

            if (issuanceOn.Value is not null && !Status.Equals(RequestStatus.Done))
                throw new IssuedException("Merch cannot be issued, some items are not available in stock");

            IssuanceOn = new IssuanceOn(issuanceOn.Value);
        }
    }
}