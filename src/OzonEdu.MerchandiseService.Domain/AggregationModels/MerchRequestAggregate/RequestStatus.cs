using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    /// <summary>
    ///     Статус заявки на выдачу мерча.
    /// </summary>
    public class RequestStatus : Enumeration
    {
        public RequestStatus(int id, string name) : base(id, name)
        {
        }

        /// <summary>
        ///     В работе.
        /// </summary>
        public static RequestStatus InWork = new(1, nameof(InWork));

        /// <summary>
        ///     Выполнено.
        /// </summary>
        public static RequestStatus Done = new(2, nameof(Done));
    }
}