using DataFileGenerator.Interfaces;

namespace DataFileGenerator.FileWriters;

public class TsvWriter<T> : IFileWriter<T>
{
    public void Write(string filePath, IEnumerable<T> data)
    {
        throw new NotImplementedException();
    }
}