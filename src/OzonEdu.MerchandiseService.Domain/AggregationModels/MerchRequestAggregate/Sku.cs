using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    /// <summary>
    ///     Идентификатор товара на складе - идентификатор из внешней системы склада.
    /// </summary>
    public class Sku : ValueObject
    {
        public Sku(long value)
        {
            Value = value;
        }

        public long Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}