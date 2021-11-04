using System;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public class RequestItemTests
    {
        [Fact]
        public void QuantityIsNotAllowedZero()
        {
            Assert.Throws<NegativeValueException>(() => new RequestItem(
                new Sku(1),
                new Quantity(0),
                RequestItemStatus.Reserved,
                new Name("Футболка")
            ));
        }
        
        [Fact]
        public void QuantityIsNotNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RequestItem(
                new Sku(1),
                null,
                RequestItemStatus.Reserved,
                new Name("Футболка")
            ));
        }
        
        [Fact]
        public void QuantityIsNotNegative()
        {
            Assert.Throws<NegativeValueException>(() => new RequestItem(
                new Sku(1),
                new Quantity(-1),
                RequestItemStatus.Reserved,
                new Name("Футболка")
            ));
        }
        
        [Fact]
        public void NameIsNotNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RequestItem(
                new Sku(1),
                new Quantity(1),
                RequestItemStatus.Reserved,
               null
            ));
        }
        
        [Fact]
        public void StatusIsNotNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RequestItem(
                new Sku(1),
                new Quantity(1),
                null,
                new Name("Футболка")
            ));
        }
        
        [Fact]
        public void SkuIsNotNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RequestItem(
                null,
                new Quantity(1),
                RequestItemStatus.Reserved,
                new Name("Футболка")
            ));
        }
        
        [Fact]
        public void ValidObject()
        {
            var testObj = new RequestItem(
                new Sku(1),
                new Quantity(1),
                RequestItemStatus.Reserved,
                new Name("Футболка")
            );
        }
    }
}