using FHBank.Domain.AggregatesModel;
using FHBank.Domain.SeedWork;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace FHBank.Infrastructure
{
    public class FHBankContext
    {
        private readonly IMongoDatabase _db;
        private Dictionary<Type, string> DocumentNames { get; } = new Dictionary<Type, string>();
        public FHBankContext(IOptions<DbSettings> settings, IMongoClient client)
        {
            _db = client.GetDatabase(settings.Value.Database);
            DocumentNames.Add(typeof(Account), $"accounts");
        }

        public IMongoCollection<Account> Accounts => _db.GetCollection<Account>(CollectionName<Account>());

        private string CollectionName<T>() where T : Entity { return DocumentNames[typeof(T)]; }
        public IMongoCollection<T> GetCollection<T>() where T : Entity
        {
            return _db.GetCollection<T>(CollectionName<T>());
        }
    }
}
