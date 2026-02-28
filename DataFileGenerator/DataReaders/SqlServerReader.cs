using System.Data;
using DataFileGenerator.Interfaces;
using Microsoft.Data.SqlClient;
using System.Globalization;

namespace DataFileGenerator.DataReaders;

public class SqlServerReader : IDataSourceReader
{
    private readonly SqlConnection _connection;
    private readonly string _viewName;
    private bool _leaveConnectionOpen = true;

    public SqlServerReader(SqlConnection? connection, string viewName)
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
            using SqlCommand command = new SqlCommand($"select * from {_viewName}", _connection);
            using SqlDataReader reader = command.ExecuteReader();

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

    private string GetLine(SqlDataReader reader)
    {
        return string.Join('\t', new[]
        {
            reader.GetString(0),
            reader.GetBoolean(1) ? "1" : "0",
            reader.GetInt32(2).ToString(CultureInfo.InvariantCulture),
            reader.GetString(3),
            reader.GetDecimal(4).ToString(CultureInfo.InvariantCulture),
            reader.GetInt16(5).ToString(CultureInfo.InvariantCulture),
            reader.GetBoolean(6) ? "1" : "0"
        });
    }
}