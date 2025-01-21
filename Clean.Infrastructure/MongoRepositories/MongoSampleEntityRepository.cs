using Clean.Application.Contract.MongoDB;
using Clean.Domain;
using Clean.Domain.Entities.Mongo;
using Clean.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Clean.Infrastructure.MongoRepositories;

public class MongoSampleEntityRepository : BaseRepository<MongoSampleEntity>, IMongoSampleEntityRepository
{
    public MongoSampleEntityRepository(Configs configuration) : base(configuration)
    {
    }
  
}