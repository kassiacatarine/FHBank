﻿using FHBank.Domain.Exceptions;
using FHBank.Domain.SeedWork;

namespace FHBank.Domain.AggregatesModel
{
    public class Account : Entity
    {
        public int Number { get; private set; }
        public string Holder { get; private set; }
        public decimal Balance { get; private set; }

        public Account(string holder, decimal balance)
        {
            Holder = holder;
            Deposit(balance);
        }

        public void Withdraw(decimal amount)
        {
            if (Balance < amount)
                throw new FHBankDomainException($"Is not possible to withdraw the {amount} amount from the {Balance}.");

            Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new FHBankDomainException($"Is not possible to deposit the {amount} amount.");

            Balance += amount;
        }
    }
}
