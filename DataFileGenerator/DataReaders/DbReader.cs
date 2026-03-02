using System.Data;
using System.Data.Common;
using DataFileGenerator.Interfaces;
using System.Globalization;

namespace DataFileGenerator.DataReaders;

public class DbReader : IDbReader
{
    private readonly DbConnection _connection;
    private readonly string _viewName;
    private bool _leaveConnectionOpen = true;

    public DbReader(DbConnection? connection, string viewName)
    {
        if (string.IsNullOrWhiteSpace(viewName))
            throw new ArgumentException("View name cannot be null or whitespace", nameof(viewName));
        _connection = connection ?? throw new ArgumentNullException(nameof(connection), "Connection cannot be null");
        _viewName = viewName;
    }

    public IEnumerable<string> Read()
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
            _leaveConnectionOpen = false;
        }

        try
        {
            using DbCommand command = _connection.CreateCommand();
            command.CommandText = $"select * from {_viewName};";
            using DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return GetLine(reader);
            }
        }
        finally
        {
            if (!_leaveConnectionOpen)
                _connection.Close();
        }
    }

    private string GetLine(DbDataReader reader)
    {
        return string.Join('\t', new[]
        {
            reader["CategoryName"].ToString(),
            (bool)reader["CategoryIsActive"] ? "1" : "0",
            reader["ProductCode"].ToString(),
            reader["ProductName"].ToString(),
            reader["Price"].ToString(),
            reader["Quantity"].ToString(),
            (bool)reader["ProductIsActive"] ? "1" : "0"
        });
    }
}