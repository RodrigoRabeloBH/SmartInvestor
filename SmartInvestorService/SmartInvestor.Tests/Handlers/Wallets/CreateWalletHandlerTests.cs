using AutoFixture;
using Moq;
using SmartInvestor.Application.Command.Handlers.Wallets;
using SmartInvestor.Application.Command.Requests.Wallets;
using SmartInvestor.Application.Command.Responses.Wallets;
using SmartInvestor.Domain.Entities;
using SmartInvestor.Domain.Interfaces;

namespace SmartInvestor.Tests.Handlers.Wallets
{
    public class CreateWalletHandlerTests
    {
        private readonly CreateWalletHandler _sut;
        private readonly Fixture _fixture;
        private readonly Mock<IWalletRepository> _repo;

        public CreateWalletHandlerTests()
        {
            _fixture = new Fixture();
            _repo = new Mock<IWalletRepository>();
            _sut = new CreateWalletHandler(_repo.Object);
        }

        [Fact]
        public async Task Handle()
        {
            var request = _fixture.Create<CreateWalletRequest>();

            _repo.Setup(x => x.CreateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var response = await _sut.Handle(request, new());

            Assert.IsType<CreateWalletResponse>(response);
            Assert.NotNull(response);
            Assert.NotNull(response.Wallet);
            _repo.Verify(x => x.CreateAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
