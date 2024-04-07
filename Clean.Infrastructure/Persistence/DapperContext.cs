using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Clean.Infrastructure.Persistence;

public class DapperContext
{
    //// PostgreSql Connection
    //public NpgsqlConnection _sqlConnection;
    //public DapperContext(IConfiguration configuration)
    //{
    //    _sqlConnection = new NpgsqlConnection(configuration.GetConnectionString("IPLAPostgresDB"));
    //}


    //// Sql Server Connection
    public SqlConnection _sqlConnection;
    public DapperContext(IConfiguration configuration)
    {
        _sqlConnection = new SqlConnection(configuration.GetConnectionString("IPLAPostgresDB"));
    }
}