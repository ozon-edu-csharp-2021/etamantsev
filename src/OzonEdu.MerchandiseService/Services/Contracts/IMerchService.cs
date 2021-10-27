using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Models;

namespace OzonEdu.MerchandiseService.Services.Contracts
{
    public interface IMerchService
    {
        Task<List<MerchItem>> GetAll(CancellationToken token);

        Task<IssuanceInfo> GetIssuanceInfo(long merchId, CancellationToken token);
    }
}