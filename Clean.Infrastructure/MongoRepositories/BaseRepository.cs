using System.Linq.Expressions;
using System.Security.Authentication;
using Clean.Application.Contract.MongoDB;
using Clean.Domain;
using Clean.Domain.Entities.Mongo;
using Clean.Domain.Enums;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Clean.Infrastructure.MongoRepositories;

public class BaseRepository<TDocument> : IBaseRepository<TDocument> where TDocument : DocumentBase
{
    protected readonly IMongoCollection<TDocument> _collection;

    public BaseRepository(Configs configs)
    {
        var client = new MongoClient(configs.MongoConfigs.ConnectionString);
        var database = client.GetDatabase(configs.MongoConfigs.Database);
       _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));

    }

    private protected string GetCollectionName(Type documentType)
    {
        return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
            .FirstOrDefault())?.CollectionName;
    }

    protected  IMongoQueryable<TDocument> GetQueryable()
    {
        return _collection.AsQueryable().Where(a=>a.IsDeleted==false);
    }

    

    public virtual TDocument FindById(string id)
    {
        var objectId = new ObjectId(id);
        var filter = GetQueryable().Where(a => a.Id == objectId);
        return filter.FirstOrDefault();
    }

    public virtual async Task<TDocument> FindByIdAsync(string id)
    {
        var objectId = new ObjectId(id);
        var filter = GetQueryable().Where(a => a.Id == objectId);
        return await filter.FirstOrDefaultAsync();
    }


    public virtual void InsertOne(TDocument document)
    {
        _collection.InsertOne(document);
    }

    public virtual Task InsertOneAsync(TDocument document)
    {
        return Task.Run(() => _collection.InsertOneAsync(document));
    }

    public void InsertMany(ICollection<TDocument> documents)
    {
        _collection.InsertMany(documents);
    }


    public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
    {
        await _collection.InsertManyAsync(documents);
    }

    public void ReplaceOne(TDocument document)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
        _collection.FindOneAndReplace(filter, document);
    }

    public virtual async Task ReplaceOneAsync(TDocument document)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
        await _collection.FindOneAndReplaceAsync(filter, document);
    }
    
    public void DeleteById(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
        _collection.FindOneAndDelete(filter);
    }

    public Task DeleteByIdAsync(string id)
    {
        return Task.Run(() =>
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            _collection.FindOneAndDeleteAsync(filter);
        });
    }

    public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
    {
        _collection.DeleteMany(filterExpression);
    }

    public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
    }

    public List<TDocument> GetAll()
    {
       return _collection.Find(_=>true).ToList();
    }
}