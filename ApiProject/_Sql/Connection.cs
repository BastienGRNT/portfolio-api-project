using Npgsql;

namespace ApiProject._Sql;

public class Connection
{
    public static NpgsqlConnection GetConnection()
    {
        string host = DotNetEnv.Env.GetString("DB_HOST");
        string port = DotNetEnv.Env.GetString("DB_PORT");
        string database = DotNetEnv.Env.GetString("DB_DATABASE");
        string user = DotNetEnv.Env.GetString("DB_USER");
        string password = DotNetEnv.Env.GetString("DB_PASSWORD");
        
        string connectionUrl = $"Host={host};Port={port};Database={database};Username={user};Password={password};";
        
        var connection = new NpgsqlConnection(connectionUrl);
        connection.Open();
        return connection;
    }
}