using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Repository
{
    public static class FakeStore
    {
        public static List<MerchRequest> MerchRequests = new List<MerchRequest>
        {
            new MerchRequest(
                new Employee( new Name("Test_performer")),
                new Employee( new Name("Test_employee")),
                new CreatedOn(new DateTimeOffset(2021, 9, 21, 13, 20, 20, 30, default)),
                RequestStatus.Done,
                new IssuanceOn(new DateTimeOffset(2021, 9, 22, 13, 20, 20, 30, default)),
                MerchPack.Welcome, 
                new List<RequestItem>
                {
                    new RequestItem(new Sku(1), new Quantity(2), RequestItemStatus.Reserved, new Name("Ручка с логотипом ozon")),
                    new RequestItem(new Sku(2), new Quantity(1), RequestItemStatus.Reserved, new Name("Блокнот с логотипом ozon")),
                    new RequestItem(new Sku(3), new Quantity(1), RequestItemStatus.Reserved, new Name("Сумка с логотипом ozon")),
                }),
            
            new MerchRequest(
                new Employee( new Name("Test_performer_2")),
                new Employee( new Name("Test_employee2")),
                new CreatedOn(new DateTimeOffset(2021, 8, 21, 13, 20, 20, 30, default)),
                RequestStatus.Done,
                new IssuanceOn(new DateTimeOffset(2021, 8, 22, 13, 20, 20, 30, default)),
                MerchPack.Starter, 
                new List<RequestItem>
                {
                    new RequestItem(new Sku(1), new Quantity(1), RequestItemStatus.Reserved, new Name("Ручка с логотипом ozon")),
                    new RequestItem(new Sku(5), new Quantity(1), RequestItemStatus.Reserved, new Name("Календарь с логотипом ozon")),
                }),
            
            new MerchRequest(
                new Employee(new Name("Test_performer_3")),
                new Employee(new Name("Test_employee3")),
                new CreatedOn(new DateTimeOffset(2021, 11, 4, 13, 20, 20, 30, default)),
                RequestStatus.InWork,
                new IssuanceOn(null),
                MerchPack.ConferenceSpeaker, 
                new List<RequestItem>
                {
                    new RequestItem(new Sku(1), new Quantity(250), RequestItemStatus.Reserved, new Name("Ручка с логотипом ozon")),
                    new RequestItem(new Sku(2), new Quantity(200), RequestItemStatus.Reserved, new Name("Блокнот с логотипом ozon")),
                    new RequestItem(new Sku(60), new Quantity(200), RequestItemStatus.Reserved, new Name("информационные буклеты ozon")),
                    new RequestItem(new Sku(77), new Quantity(200), RequestItemStatus.OutOfStock, new Name("Набор стикеров ozon"))
                })
        };
    }
}