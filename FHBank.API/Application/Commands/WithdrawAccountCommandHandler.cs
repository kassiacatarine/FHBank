using FHBank.API.Application.Mutations.Account;
using FHBank.Domain.AggregatesModel;
using FHBank.Domain.Exceptions;
using FHBank.Infrastructure.Repositories;
using HotChocolate;
using HotChocolate.Execution;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FHBank.API.Application.Commands
{
    public class WithdrawAccountCommandHandler : IRequestHandler<WithdrawAccountCommand, AccountPayload>
    {
        private readonly IRepository<Account> _repository;

        public WithdrawAccountCommandHandler(IRepository<Account> repository)
        {
            _repository = repository;
        }

        public async Task<AccountPayload> Handle(WithdrawAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _repository.FindOneAsync(x => x.Number.Equals(request.Number));
                if (account is null)
                    throw new QueryException(
                        ErrorBuilder.New()
                            .SetMessage("The specified conta number are invalid.")
                            .SetCode("INVALID_CREDENTIALS")
                            .Build());

                account.Withdraw(request.Amount);

                await _repository.ReplaceOneAsync(x => x.Id == account.Id, account);

                return new AccountPayload(account.Number, account.Balance);
            }
            catch (FHBankDomainException e)
            {
                throw new QueryException(
                        ErrorBuilder.New()
                            .SetMessage("Saldo Insuficiente.")
                            .SetExtension("category", "graphql")
                            .SetException(e)
                            .Build());
            }
        }
    }
}
