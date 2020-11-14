using FHBank.Application.Commands;
using FHBank.Domain.AggregatesModel;
using FHBank.Infrastructure.Repositories;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FHBank.UnitTests.Application.Commands
{
    public class WithdrawAccountCommandHandlerTest
    {
        private readonly Mock<IRepository<Account>> _accountRepositoryMock;
        public WithdrawAccountCommandHandlerTest()
        {
            _accountRepositoryMock = new Mock<IRepository<Account>>();
        }

        [Fact]
        public async Task TestHandleWithdrawAccountAndReturnsFalseIfAccountNotFound()
        {
            // Arrange
            var request = new WithdrawAccountCommand("123", 100M);
            _accountRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(It.IsAny<Account>()));
            var handler = new WithdrawAccountCommandHandler(_accountRepositoryMock.Object);
            // Act
            var result = await handler.Handle(request, new CancellationToken());
            // Assert
            _accountRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            Assert.False(result);
        }

        [Theory]
        [InlineData(100.50)]
        [InlineData(300)]
        public async Task TestHandleWithdrawAccountAndReturnsExceptionIfAmountInvalid(decimal amount)
        {
            // Arrange
            var request = new WithdrawAccountCommand("123", amount);
            _accountRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new Account("User", 100M)));
            var handler = new WithdrawAccountCommandHandler(_accountRepositoryMock.Object);
            // Act
            var result = await handler.Handle(request, new CancellationToken());
            // Assert
            _accountRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            Assert.False(result);
        }

        [Theory]
        [InlineData(60.59)]
        [InlineData(1)]
        public async Task TestHandleWithdrawAccountAndReturnsTrueIfAmountValid(decimal amount)
        {
            // Arrange
            var request = new WithdrawAccountCommand("123", amount);
            _accountRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new Account("User", 100M)));

            _accountRepositoryMock.Setup(x => x.ReplaceOneAsync(It.IsAny<Expression<Func<Account, bool>>>(), It.IsAny<Account>()))
                .Returns(Task.CompletedTask);
            var handler = new WithdrawAccountCommandHandler(_accountRepositoryMock.Object);
            // Act
            var result = await handler.Handle(request, new CancellationToken());
            // Assert
            _accountRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _accountRepositoryMock.Verify(x => x.ReplaceOneAsync(It.IsAny<Expression<Func<Account, bool>>>(), It.IsAny<Account>()), Times.Once);
            Assert.True(result);
        }
    }
}
