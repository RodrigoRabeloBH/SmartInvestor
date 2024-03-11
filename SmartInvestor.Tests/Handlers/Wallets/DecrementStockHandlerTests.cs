using AutoFixture;
using Moq;
using SmartInvestor.Application.Command.Handlers.Wallets;
using SmartInvestor.Application.Command.Requests.Wallets;
using SmartInvestor.Application.Command.Responses.Wallets;
using SmartInvestor.Domain.Entities;
using SmartInvestor.Domain.Interfaces;

namespace SmartInvestor.Tests.Handlers.Wallets
{
    public class DecrementStockHandlerTests
    {
        private readonly DecrementStockHandler _sut;
        private readonly Fixture _fixture;
        private readonly Mock<IWalletRepository> _repoMock;

        public DecrementStockHandlerTests()
        {
            _fixture = new Fixture();
            _repoMock = new Mock<IWalletRepository>();
            _sut = new DecrementStockHandler(_repoMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_DecrementStockResponse()
        {
            //Arrange

            var request = _fixture.Create<DecrementStockRequest>();

            var wallet = _fixture.Build<Wallet>()
                .Without(x => x.Stocks)
                .Create();

            var stokPlanning = _fixture.Build<StockPlanning>()
                .Without(x => x.Wallet)
                .With(x => x.Ticket, request.Ticket)
                .Create();

            wallet.Stocks.Add(stokPlanning);

            _repoMock.Setup(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(wallet);

            _repoMock.Setup(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Act

            var response = await _sut.Handle(request, new());

            //Assert

            Assert.IsType<DecrementStockResponse>(response);
            Assert.NotNull(response);
            Assert.NotNull(response.Wallet);
            Assert.True(response.Success);

            _repoMock.Verify(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repoMock.Verify(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Throws_ApplicationException_TicketNotFound()
        {
            //Arrange

            var request = _fixture.Create<DecrementStockRequest>();

            var wallet = _fixture.Build<Wallet>()
                .Without(x => x.Stocks)
                .Create();

            var stokPlanning = _fixture.Build<StockPlanning>()
                .Without(x => x.Wallet)
                .With(x => x.Ticket, request.Ticket)
                .Create();

            _repoMock.Setup(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(wallet);

            _repoMock.Setup(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Assert

            await Assert.ThrowsAsync<ApplicationException>(async () => await _sut.Handle(request, new()));

            _repoMock.Verify(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repoMock.Verify(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Throws_ApplicationException_WalletNotFound()
        {
            //Arrange

            var request = _fixture.Create<DecrementStockRequest>();

            _repoMock.Setup(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);

            _repoMock.Setup(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            //Assert

            await Assert.ThrowsAsync<ApplicationException>(async () => await _sut.Handle(request, new()));

            _repoMock.Verify(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repoMock.Verify(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
