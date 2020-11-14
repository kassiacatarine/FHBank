using MediatR;

namespace FHBank.Application.Commands
{
    public class CreateAccountCommand : IRequest<bool>
    {
        public string Holder { get; private set; }
        public decimal Balance { get; private set; }

        public CreateAccountCommand(string holder, decimal balance)
        {
            Holder = holder;
            Balance = balance;
        }
    }
}
