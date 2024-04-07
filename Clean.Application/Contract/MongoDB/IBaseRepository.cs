using System.Linq.Expressions;
using Clean.Domain.Entities.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Clean.Application.Contract.MongoDB;

public interface IBaseRepository<TDocument> where TDocument : DocumentBase
{

    

    List<TDocument> GetAll();
    TDocument FindById(string id);
    Task<TDocument> FindByIdAsync(string id);


    void InsertOne(TDocument document);

    Task InsertOneAsync(TDocument document);
    void InsertMany(ICollection<TDocument> documents);


    Task InsertManyAsync(ICollection<TDocument> documents);

    void ReplaceOne(TDocument document);

    Task ReplaceOneAsync(TDocument document);

    void DeleteById(string id);
    Task DeleteByIdAsync(string id);
    void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);
    Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
}