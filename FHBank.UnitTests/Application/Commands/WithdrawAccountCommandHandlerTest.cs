using FHBank.API.Application.Commands;
using FHBank.Domain.AggregatesModel;
using FHBank.Infrastructure.Repositories;
using FluentAssertions;
using HotChocolate.Execution;
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
        public void TestHandleWithdrawAccountAndReturnsFalseIfAccountNotFound()
        {
            // Arrange
            var request = new WithdrawAccountCommand(123, 100M);
            _accountRepositoryMock.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns(Task.FromResult(It.IsAny<Account>()));
            var handler = new WithdrawAccountCommandHandler(_accountRepositoryMock.Object);
            // Act -- Assert
            handler.Invoking(x => x.Handle(request, new CancellationToken()))
                .Should().Throw<QueryException>()
                .WithMessage("The specified conta number are invalid.");
            _accountRepositoryMock.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<Account, bool>>>()), Times.Once);
        }

        [Theory]
        [InlineData(100.50)]
        [InlineData(300)]
        public void TestHandleWithdrawAccountAndReturnsExceptionIfAmountInvalid(decimal amount)
        {
            // Arrange
            var request = new WithdrawAccountCommand(123, amount);
            _accountRepositoryMock.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns(Task.FromResult(new Account(100M)));
            var handler = new WithdrawAccountCommandHandler(_accountRepositoryMock.Object);
            // Act -- Assert
            handler.Invoking(x => x.Handle(request, new CancellationToken()))
                .Should().Throw<QueryException>()
                .WithMessage("Saldo Insuficiente.");
            _accountRepositoryMock.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<Account, bool>>>()), Times.Once);
        }

        [Theory]
        [InlineData(60.59)]
        [InlineData(1)]
        [InlineData(100)]
        public async Task TestHandleWithdrawAccountAndReturnsTrueIfAmountValid(decimal amount)
        {
            // Arrange
            var request = new WithdrawAccountCommand(123, amount);
            _accountRepositoryMock.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns(Task.FromResult(new Account(100M)));

            _accountRepositoryMock.Setup(x => x.ReplaceOneAsync(It.IsAny<Expression<Func<Account, bool>>>(), It.IsAny<Account>()))
                .Returns(Task.CompletedTask);
            var handler = new WithdrawAccountCommandHandler(_accountRepositoryMock.Object);
            // Act
            var result = await handler.Handle(request, new CancellationToken());
            // Assert
            _accountRepositoryMock.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<Account, bool>>>()), Times.Once);
            _accountRepositoryMock.Verify(x => x.ReplaceOneAsync(It.IsAny<Expression<Func<Account, bool>>>(), It.IsAny<Account>()), Times.Once);
            result.Saldo.Should()
                .Be(100M - amount);
        }
    }
}
