using System.Data.Common;
using DataFileGenerator.DataReaders;
using DataFileGenerator.Exporters;
using DataFileGenerator.FileWriters;
using DataFileGenerator.Helpers;
using DataFileGenerator.Interfaces;
using Microsoft.Data.SqlClient;

namespace DataFileGenerator;

internal static class Program
{
    private static void Main()
    {
        const string connectionString =
            "Server=localhost,1433;Database=Northwind;User Id=sa;Password=Giorgigamer707;TrustServerCertificate=True;";

        //const string viewName = "v_categories_products";
        const string viewName = "v_categories";

        //Func<DbDataReader, string, string> getLine = Formater.GetCategoryProductRow;
        Func<DbDataReader, string, string> getLine = Formater.GetCategoryRow;
        
        string outputPath = "OutputFile/Categories.tsv";

        using var connection = new SqlConnection(connectionString);
        
        IDbReader reader = new DbReader(connection, viewName, getLine, "\t");
        IFileWriter writer = new TsvWriter(outputPath);
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