namespace DataFileGenerator.Interfaces;

public interface IFileWriter<T>
{
    public void Write(string filePath, IEnumerable<T> data);
}