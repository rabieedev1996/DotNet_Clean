using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clean.Domain.Entities.SQL;

public class EntityBase
{
    [Column("id")]
    public long Id { set; get; }
    [Column("created_at")]
    public DateTime CreatedAt { set; get; }
    [Column("jalali_created_at")]
    [StringLength(30)]
    public string JalaliCreatedAt { set; get; }
    [Column("is_deleted")]
    public bool IsDeleted { set; get; }
    [Column("jalali_date_key")]
    public long JalaliDateKey { set; get; }
    [Column("modify_date")]
    public DateTime? ModifyDate { set; get; }
    [Column("is_enable")]
    public bool IsEnable { set; get; }

}