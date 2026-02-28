namespace DataFileGenerator.Interfaces;

public interface IDataSourceReader<T>
{
    public IEnumerable<T> Read();
}