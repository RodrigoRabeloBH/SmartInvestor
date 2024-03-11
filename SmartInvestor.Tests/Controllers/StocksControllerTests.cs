using AutoFixture;
using MediatR;
using Moq;
using SmartInvestor.Api.Controllers;
using SmartInvestor.Application.Command.Requests.Stocks;
using SmartInvestor.Application.Command.Responses.Stocks;

namespace SmartInvestor.Tests.Controllers
{
    public class StocksControllerTests
    {
        private readonly StocksController _sut;
        private readonly Fixture _fixture;
        private readonly Mock<IMediator> _mediatorMock;

        public StocksControllerTests()
        {
            _fixture = new Fixture();
            _mediatorMock = new Mock<IMediator>();

            _sut = new StocksController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GeByTicket_ShouldReturnStockResponse()
        {
            // Arrange

            var request = _fixture.Create<StockRequest>();

            var response = _fixture.Create<StockResponse>();

            _mediatorMock.Setup(x => x.Send(It.IsAny<StockRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            //Act

            var result = await _sut.GeByTicket(request.Ticket);

            //Assert

            Assert.IsType<StockResponse>(result.Value);
        }

        [Fact]
        public async Task GetAll_ShouldReturnStockListResponse()
        {
            // Arrange

            var request = _fixture.Create<StockListRequest>();

            var response = _fixture.Create<StockListResponse>();

            _mediatorMock.Setup(x => x.Send(It.IsAny<StockListRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            //Act

            var result = await _sut.GetAll(request.QueryParams);

            //Assert

            Assert.IsType<StockListResponse>(result.Value);
        }
    }
}
