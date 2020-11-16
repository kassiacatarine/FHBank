using FHBank.Domain.AggregatesModel;
using FHBank.Infrastructure.Repositories;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Types;
using System.Threading.Tasks;

namespace FHBank.Application.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class AccountQueries
    {
        /// <summary>
        /// Gets a balance by its account id.
        /// </summary>
        public async Task<decimal> Saldo(
            int conta,
            [Service] IRepository<Account> repository)
        {
            var account = await repository.FindOneAsync(x => x.Number.Equals(conta));
            if (account is null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The specified conta are invalid.")
                        .SetCode("INVALID_CREDENTIALS")
                        .Build());
            }
            return account.Balance;
        }
    }
}
