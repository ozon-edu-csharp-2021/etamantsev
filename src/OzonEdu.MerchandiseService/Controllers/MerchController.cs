using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.Models;
using OzonEdu.MerchandiseService.Services.Contracts;

namespace OzonEdu.MerchandiseService.Controllers
{
    [ApiController]
    [Route("v1/api/merch")]
    public class MerchController : ControllerBase
    {
        private readonly IMerchService _merchService;

        public MerchController(IMerchService merchService)
        {
            _merchService = merchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var stockItems = await _merchService.GetAll(token);
            return Ok(stockItems);
        }

        [HttpGet("{merchId:long}/issuance")]
        [ProducesResponseType(typeof(MerchItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Issuance(long merchId, CancellationToken token)
        {
            var issuance = await _merchService.GetIssuanceInfo(merchId, token);
            if (issuance is null)
            {
                return NotFound();
            }

            return Ok(issuance);
        }
    }
}