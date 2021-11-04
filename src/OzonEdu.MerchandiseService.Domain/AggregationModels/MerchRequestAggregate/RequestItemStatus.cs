using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public class RequestItemStatus : Enumeration
    {
        public RequestItemStatus(int id, string name) : base(id, name)
        {
        }
        
        /// <summary>
        ///     Зарезервировано.
        /// </summary>
        public static RequestItemStatus Reserved = new(1, nameof(Reserved));

        /// <summary>
        ///     Нет в наличии на складе.
        /// </summary>
        public static RequestItemStatus OutOfStock = new(2, nameof(OutOfStock));
    }
}