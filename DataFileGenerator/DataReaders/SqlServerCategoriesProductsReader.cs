using DataFileGenerator.Interfaces;
using DataFileGenerator.Models;
using Microsoft.Data.SqlClient;

namespace DataFileGenerator.DataReaders;

public class SqlServerCategoriesProductsReader : IDataSourceReader<CategoryProductRow>
{
    private readonly SqlConnection _connection;
    private readonly string _viewName;

    public SqlServerCategoriesProductsReader(SqlConnection? connection, string viewName)
    {
        if (string.IsNullOrWhiteSpace(viewName))
            throw new ArgumentException("View name cannot be null or whitespace", nameof(viewName));
        _connection = connection ?? throw new ArgumentNullException(nameof(connection), "Connection cannot be null");
        _viewName = viewName;
    }

    public IEnumerable<CategoryProductRow> Read()
    {
        throw new NotImplementedException();
    }
}