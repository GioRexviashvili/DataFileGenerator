namespace DataFileGenerator.Interfaces;

public interface IFileWriter
{
    public void Write(IEnumerable<string> data);
}