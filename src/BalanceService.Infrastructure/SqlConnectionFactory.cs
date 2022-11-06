using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace BalanceService.Infrastructure;

public class MySqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;
    private IDbConnection _connection;

    public MySqlConnectionFactory(string connectionString)
    {
        this._connectionString = connectionString;
    }

    public IDbConnection GetOpenConnection()
    {
        if (this._connection == null || this._connection.State != ConnectionState.Open)
        {
            this._connection = new MySqlConnection(_connectionString);
            this._connection.Open();
        }

        return this._connection;
    }

    public IDbConnection CreateNewConnection()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();

        return connection;
    }

    public string GetConnectionString()
    {
        return _connectionString;
    }

    public void Dispose()
    {
        if (this._connection != null && this._connection.State == ConnectionState.Open)
        {
            this._connection.Dispose();
        }
    }
}
