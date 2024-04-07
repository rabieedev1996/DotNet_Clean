using Clean.Application.Contract.Services;
using Clean.Utility;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;


namespace Clean.Infrastructure.Service;

public class LogService : ILogService
{
    private IConfiguration _configuration;

    public LogService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task InsertLog<TData>(string category, TData body)
    {
        var date = DateTime.Now;
        var connectionString = _configuration.GetConnectionString("IPLAMongoDB");
        var data = new LogModel<TData>()
        {
            Body = body,
            Date = date.ToString("yyyy-MM-dd"),
            JalaliDate = date.ToFa("yyyy-MM-dd"),
            DateKey = long.Parse(date.ToString("yyyyMMddHHmmss")),
            JalaliDateKey = long.Parse(date.ToFa("yyyyMMddHHmmss")),
            Time = date.ToString("hh:mm:ss")
        };
        var client = new MongoClient(connectionString);
        var collection = client.GetDatabase("IPLA_LOG").GetCollection<BsonDocument>(category);
        await collection.InsertOneAsync(data.ToBsonDocument());
    }

    class LogModel<TData>
    {
        public string Date { set; get; }
        public string JalaliDate { set; get; }
        public string Time { set; get; }
        public long DateKey { set; get; }
        public long JalaliDateKey { set; get; }
        public TData Body { set; get; }
    }
}