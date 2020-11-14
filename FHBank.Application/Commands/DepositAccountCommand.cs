using MediatR;

namespace FHBank.Application.Commands
{
    public class DepositAccountCommand : IRequest<bool>
    {
        public string Id { get; private set; }
        public decimal Amount { get; private set; }

        public DepositAccountCommand(string id, decimal amount)
        {
            Id = id;
            Amount = amount;
        }
    }
}
