using DataFileGenerator.DataReaders;
using DataFileGenerator.Exporters;
using DataFileGenerator.FileWriters;
using Microsoft.Data.SqlClient;

namespace DataFileGenerator;

internal static class Program
{
    private static void Main()
    {
        const string connectionString =
            "Server=localhost,1433;Database=Northwind;User Id=sa;Password=Giorgigamer707;TrustServerCertificate=True;";

        const string viewName = "v_categories_products";

        string outputPath = "OutputFile/Products.tsv";

        using var connection = new SqlConnection(connectionString);

        var reader = new SqlServerReader(connection, viewName);
        var writer = new TsvWriter(outputPath);
        var exporter = new Exporter(reader, writer);

        try
        {
            exporter.Export();
            Console.WriteLine($"Export completed: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Export failed:");
            Console.WriteLine(ex);
        }
    }
}