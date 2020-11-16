using FHBank.API.Application.Mutations.Account;
using MediatR;

namespace FHBank.API.Application.Commands
{
    public class DepositAccountCommand : IRequest<AccountPayload>
    {
        public int Number { get; private set; }
        public decimal Amount { get; private set; }

        public DepositAccountCommand(int number, decimal amount)
        {
            Number = number;
            Amount = amount;
        }
    }
}
