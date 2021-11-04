using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public class CreatedOn : ValueObject
    {
        public CreatedOn(DateTimeOffset value)
        {
            Value = value;
        }

        public DateTimeOffset Value { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}