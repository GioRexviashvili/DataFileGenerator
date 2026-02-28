namespace DataFileGenerator.Interfaces;

public interface IDataSourceReader
{
    public IEnumerable<string> Read();
}