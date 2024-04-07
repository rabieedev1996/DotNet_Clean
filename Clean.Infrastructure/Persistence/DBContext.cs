using System.Text.RegularExpressions;
using Clean.Domain.Entities.Mongo;
using Clean.Domain.Entities.SQL;
using Clean.Infrastructure.SQLRepositories;
using Clean.Utility;
using Microsoft.EntityFrameworkCore;


namespace Clean.Infrastructure.Persistence;

public class CleanContext:DbContext
{
    public CleanContext(DbContextOptions<CleanContext> options):base(options) {  }
    public DbSet<SQLSampleEntity> Sample { get; set; }
    
    

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var now = DateTime.Now;
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.IsDeleted = false;
                    entry.Entity.IsEnable = true;
                    entry.Entity.CreatedAt = now;
                    entry.Entity.JalaliCreatedAt = now.ToFa("yyyy/MM/dd");
                    entry.Entity.JalaliDateKey = int.Parse(now.ToFa("yyyyMMdd"));
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifyDate = now;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}