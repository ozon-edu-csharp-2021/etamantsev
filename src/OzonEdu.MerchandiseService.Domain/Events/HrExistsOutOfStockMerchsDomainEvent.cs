using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;

namespace OzonEdu.MerchandiseService.Domain.Events
{
    /// <summary>
    ///     В заявке на выдачу мерча есть позиции которых нет на складе -> уведомить ответственного менеджера.
    /// </summary>
    public class HrExistsOutOfStockMerchsDomainEvent : INotification
    {
        public HrExistsOutOfStockMerchsDomainEvent(Employee performer)
        {
            Performer = performer;
        }

        /// <summary>
        ///     Ответственный менеджер.
        /// </summary>
        public Employee Performer { get; }
    }
}