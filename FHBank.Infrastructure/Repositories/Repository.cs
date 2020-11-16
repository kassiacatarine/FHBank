using FHBank.Domain.SeedWork;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FHBank.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly IQueriesContext _context;
        private IMongoCollection<T> Collection => _context.GetCollection<T>();
        public Repository(IQueriesContext context)
        {
            _context = context;
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> filter)
        {
            var results = await Collection.FindAsync(filter);
            return results.FirstOrDefault();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var test = await _context.Accounts.FindAsync(t => t.Id == id);
            var results = await Collection.FindAsync(t => t.Id == id);
            return results.FirstOrDefault();
        }

        public async Task InsertOneAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public async Task ReplaceOneAsync(Expression<Func<T, bool>> filter, T entity)
        {
            await Collection.ReplaceOneAsync(filter, entity);
        }
    }
}
