using FHBank.API.Application.Commands;
using FHBank.Domain.AggregatesModel;
using FHBank.Infrastructure.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FHBank.UnitTests.Application.Commands
{
    public class CreateAccountCommandHandlerTest
    {
        private readonly Mock<IRepository<Account>> _accountRepositoryMock;
        public CreateAccountCommandHandlerTest()
        {
            _accountRepositoryMock = new Mock<IRepository<Account>>();
        }

        [Fact]
        public async Task TestHandleCreateAccountAndReturnsTrueIfIsPersisted()
        {
            // Arrange
            var request = new CreateAccountCommand(100M);
            _accountRepositoryMock.Setup(x => x.InsertOneAsync(It.IsAny<Account>()))
                .Returns(Task.CompletedTask);
            var handler = new CreateAccountCommandHandler(_accountRepositoryMock.Object);
            // Act
            var result = await handler.Handle(request, new CancellationToken());
            // Assert
            _accountRepositoryMock.Verify(x => x.InsertOneAsync(It.IsAny<Account>()), Times.Once);
            result.Should()
                .NotBeNull();
        }
    }
}
