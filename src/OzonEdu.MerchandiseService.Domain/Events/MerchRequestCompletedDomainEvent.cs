using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;

namespace OzonEdu.MerchandiseService.Domain.Events
{
    /// <summary>
    ///     Заявка на мерч выполнена.
    /// </summary>
    public class MerchRequestCompletedDomainEvent : INotification
    {
        public MerchRequestCompletedDomainEvent(Employee recipient)
        {
            Recipient = recipient;
        }

        /// <summary>
        ///     Получатель мерча.
        /// </summary>
        public Employee Recipient { get; }
    }
}