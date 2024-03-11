using AutoFixture;
using Moq;
using SmartInvestor.Application.Command.Handlers.Wallets;
using SmartInvestor.Application.Command.Requests.Wallets;
using SmartInvestor.Application.Command.Responses.Wallets;
using SmartInvestor.Domain.Entities;
using SmartInvestor.Domain.Interfaces;

namespace SmartInvestor.Tests.Handlers.Wallets
{
    public class RemoveWalletHandlerTests
    {
        private readonly RemoveWalletHandler _sut;
        private readonly Fixture _fixture;
        private readonly Mock<IWalletRepository> _repoMock;

        public RemoveWalletHandlerTests()
        {
            _fixture = new Fixture();
            _repoMock = new Mock<IWalletRepository>();
            _sut = new RemoveWalletHandler(_repoMock.Object);
        }

        [Fact]
        public async Task Handle()
        {
            var request = _fixture.Create<RemoveWalletRequest>();

            var wallet = _fixture.Build<Wallet>()
                .Without(x => x.Stocks)
                .Create();

            _repoMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var response = await _sut.Handle(request, new());

            Assert.IsType<RemoveWalletResponse>(response);
            Assert.True(response.Success);
            _repoMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
