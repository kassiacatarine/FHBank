using FHBank.Domain.AggregatesModel;
using MediatR;

namespace FHBank.Domain.Events
{

    /// <summary>
    /// Event used when an account is created
    /// </summary>
    public class AccountStartedDomainEvent : INotification
    {
        public decimal Balance { get; }
        public Account Account { get; }

        public AccountStartedDomainEvent(Account account, decimal balance)
        {
            Account = account;
            Balance = balance;
        }
    }
}
