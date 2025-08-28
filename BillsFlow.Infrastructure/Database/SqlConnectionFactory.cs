namespace BillsFlow.Infrastructure.Database;

#region Usings
using System.Data;
using BillsFlow.Application.Abstractions.Database;
using Npgsql;
#endregion

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        return connection;
    }
}