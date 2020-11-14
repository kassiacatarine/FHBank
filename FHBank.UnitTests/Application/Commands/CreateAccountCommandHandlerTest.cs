using FHBank.Application.Commands;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FHBank.UnitTests.Application.Commands
{
    public class CreateAccountCommandHandlerTest
    {
        [Fact]
        public async Task TestHandleCreateAccountAndReturnsFalseIfIsNotPersisted()
        {
            // Arrange
            var handler = new CreateAccountCommandHandler();
            // Act
            var result = await handler.Handle(It.IsAny<CreateAccountCommand>(), new CancellationToken());
            // Assert
            Assert.False(result);
        }
    }
}
