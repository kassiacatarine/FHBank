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
    public class DepositAccountCommandHandlerTest
    {
        private readonly Mock<IRepository<Account>> _accountRepositoryMock;
        public DepositAccountCommandHandlerTest()
        {
            _accountRepositoryMock = new Mock<IRepository<Account>>();
        }

        [Fact]
        public void TestHandleDepositAccountAndReturnsFalseIfAccountNotFound()
        {
            // Arrange
            var request = new DepositAccountCommand(123, 100M);
            _accountRepositoryMock.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns(Task.FromResult(It.IsAny<Account>()));
            var handler = new DepositAccountCommandHandler(_accountRepositoryMock.Object);
            // Act -- Assert
            handler.Invoking(x => x.Handle(request, new CancellationToken()))
                .Should().Throw<QueryException>()
                .WithMessage("The specified conta number are invalid.");
            _accountRepositoryMock.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<Account, bool>>>()), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void TestHandleDepositAccountAndReturnsExceptionIfAmountInvalid(decimal amount)
        {
            // Arrange
            var request = new DepositAccountCommand(123, amount);
            _accountRepositoryMock.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns(Task.FromResult(new Account(100M)));
            var handler = new DepositAccountCommandHandler(_accountRepositoryMock.Object);
            // Act -- Assert
            handler.Invoking(x => x.Handle(request, new CancellationToken()))
                .Should().Throw<QueryException>()
                .WithMessage("Valor inválido.");
            _accountRepositoryMock.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<Account, bool>>>()), Times.Once);
        }

        [Theory]
        [InlineData(10.09)]
        [InlineData(1)]
        public async Task TestHandleDepositAccountAndReturnsTrueIfAmountValid(decimal amount)
        {
            // Arrange
            var request = new DepositAccountCommand(123, amount);
            _accountRepositoryMock.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<Account, bool>>>()))
                .Returns(Task.FromResult(new Account(100M)));

            _accountRepositoryMock.Setup(x => x.ReplaceOneAsync(It.IsAny<Expression<Func<Account, bool>>>(), It.IsAny<Account>()))
                .Returns(Task.CompletedTask);
            var handler = new DepositAccountCommandHandler(_accountRepositoryMock.Object);
            // Act
            var result = await handler.Handle(request, new CancellationToken());
            // Assert
            _accountRepositoryMock.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<Account, bool>>>()), Times.Once);
            _accountRepositoryMock.Verify(x => x.ReplaceOneAsync(It.IsAny<Expression<Func<Account, bool>>>(), It.IsAny<Account>()), Times.Once);
            result.Saldo.Should()
                .Be(100M + amount);
        }
    }
}
