using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OzonEdu.MerchandiseService.Grpc;
using OzonEdu.MerchandiseService.Services.Contracts;

namespace OzonEdu.MerchandiseService.GrpcServices
{
    public class MerchandiseGrpcService : MerchandiseServiceGrpc.MerchandiseServiceGrpcBase
    {
        private readonly IMerchService _merchService;

        public MerchandiseGrpcService(IMerchService merchService)
        {
            _merchService = merchService;
        }

        public override async Task<GetAllMerchResponse> V1GetAll(
            GetAllMerchRequest request,
            ServerCallContext context)
        {
            var retVal = await _merchService.GetAll(context.CancellationToken);
            return new GetAllMerchResponse
            {
                Merch =
                {
                    retVal.Select(e => new GetAllMerchItemsResponseUnit
                    {
                        Id = e.Id,
                        Name = e.Name
                    })
                }
            };
        }

        public override async Task<GetIssuanceResponse> V1GetIssuance(
            GetIssuanceRequest request,
            ServerCallContext context)
        {
            var retVal = await _merchService.GetIssuanceInfo(request.MerchId, context.CancellationToken);
            return new GetIssuanceResponse
            {
                Merch = retVal.MerchName,
                Performer = retVal.Performer,
                IssuanceOn = Timestamp.FromDateTimeOffset(retVal.IssuanceOn),
                MerchId = retVal.MerchId
            };
        }
    }
}