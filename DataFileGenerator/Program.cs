using System.Data.Common;
using DataFileGenerator.DataReaders;
using DataFileGenerator.Exporters;
using DataFileGenerator.FileWriters;
using DataFileGenerator.Helpers;
using DataFileGenerator.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace DataFileGenerator;

internal static class Program
{
    private static void Main()
    {
        try
        {
            var configuration = BuildConfiguration();

            var connectionString = configuration["Database:ConnectionString"]
                                   ?? throw new InvalidOperationException(
                                       "Database:ConnectionString is missing in configuration.");

            var outputPath = configuration["Export:OutputDirectory"]
                             ?? throw new InvalidOperationException(
                                 "Export:OutputDirectory is missing in configuration.");

            const string viewName = "v_shippers_orders";
            //const string viewName = "v_categories_products";
            //const string viewName = "v_categories";

            Func<DbDataReader, string, string> getLine = Formater.GetShipperOrderRow;
            //Func<DbDataReader, string, string> getLine = Formater.GetCategoryProductRow;
            //Func<DbDataReader, string, string> getLine = Formater.GetCategoryRow;

            using var connection = new SqlConnection(connectionString);

            IDbReader reader = new DbReader(connection, viewName, getLine, ",");
            using IFileWriter writer = new TsvWriter(outputPath);
            var exporter = new Exporter(reader, writer);

            exporter.Export();
            Console.WriteLine($"Export completed: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Export failed:");
            Console.WriteLine(ex);
        }
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();
        return configuration;
    }
}