using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using SmartInvestor.Application.Command.Handlers.Stocks;
using SmartInvestor.Application.Command.Requests.Stocks;
using SmartInvestor.Application.Command.Responses.Stocks;
using SmartInvestor.Domain.Interfaces;
using SmartInvestor.Domain.Models;
using SmartInvestor.Domain.Utils;

namespace SmartInvestor.Tests.Handlers.Stocks
{
    public class StockListHandlerTests
    {
        private readonly StockListHandler _sut;
        private readonly Fixture _fixture;

        private readonly Mock<IBrapiServices> _brapiServicesMock;
        private readonly Mock<ILogger<StockHandler>> _loggerMock;

        public StockListHandlerTests()
        {
            _fixture = new Fixture();

            _brapiServicesMock = new Mock<IBrapiServices>();
            _loggerMock = new Mock<ILogger<StockHandler>>();

            _sut = new StockListHandler(_brapiServicesMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_SouldReturnStockResponseWithStock()
        {
            // Arrange

            var request = _fixture.Create<StockListRequest>();

            var stocks = _fixture.Create<List<Stock>>();

            _brapiServicesMock.Setup(x => x.GetStocks(It.IsAny<QueryParams>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(stocks);

            //Act

            var response = await _sut.Handle(request, new());

            //Assert

            Assert.NotNull(response);

            Assert.NotNull(response.Stocks);

            Assert.IsType<StockListResponse>(response);

            _brapiServicesMock.Verify(x => x.GetStocks(It.IsAny<QueryParams>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_SouldReturnNull()
        {
            // Arrange

            var request = _fixture.Create<StockListRequest>();

            _brapiServicesMock.Setup(x => x.GetStocks(It.IsAny<QueryParams>(), It.IsAny<CancellationToken>()))
              .ReturnsAsync(() => null);

            //Act

            var response = await _sut.Handle(request, new());

            //Assert

            Assert.IsType<StockListResponse>(response);

            Assert.Null(response.Stocks);

            _brapiServicesMock.Verify(x => x.GetStocks(It.IsAny<QueryParams>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
