using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.Infrastructure.DomainServices.Contracts;
using OzonEdu.MerchandiseService.Models;

namespace OzonEdu.MerchandiseService.Controllers
{
    [ApiController]
    [Route("v1/api/merch")]
    public class MerchController : ControllerBase
    {
        private readonly IMerchRequestDomainService _merchService;

        public MerchController(IMerchRequestDomainService merchService)
        {
            _merchService = merchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var stockItems = await _merchService.GetAllMerchAsync(token);
            return Ok(stockItems);
        }

        [HttpGet("{merchRequestId:long}/issuance")]
        [ProducesResponseType(typeof(IssuanceInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Issuance(long merchRequestId, CancellationToken token)
        {
            var issuance = await _merchService.GetIssuanceInfoAsync(merchRequestId, token);
            if (issuance is null)
            {
                return NotFound();
            }

            return Ok(issuance);
        }
    }
}