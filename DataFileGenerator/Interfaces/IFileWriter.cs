namespace DataFileGenerator.Interfaces;

public interface IFileWriter : IDisposable
{
    public void Write(IEnumerable<string> data);
}