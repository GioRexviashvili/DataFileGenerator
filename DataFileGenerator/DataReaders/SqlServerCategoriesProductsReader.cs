using System.Data;
using DataFileGenerator.Interfaces;
using DataFileGenerator.Models;
using Microsoft.Data.SqlClient;

namespace DataFileGenerator.DataReaders;

public class SqlServerCategoriesProductsReader : IDataSourceReader<CategoryProductRow>
{
    private readonly SqlConnection _connection;
    private readonly string _viewName;
    private bool _leaveConnectionOpen = true;

    public SqlServerCategoriesProductsReader(SqlConnection? connection, string viewName)
    {
        if (string.IsNullOrWhiteSpace(viewName))
            throw new ArgumentException("View name cannot be null or whitespace", nameof(viewName));
        _connection = connection ?? throw new ArgumentNullException(nameof(connection), "Connection cannot be null");
        _viewName = viewName;
    }

    public IEnumerable<CategoryProductRow> Read()
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
                yield return new CategoryProductRow
                {
                    CategoryName = reader.GetString(0),
                    CategoryIsActive = reader.GetBoolean(1),
                    ProductCode = reader.GetInt32(2),
                    ProductName = reader.GetString(3),
                    Price = reader.GetDecimal(4),
                    Quantity = reader.GetInt16(5),
                    ProductIsActive = reader.GetBoolean(6)
                };
            }
        }
        finally
        {
            if (!_leaveConnectionOpen)
                _connection.Close();
        }
    }
}