using FHBank.Domain.AggregatesModel;
using FHBank.Infrastructure.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FHBank.Application.Commands
{
    public class WithdrawAccountCommandHandler : IRequestHandler<WithdrawAccountCommand, bool>
    {
        private readonly IRepository<Account> _repository;

        public WithdrawAccountCommandHandler(IRepository<Account> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(WithdrawAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _repository.GetByIdAsync(request.Id);
                if (account == null)
                    return false;

                account.Withdraw(request.Amount);

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
