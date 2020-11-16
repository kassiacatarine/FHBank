using FHBank.API.Application.Mutations.Account;
using FHBank.Domain.AggregatesModel;
using FHBank.Infrastructure.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FHBank.API.Application.Commands
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountPayload>
    {
        private readonly IRepository<Account> _repository;

        public CreateAccountCommandHandler(IRepository<Account> repository)
        {
            _repository = repository;
        }

        public async Task<AccountPayload> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account(request.Balance);
            await _repository.InsertOneAsync(account);
            return new AccountPayload(account.Number, account.Balance);
        }
    }
}
