using System.Data;
using Npgsql;

namespace TestTaskMegapolis.Data;

public class SqlConnectionFactory(IConfiguration configuration)
{
    public IDbConnection CreateDbConnection()
    {
        var connectionString = configuration.GetConnectionString("TestDB");

        return new NpgsqlConnection(connectionString);
    }
}