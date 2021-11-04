using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OzonEdu.MerchandiseService.Grpc;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Contracts;

namespace OzonEdu.MerchandiseService.GrpcServices
{
    public class MerchandiseGrpcService : MerchandiseServiceGrpc.MerchandiseServiceGrpcBase
    {
        private readonly IMerchRequestDomainService _merchService;

        public MerchandiseGrpcService(IMerchRequestDomainService merchService)
        {
            _merchService = merchService;
        }

        public override async Task<GetAllMerchResponse> V1GetAll(
            GetAllMerchRequest request,
            ServerCallContext context)
        {
            var retVal = await _merchService.GetAllMerchAsync(context.CancellationToken);
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
            var retVal = await _merchService.GetIssuanceInfoAsync(request.MerchRequestId, context.CancellationToken);
            return new GetIssuanceResponse
            {
                Merch =
                {
                    retVal.Merch.Select(e => new GetAllMerchItemsResponseUnit
                    {
                        Id = e.Id,
                        Name = e.Name
                    })
                },
                Performer = retVal.Performer,
                IssuanceOn = retVal.IssuanceOn .HasValue
                    ? Timestamp.FromDateTimeOffset(retVal.IssuanceOn.Value)
                    : null,
                MerchRequestId = retVal.MerchRequestId
            };
        }
    }
}