using System;
using System.Collections.Generic;
using System.Linq;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void MerchItemsNotEmpty()
        {
            Assert.Throws<EmptyMerchRequestException>(() => new MerchRequest(
                new Employee(new Name("Исполнитель")),
                new Employee(new Name("Сотрудник")),
                new CreatedOn(DateTimeOffset.Now),
                RequestStatus.InWork,
                new IssuanceOn(null),
                MerchPack.Starter,
                Enumerable.Empty<RequestItem>()));
        }
        
        [Fact]
        public void MerchItemsNotNull()
        {
            Assert.Throws<EmptyMerchRequestException>(() => new MerchRequest(
                new Employee(new Name("Исполнитель")),
                new Employee(new Name("Сотрудник")),
                new CreatedOn(DateTimeOffset.Now),
                RequestStatus.InWork,
                new IssuanceOn(null),
                MerchPack.Starter,
                null));
        }
        
        [Fact]
        public void MerchItemsWithoutDublicatesBySku()
        {
            Assert.Throws<NotValidItemsException>(() => new MerchRequest(
                new Employee(new Name("Исполнитель")),
                new Employee(new Name("Сотрудник")),
                new CreatedOn(DateTimeOffset.Now),
                RequestStatus.InWork,
                new IssuanceOn(null),
                MerchPack.Starter,
                new List<RequestItem>
                {
                    new RequestItem(
                        new Sku(1),
                        new Quantity(1),
                        RequestItemStatus.Reserved,
                        new Name("Футболка")
                    ),
                    new RequestItem(
                        new Sku(1),
                        new Quantity(2),
                        RequestItemStatus.Reserved,
                        new Name("Футболка")
                    )
                }));
        }
        
        [Fact]
        public void MerchStatusNotNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MerchRequest(
                new Employee(new Name("Исполнитель")),
                new Employee(new Name("Сотрудник")),
                new CreatedOn(DateTimeOffset.Now),
                null,
                new IssuanceOn(null),
                MerchPack.Starter,
                new List<RequestItem>
                {
                    new RequestItem(
                        new Sku(1),
                        new Quantity(1),
                        RequestItemStatus.Reserved,
                        new Name("Футболка")
                    )
                }));
        }
        
        [Fact]
        public void CantSetDoneStatusIfExistsMerchWhereNotExistsInStock()
        {
            Assert.Throws<NotValidRequestStatusException>(() => new MerchRequest(
                new Employee(new Name("Исполнитель")),
                new Employee(new Name("Сотрудник")),
                new CreatedOn(DateTimeOffset.Now),
                RequestStatus.Done, 
                new IssuanceOn(null),
                MerchPack.Starter,
                new List<RequestItem>
                {
                    new RequestItem(
                        new Sku(1),
                        new Quantity(1),
                        RequestItemStatus.Reserved,
                        new Name("Футболка")
                    ),
                    new RequestItem(
                        new Sku(2),
                        new Quantity(1),
                        RequestItemStatus.OutOfStock,
                        new Name("Футболка2")
                    )
                }));
        }
        
        [Fact]
        public void CantSetIssuanceOnIfRequestInWork()
        {
            Assert.Throws<IssuedException>(() => new MerchRequest(
                new Employee(new Name("Исполнитель")),
                new Employee(new Name("Сотрудник")),
                new CreatedOn(DateTimeOffset.Now),
                RequestStatus.InWork, 
                new IssuanceOn(DateTimeOffset.Now),
                MerchPack.Starter,
                new List<RequestItem>
                {
                    new RequestItem(
                        new Sku(1),
                        new Quantity(1),
                        RequestItemStatus.Reserved,
                        new Name("Футболка")
                    ),
                    new RequestItem(
                        new Sku(2),
                        new Quantity(1),
                        RequestItemStatus.OutOfStock,
                        new Name("Футболка2")
                    )
                }));
        }
        
        [Fact]
        public void RefreshMerchStatusesValidMerchStatuses()
        {
            var merchRequest = new MerchRequest(
                new Employee(new Name("Исполнитель")),
                new Employee(new Name("Сотрудник")),
                new CreatedOn(DateTimeOffset.Now),
                RequestStatus.InWork,
                new IssuanceOn(null),
                MerchPack.Starter,
                new List<RequestItem>
                {
                    new RequestItem(
                        new Sku(1),
                        new Quantity(1),
                        RequestItemStatus.Reserved,
                        new Name("Футболка")
                    ),
                    new RequestItem(
                        new Sku(2),
                        new Quantity(1),
                        RequestItemStatus.OutOfStock,
                        new Name("Футболка2")
                    )
                });

            merchRequest.RefreshMerchStatuses(new List<RequestItem>{
                new RequestItem(
                new Sku(2),
                new Quantity(1),
                RequestItemStatus.Reserved,
                new Name("Футболка2")
            )});
            
            Assert.All(
                merchRequest.Items.Select(e => e.Status),
                i => { Assert.Equal(i.Equals(RequestItemStatus.Reserved), true);});
        }

        [Fact]
        public void RefreshMerchStatusesValidRequestStatus()
        {
            var merchRequest = new MerchRequest(
                new Employee(new Name("Исполнитель")),
                new Employee(new Name("Сотрудник")),
                new CreatedOn(DateTimeOffset.Now),
                RequestStatus.InWork,
                new IssuanceOn(null),
                MerchPack.Starter,
                new List<RequestItem>
                {
                    new RequestItem(
                        new Sku(1),
                        new Quantity(1),
                        RequestItemStatus.Reserved,
                        new Name("Футболка")
                    ),
                    new RequestItem(
                        new Sku(2),
                        new Quantity(1),
                        RequestItemStatus.OutOfStock,
                        new Name("Футболка2")
                    )
                });

            merchRequest.RefreshMerchStatuses(new List<RequestItem>{
                new RequestItem(
                    new Sku(2),
                    new Quantity(1),
                    RequestItemStatus.Reserved,
                    new Name("Футболка2")
                )});
            
            Assert.Equal(merchRequest.Status.Equals(RequestStatus.Done), true);
        }
        
        [Fact]
        public void RefreshMerchStatusesValidIssuanceOn()
        {
            var merchRequest = new MerchRequest(
                new Employee(new Name("Исполнитель")),
                new Employee(new Name("Сотрудник")),
                new CreatedOn(DateTimeOffset.Now),
                RequestStatus.InWork,
                new IssuanceOn(null),
                MerchPack.Starter,
                new List<RequestItem>
                {
                    new RequestItem(
                        new Sku(1),
                        new Quantity(1),
                        RequestItemStatus.Reserved,
                        new Name("Футболка")
                    ),
                    new RequestItem(
                        new Sku(2),
                        new Quantity(1),
                        RequestItemStatus.OutOfStock,
                        new Name("Футболка2")
                    )
                });

            merchRequest.RefreshMerchStatuses(new List<RequestItem>{
                new RequestItem(
                    new Sku(2),
                    new Quantity(1),
                    RequestItemStatus.Reserved,
                    new Name("Футболка2")
                )});
            
            Assert.Equal(merchRequest.IssuanceOn.HasValue, true);
        }
    }
}