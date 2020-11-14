using FHBank.Domain.AggregatesModel;
using FHBank.Domain.Exceptions;
using Moq;
using Xunit;

namespace FHBank.UnitTests.Domain
{
    public class AccountTest
    {
        [Theory]
        [InlineData(300, 100, 200)]
        [InlineData(200.50, 50, 150.50)]
        [InlineData(1050, 1050, 0)]
        public void TestWithdrawWhitValidAmount(decimal initialBalance, decimal amount, decimal expected)
        {
            // Arrange
            var account = new Account(It.IsAny<string>(), initialBalance);
            // Act
            account.Withdraw(amount);
            // Assert
            Assert.Equal(expected, account.Balance);
        }

        [Theory]
        [InlineData(300, 301)]
        [InlineData(200.50, 200.51)]
        public void TestWithdrawWhitInvalidAmountAndReturnsException(decimal initialBalance, decimal amount)
        {
            // Arrange
            var account = new Account(It.IsAny<string>(), initialBalance);

            // Act - Assert
            Assert.Throws<FHBankDomainException>(() => account.Withdraw(amount));
        }

        [Theory]
        [InlineData(300, 600)]
        [InlineData(10.49, 200.51)]
        public void TestDepositWhitValidAmount(decimal initialBalance, decimal amount)
        {
            // Arrange
            var expected = initialBalance + amount;
            var account = new Account(It.IsAny<string>(), initialBalance);

            // Act
            account.Deposit(amount);
            // Assert
            Assert.Equal(expected, account.Balance);
        }

        [Theory]
        [InlineData(300, 0)]
        [InlineData(10.49, -130.99)]
        public void TestDepositWhitInvalidAmountAndReturnsException(decimal initialBalance, decimal amount)
        {
            // Arrange
            var account = new Account(It.IsAny<string>(), initialBalance);

            // Act - Assert
            Assert.Throws<FHBankDomainException>(() => account.Deposit(amount));
        }
    }
}
