using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MsftFramework.Abstractions.Persistence.Mongo;
using MsftFramework.Persistence.Mongo;

namespace MS.Services.TaskCatalog.Infrastructure.Shared.Data;

public class TaskCatalogReadDbContext : MongoDbContext
{
    public TaskCatalogReadDbContext(IOptions<MongoOptions> options) : base(options.Value)
    {
        //Products = GetCollection<ProductReadModel>(nameof(Products).Underscore());
    }

    //public IMongoCollection<ProductReadModel> Products { get; }
}