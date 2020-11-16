using FHBank.API.Application.Commands;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FHBank.API.Application.Mutations.Account
{
    [ExtendObjectType(Name = "Mutation")]
    public class AccountMutations
    {
        public async Task<AccountPayload> CreateAccount(
            decimal valor,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new CreateAccountCommand(valor), cancellationToken);
        }
        public async Task<AccountPayload> Sacar(
            int conta,
            decimal valor,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new WithdrawAccountCommand(conta, valor), cancellationToken);
        }
        
        public async Task<AccountPayload> Depositar(
            int conta,
            decimal valor,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new DepositAccountCommand(conta, valor), cancellationToken);
        }
    }
}

