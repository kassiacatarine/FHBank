using FHBank.Domain.Exceptions;
using FHBank.Domain.SeedWork;
using System;
using System.Security.Cryptography;

namespace FHBank.Domain.AggregatesModel
{
    public class Account : Entity
    {
        public int Number { get; private set; }
        public decimal Balance { get; private set; }

        public Account(decimal balance)
        {
            Number = int.Parse(Get8Digits());
            Deposit(balance);
        }

        private string Get8Digits()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return String.Format("{0:D8}", random);
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
