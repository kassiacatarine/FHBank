using FHBank.Domain.AggregatesModel;
using FHBank.Infrastructure.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FHBank.Application.Commands
{
    public class DepositAccountCommandHandler : IRequestHandler<DepositAccountCommand, bool>
    {
        private readonly IRepository<Account> _repository;

        public DepositAccountCommandHandler(IRepository<Account> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DepositAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _repository.GetByIdAsync(request.Id);
                if (account == null)
                    return false;

                account.Deposit(request.Amount);

                await _repository.ReplaceOneAsync(x => x.Id == account.Id, account);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
