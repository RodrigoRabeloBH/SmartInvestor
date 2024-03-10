using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInvestor.Application.Command.Requests.Wallets;
using SmartInvestor.Application.Command.Responses.Wallets;
using SmartInvestor.Domain.Dto;

namespace SmartInvestor.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<GetWalletResponse>> GetWaletByUserId(Guid userId)
        {
            var request = new GetWalletRequest { UserId = userId };

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("createWallet")]
        public async Task<ActionResult<CreateWalletResponse>> CreateWallet(CreateWalletDto createWalletDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var request = new CreateWalletRequest
            {
                DividendGoalPerYear = createWalletDto.DividendGoalPerYear,
                Username = User.Identity.Name,
                UserId = Guid.Parse(User.Claims.First(x => x.Type == "userId").Value)
            };

            var response = await _mediator.Send(request);

            if (response.Success)
                return CreatedAtAction(nameof(GetWaletByUserId), new { response.Wallet.UserId }, response.Wallet);

            return BadRequest("Could not save changes to the DB");
        }

        [HttpPost("createSotckPlanning")]
        public async Task<ActionResult<CreateStockPlanningResponse>> CreateStockPlanning(CreateStockPlanningRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _mediator.Send(request);

            if (response.Success)
                return CreatedAtAction(nameof(GetWaletByUserId), new { response.Wallet.UserId }, response.Wallet);

            return BadRequest("Could not save changes to the DB");
        }

        [HttpPut("updateWallet")]
        public async Task<ActionResult<UpdateWalletResponse>> UpdateWallet(UpdateWalletDto updateWalletDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var request = new UpdateWalletRequest
            {
                Username = User.Identity.Name,
                UserId = Guid.Parse(User.Claims.First(x => x.Type == "userId").Value),
                DividendGoalPerYear = updateWalletDto.DividendGoalPerYear,
                WalletId = updateWalletDto.WalletId
            };

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPut("decrementStock")]
        public async Task<ActionResult<DecrementStockResponse>> DecrementStock(DecrementStockRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _mediator.Send(request);

            return response;
        }

        [HttpDelete("deleteWallet")]
        public async Task<ActionResult<RemoveWalletResponse>> DeleteWallet(RemoveWalletRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _mediator.Send(request);

            return response;
        }
    }
}
