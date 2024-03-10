using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartInvestor.Application.Command.Requests.Stocks;
using SmartInvestor.Domain.Models;
using SmartInvestor.Domain.Utils;

namespace SmartInvestor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StocksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{ticket}")]
        public async Task<ActionResult<StockIDetail>> GeByTicket(string ticket)
        {
            var request = new StockRequest { Ticket = ticket };

            var response = await _mediator.Send(request);

            return response.Stock;
        }

        [HttpGet]
        public async Task<ActionResult<List<Stock>>> GetAll([FromQuery] QueryParams queryParams)
        {
            var request = new StockListRequest { QueryParams = queryParams };

            var response = await _mediator.Send(request);

            return response.Stocks;
        }
    }
}
