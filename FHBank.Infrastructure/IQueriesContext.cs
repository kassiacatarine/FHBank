﻿using FHBank.Domain.AggregatesModel;
using FHBank.Domain.SeedWork;
using MongoDB.Driver;

namespace FHBank.Infrastructure
{
    public interface IQueriesContext
    {
        public IMongoCollection<Account> Accounts { get; }
        public IMongoCollection<T> GetCollection<T>() where T : Entity;
    }
}
