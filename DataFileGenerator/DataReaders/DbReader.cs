using System.Data;
using System.Data.Common;
using DataFileGenerator.Interfaces;

namespace DataFileGenerator.DataReaders;

public class DbReader : IDbReader
{
    private readonly DbConnection _connection;
    private readonly string _viewName;
    private readonly Func<DbDataReader, string, string> _getLine;
    private readonly string _separator;
    private bool _leaveConnectionOpen = true;

    public DbReader(DbConnection? connection, string viewName, Func<DbDataReader, string, string> getLine, string separator)
    {
        if (string.IsNullOrWhiteSpace(viewName))
            throw new ArgumentException("View name cannot be null or whitespace", nameof(viewName));
        _connection = connection ?? throw new ArgumentNullException(nameof(connection), "Connection cannot be null");
        _viewName = viewName;
        _getLine = getLine;
        _separator = separator;
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
                yield return _getLine(reader, _separator);
            }
        }
        finally
        {
            if (!_leaveConnectionOpen)
                _connection.Close();
        }
    }


}