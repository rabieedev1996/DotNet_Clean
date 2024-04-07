using System;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Clean.Utility;

namespace Clean.Domain.Entities.Mongo;

public class DocumentBase
{
    public DocumentBase()
    {
        var now = DateTime.Now;
        CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        IsDeleted = false;
        IsEnable = true;
        JalaliCreatedAt = now.ToFa("yyyy/MM/dd");
        JalaliDateKey = int.Parse(now.ToFa("yyyyMMdd"));
        MiladiDateKey = long.Parse(now.ToString("yyyyMMddHHmmss"));
    }

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public ObjectId Id { set; get; }
    public string CreatedAt { set; get; }
    public string JalaliCreatedAt { set; get; }
    public bool IsDeleted { set; get; }
    public bool IsEnable { set; get; }
    public bool IsPublished { set; get; }
    public long JalaliDateKey { set; get; }
    public long MiladiDateKey { set; get; }
    public string ModifyDate { set; get; }
}