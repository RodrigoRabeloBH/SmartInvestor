using AutoFixture;
using AutoMapper;
using Moq;
using SmartInvestor.Application.Command.Handlers.Wallets;
using SmartInvestor.Application.Command.Requests.Wallets;
using SmartInvestor.Application.Command.Responses.Wallets;
using SmartInvestor.Domain.Entities;
using SmartInvestor.Domain.Interfaces;

namespace SmartInvestor.Tests.Handlers.Wallets
{
    public class UpdateWalletHandlerTests
    {
        private readonly UpdateWalletHandler _sut;
        private readonly Fixture _fixture;
        private readonly Mock<IWalletRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;

        public UpdateWalletHandlerTests()
        {
            _fixture = new Fixture();
            _repoMock = new Mock<IWalletRepository>();
            _mapperMock = new Mock<IMapper>();
            _sut = new UpdateWalletHandler(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle()
        {
            var request = _fixture.Create<UpdateWalletRequest>();

            var wallet = _fixture.Build<Wallet>()
                .Without(x => x.Stocks)
                .Create();

            _repoMock.Setup(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mapperMock.Setup(x => x.Map(It.IsAny<UpdateWalletRequest>(), It.IsAny<Wallet>())).Returns(wallet);

            var response = await _sut.Handle(request, new());

            Assert.NotNull(response);

            Assert.True(response.Success);

            Assert.IsType<UpdateWalletResponse>(response);

            _repoMock.Verify(x => x.UpdateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()), Times.Once);

            _mapperMock.Verify(x => x.Map(It.IsAny<UpdateWalletRequest>(), It.IsAny<Wallet>()), Times.Once);
        }
    }
}
