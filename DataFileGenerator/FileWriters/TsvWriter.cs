using DataFileGenerator.Interfaces;

namespace DataFileGenerator.FileWriters;

public class TsvWriter : IFileWriter
{
    private readonly Stream _stream;
    private bool _leaveStreamOpen = true;

    public TsvWriter(FileInfo fileInfo)
    {
        ArgumentNullException.ThrowIfNull(fileInfo);
        if (!fileInfo.Exists)
            throw new FileNotFoundException($"File not found: {fileInfo.FullName}");

        _stream = fileInfo.OpenWrite();
    }

    public TsvWriter(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or whitespace", nameof(filePath));

        var dir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(dir))
            Directory.CreateDirectory(dir);

        _stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read);
    }

    public void Write(IEnumerable<string> data)
    {
        using StreamWriter writer = new StreamWriter(_stream);
        foreach (var item in data)
        {
            writer.WriteLine(item);
        }
    }
}