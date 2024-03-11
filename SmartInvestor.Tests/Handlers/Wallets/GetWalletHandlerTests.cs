using AutoFixture;
using Moq;
using SmartInvestor.Application.Command.Handlers.Wallets;
using SmartInvestor.Application.Command.Requests.Wallets;
using SmartInvestor.Application.Command.Responses.Wallets;
using SmartInvestor.Domain.Entities;
using SmartInvestor.Domain.Interfaces;

namespace SmartInvestor.Tests.Handlers.Wallets
{
    public class GetWalletHandlerTests
    {
        private readonly GetWalletHandler _sut;
        private readonly Fixture _fixture;
        private readonly Mock<IWalletRepository> _repoMock;

        public GetWalletHandlerTests()
        {
            _fixture = new Fixture();
            _repoMock = new Mock<IWalletRepository>();
            _sut = new GetWalletHandler(_repoMock.Object);
        }

        [Fact]
        public async Task Handle()
        {
            var request = _fixture.Create<GetWalletRequest>();

            var wallet = _fixture.Build<Wallet>()
                .Without(x => x.Stocks)
                .Create();

            _repoMock.Setup(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(wallet);

            var response = await _sut.Handle(request, new());

            Assert.IsType<GetWalletResponse>(response);
            Assert.NotNull(response);
            Assert.NotNull(response.Wallet);
            _repoMock.Verify(x => x.GetWallet(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
