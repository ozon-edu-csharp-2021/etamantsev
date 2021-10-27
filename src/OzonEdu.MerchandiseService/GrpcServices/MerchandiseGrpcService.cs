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

        public override async Task<GetAllMerchItemsResponse> V1GetAll(
            Empty request,
            ServerCallContext context)
        {
            var retVal = await _merchService.GetAll(context.CancellationToken);
            return new GetAllMerchItemsResponse
            {
                Merch = { retVal.Select(e => new GetAllMerchItemsResponseUnit
                {
                    Id = e.Id,
                    Name = e.Name
                })}
            };
        }

        public override async Task<GetIssuanceResponseUnit> V1GetIssuance(
            IssuanceRequest request,
            ServerCallContext context)
        {
            var retVal = await _merchService.GetIssuanceInfo(request.MerchId, context.CancellationToken);
            return new GetIssuanceResponseUnit
            {
                Merch = retVal.Merch,
                Performer = retVal.Performer,
                IssuanceOn = Timestamp.FromDateTimeOffset(retVal.IssuanceOn),
                MerchId = retVal.MerchId
            };
        }
    }
}