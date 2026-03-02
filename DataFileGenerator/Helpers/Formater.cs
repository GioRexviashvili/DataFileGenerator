using System.Data.Common;

namespace DataFileGenerator.Helpers;

public static class Formater
{
    public static string GetCategoryProductRow(DbDataReader reader, string separator)
    {
        return string.Join(separator, new[]
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

    public static string GetCategoryRow(DbDataReader reader, string separator)
    {
        return string.Join(separator, new[]
        {
            reader["CategoryId"].ToString(),
            reader["CategoryName"].ToString(),
            reader["Description"].ToString(),
            (bool)reader["IsActive"] ? "1" : "0",
        });
    }
    
    public static string GetShipperOrderRow(DbDataReader reader, string separator)
    {
        return string.Join(separator, new[]
        {
            reader["CompanyName"].ToString(),
            reader["OrderID"].ToString(),
            reader["OrderDate"].ToString(),
            reader["ProductID"].ToString(),
            reader["Quantity"].ToString(),
        });
    }
}