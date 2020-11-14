using FHBank.Domain.Exceptions;
using FHBank.Domain.SeedWork;
using System;

namespace FHBank.Domain.AggregatesModel
{
    public class Account : Entity
    {
        public decimal Balance { get; private set; }

        public Account(decimal balance)
        {
            Balance = balance;
        }

        public void Withdraw(decimal amount)
        {
            if (Balance < amount)
                throw new FHBankDomainException($"Is not possible to withdraw the {amount} amount from the {Balance}.");

            Balance = Balance - amount;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new FHBankDomainException($"Is not possible to deposit the {amount} amount.");

            Balance = Balance + amount;
        }
    }
}
