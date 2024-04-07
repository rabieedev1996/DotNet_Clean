using Clean.Application.Contract.SQLDB;
using Clean.Domain.Entities.SQL;
using Clean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Clean.Infrastructure.SQLRepositories;

public class SqlSampleEntityRepository : BaseRepository<SQLSampleEntity>, ISqlSampleEntityRepository
{
    public SqlSampleEntityRepository(CleanContext dbContext, DapperContext dapperContext) : base(dbContext, dapperContext)
    {
    }

}