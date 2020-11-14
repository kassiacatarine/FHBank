using FHBank.Domain.AggregatesModel;
using FHBank.Infrastructure.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FHBank.Application.Commands
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, bool>
    {
        private readonly IRepository<Account> _repository;

        public CreateAccountCommandHandler(IRepository<Account> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = new Account(request.Holder, request.Balance);
                await _repository.InsertOneAsync(account);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
