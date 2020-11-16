using FHBank.API.Application.Mutations.Account;
using MediatR;

namespace FHBank.API.Application.Commands
{
    public class WithdrawAccountCommand : IRequest<AccountPayload>
    {
        public int Number { get; private set; }
        public decimal Amount { get; private set; }

        public WithdrawAccountCommand(int number, decimal amount)
        {
            Number = number;
            Amount = amount;
        }
    }
}
