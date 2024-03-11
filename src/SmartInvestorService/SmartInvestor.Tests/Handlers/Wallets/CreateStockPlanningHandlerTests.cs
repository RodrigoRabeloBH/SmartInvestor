using AutoFixture;
using AutoMapper;
using Moq;
using SmartInvestor.Application.Command.Handlers.Wallets;
using SmartInvestor.Application.Command.Requests.Wallets;
using SmartInvestor.Application.Command.Responses.Wallets;
using SmartInvestor.Domain.Entities;
using SmartInvestor.Domain.Interfaces;
using SmartInvestor.Domain.Models;

namespace SmartInvestor.Tests.Handlers.Wallets
{
    public class CreateStockPlanningHandlerTests
    {
        private readonly CreateStockPlanningHandler _sut;
        private readonly Fixture _fixture;

        private readonly Mock<IWalletRepository> _repMock;
        private readonly Mock<IBrapiServices> _brapiServicesMock;
        private readonly Mock<IMapper> _mapperMock;

        public CreateStockPlanningHandlerTests()
        {
            _fixture = new Fixture();

            _repMock = new Mock<IWalletRepository>();
            _brapiServicesMock = new Mock<IBrapiServices>();
            _mapperMock = new Mock<IMapper>();

            _sut = new CreateStockPlanningHandler(_repMock.Object, _brapiServicesMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handler_StockAlreadyExists_ShouldReturn_CreateStockPlanningResponse()
        {
            // Arrange

            var request = _fixture.Create<CreateStockPlanningRequest>();

            var walet = _fixture.Build<Wallet>()
                .Without(x => x.Stocks)
                .Create();

            var stockDetail = _fixture.Create<StockDetail>();

            var stockPlanning = _fixture.Build<StockPlanning>()
                .With(x => x.Ticket, request.Ticket)
                .Without(x => x.Wallet)
                .Create();

            walet.Stocks.Add(stockPlanning);

            _mapperMock.Setup(x => x.Map<StockPlanning>(It.IsAny<CreateStockPlanningRequest>())).Returns(stockPlanning);

            _brapiServicesMock.Setup(x => x.GetStockByTicket(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(stockDetail);

            _repMock.Setup(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            _repMock.Setup(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(walet);

            //Act

            var response = await _sut.Handle(request, new());

            //Assert

            _mapperMock.Verify(x => x.Map<StockPlanning>(It.IsAny<CreateStockPlanningRequest>()), Times.Once);

            _brapiServicesMock.Verify(x => x.GetStockByTicket(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);

            _repMock.Verify(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            _repMock.Verify(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.IsType<CreateStockPlanningResponse>(response);
        }

        [Fact]
        public async Task Handler_ShouldReturn_CreateStockPlanningResponse()
        {
            // Arrange

            var request = _fixture.Create<CreateStockPlanningRequest>();

            var walet = _fixture.Build<Wallet>()
                .Without(x => x.Stocks)
                .Create();

            var stockDetail = _fixture.Create<StockDetail>();

            var stockPlanning = _fixture.Build<StockPlanning>()
                .Without(x => x.Wallet)
                .Create();

            _mapperMock.Setup(x => x.Map<StockPlanning>(It.IsAny<CreateStockPlanningRequest>())).Returns(stockPlanning);

            _brapiServicesMock.Setup(x => x.GetStockByTicket(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(stockDetail);

            _repMock.Setup(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            _repMock.Setup(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(walet);

            //Act

            var response = await _sut.Handle(request, new());

            //Assert

            _mapperMock.Verify(x => x.Map<StockPlanning>(It.IsAny<CreateStockPlanningRequest>()), Times.Once);

            _brapiServicesMock.Verify(x => x.GetStockByTicket(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);

            _repMock.Verify(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            _repMock.Verify(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.IsType<CreateStockPlanningResponse>(response);
        }

        [Fact]
        public async Task Handler_Should_Throw_ApplicationException_WalletNotFound()
        {
            // Arrange

            var request = _fixture.Create<CreateStockPlanningRequest>();

            var stockDetail = _fixture.Create<StockDetail>();

            var stockPlanning = _fixture.Build<StockPlanning>()
                .Without(x => x.Wallet)
                .Create();

            _mapperMock.Setup(x => x.Map<StockPlanning>(It.IsAny<CreateStockPlanningRequest>())).Returns(stockPlanning);

            _brapiServicesMock.Setup(x => x.GetStockByTicket(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(stockDetail);

            _repMock.Setup(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            _repMock.Setup(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(value: null);


            //Assert

            await Assert.ThrowsAsync<ApplicationException>(async () => await _sut.Handle(request, new()));

            _mapperMock.Verify(x => x.Map<StockPlanning>(It.IsAny<CreateStockPlanningRequest>()), Times.Never);

            _brapiServicesMock.Verify(x => x.GetStockByTicket(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);

            _repMock.Verify(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            _repMock.Verify(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handler_Should_Throw_ApplicationException_TicketNotFound()
        {
            // Arrange

            var request = _fixture.Create<CreateStockPlanningRequest>();

            var walet = _fixture.Build<Wallet>()
                .Without(x => x.Stocks)
                .Create();

            var stockPlanning = _fixture.Build<StockPlanning>()
                .Without(x => x.Wallet)
                .Create();

            _mapperMock.Setup(x => x.Map<StockPlanning>(It.IsAny<CreateStockPlanningRequest>())).Returns(stockPlanning);

            _brapiServicesMock.Setup(x => x.GetStockByTicket(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(value: null);

            _repMock.Setup(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            _repMock.Setup(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(walet);


            //Assert

            await Assert.ThrowsAsync<ApplicationException>(async () => await _sut.Handle(request, new()));

            _mapperMock.Verify(x => x.Map<StockPlanning>(It.IsAny<CreateStockPlanningRequest>()), Times.Never);

            _brapiServicesMock.Verify(x => x.GetStockByTicket(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);

            _repMock.Verify(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            _repMock.Verify(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
