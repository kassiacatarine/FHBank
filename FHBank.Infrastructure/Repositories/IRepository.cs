using FHBank.Domain.SeedWork;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FHBank.Infrastructure.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> FindOneAsync(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(string id);
        Task InsertOneAsync(T entity);
        Task ReplaceOneAsync(Expression<Func<T, bool>> filter, T entity);
    }
}
