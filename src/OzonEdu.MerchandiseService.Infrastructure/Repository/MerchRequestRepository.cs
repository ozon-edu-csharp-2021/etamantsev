using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.Repository
{
    public interface IMerchRequestRepository : IRepository<MerchRequest>
    {
        Task<List<MerchRequest>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<MerchRequest> Find(long id, CancellationToken cancellationToken = default);
    }

    public class MerchRequestRepository : IMerchRequestRepository
    {
        public MerchRequestRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; }

        public Task<MerchRequest> CreateAsync(MerchRequest itemToCreate, CancellationToken cancellationToken = default)
        {
            FakeStore.MerchRequests.Add(itemToCreate);
            return Task.FromResult(itemToCreate);
        }

        public Task<MerchRequest> UpdateAsync(MerchRequest itemToUpdate, CancellationToken cancellationToken = default)
        {
            var existsItem = FakeStore.MerchRequests.FirstOrDefault(e => e.Id == itemToUpdate.Id);
            if (existsItem is null)
                throw new Exception($"Item with id {itemToUpdate.Id} not found");

            FakeStore.MerchRequests.Remove(existsItem);
            FakeStore.MerchRequests.Add(itemToUpdate);
            return Task.FromResult(itemToUpdate);
        }

        public Task<List<MerchRequest>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(FakeStore.MerchRequests);
        }

        public Task<MerchRequest> Find(long id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(FakeStore.MerchRequests.FirstOrDefault(e => e.Id == id));
        }
    }
}