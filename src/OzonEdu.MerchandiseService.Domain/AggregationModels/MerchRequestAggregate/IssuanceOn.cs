using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public class IssuanceOn : ValueObject
    {
        public IssuanceOn(DateTimeOffset? value)
        {
            Value = value;
        }

        public DateTimeOffset? Value { get; }

        public bool HasValue => Value.HasValue;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}