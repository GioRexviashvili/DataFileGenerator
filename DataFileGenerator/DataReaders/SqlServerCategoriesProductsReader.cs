using DataFileGenerator.Interfaces;
using DataFileGenerator.Models;
using Microsoft.Data.SqlClient;

namespace DataFileGenerator.DataReaders;

public class SqlServerCategoriesProductsReader : IDataSourceReader<CategoryProductRow>
{
    public IEnumerable<CategoryProductRow> Read()
    {
        throw new NotImplementedException();
    }
}