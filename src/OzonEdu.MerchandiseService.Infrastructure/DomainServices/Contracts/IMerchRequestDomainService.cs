using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.HttpModels.ResponseModels;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainServices.Contracts
{
    public interface IMerchRequestDomainService
    {
        Task<IEnumerable<MerchItemResponseModel>> GetAllMerchAsync(CancellationToken token);

        Task<IssuanceInfoResponseModel> GetIssuanceInfoAsync(long merchRequestId, CancellationToken token);
    }
}