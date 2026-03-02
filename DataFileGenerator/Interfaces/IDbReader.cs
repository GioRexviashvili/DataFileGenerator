namespace DataFileGenerator.Interfaces;

public interface IDbReader
{
    public IEnumerable<string> Read();
}