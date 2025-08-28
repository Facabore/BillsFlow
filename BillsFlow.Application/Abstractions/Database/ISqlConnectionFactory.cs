namespace BillsFlow.Application.Abstractions.Database;

using System.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}