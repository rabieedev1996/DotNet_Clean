using Clean.Domain.Entities.Mongo;
using Clean.Domain.Entities.SQL;
using Clean.Domain.Enums;

namespace Clean.Application.Contract.MongoDB;

public interface IMongoSampleEntityRepository:IBaseRepository<MongoSampleEntity>
{

}