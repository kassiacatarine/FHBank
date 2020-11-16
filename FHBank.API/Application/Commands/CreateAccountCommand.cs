using FHBank.API.Application.Mutations.Account;
using MediatR;

namespace FHBank.API.Application.Commands
{
    public class CreateAccountCommand : IRequest<AccountPayload>
    {
        public decimal Balance { get; private set; }

        public CreateAccountCommand(decimal balance)
        {
            Balance = balance;
        }
    }
}
