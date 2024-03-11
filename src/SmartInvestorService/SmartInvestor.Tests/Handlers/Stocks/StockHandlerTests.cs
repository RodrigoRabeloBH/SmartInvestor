using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using SmartInvestor.Application.Command.Handlers.Stocks;
using SmartInvestor.Application.Command.Requests.Stocks;
using SmartInvestor.Application.Command.Responses.Stocks;
using SmartInvestor.Domain.Interfaces;
using SmartInvestor.Domain.Models;

namespace SmartInvestor.Tests.Handlers.Stocks
{
    public class StockHandlerTests
    {
        private readonly StockHandler _sut;
        private readonly Fixture _fixture;

        private readonly Mock<IBrapiServices> _brapiServicesMock;
        private readonly Mock<ILogger<StockHandler>> _loggerMock;

        public StockHandlerTests()
        {
            _fixture = new Fixture();
            _brapiServicesMock = new Mock<IBrapiServices>();
            _loggerMock = new Mock<ILogger<StockHandler>>();

            _sut = new StockHandler(_brapiServicesMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_SouldReturnStockResponseWithStock()
        {
            // Arrange

            var request = _fixture.Create<StockRequest>();

            var stockDetail = _fixture.Create<StockDetail>();

            _brapiServicesMock.Setup(x => x.GetStockByTicket(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(stockDetail);

            //Act

            var response = await _sut.Handle(request, new());

            //Assert

            Assert.NotNull(response);
            Assert.NotNull(response.Stock);
            Assert.IsType<StockResponse>(response);
            _brapiServicesMock.Verify(x => x.GetStockByTicket(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_SouldReturnNull()
        {
            // Arrange

            var request = _fixture.Create<StockRequest>();

            _brapiServicesMock.Setup(x => x.GetStockByTicket(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(value: null);

            //Act

            var response = await _sut.Handle(request, new());

            //Assert

            Assert.IsType<StockResponse>(response);
            Assert.Null(response.Stock);
            _brapiServicesMock.Verify(x => x.GetStockByTicket(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
