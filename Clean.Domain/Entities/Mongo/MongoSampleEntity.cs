using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Clean.Domain.Entities.Mongo;

[BsonCollection("Sample")]
public class MongoSampleEntity : DocumentBase
{
    [StringLength(100)]
    public string SampleField { set; get; }
    public List<Sample_Item> Items { get; set; }
}
public class Sample_Item
{
   
}