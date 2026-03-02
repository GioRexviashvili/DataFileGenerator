using DataFileGenerator.Interfaces;

namespace DataFileGenerator.Exporters;

public sealed class Exporter
{
    private readonly IDbReader _reader;
    private readonly IFileWriter _writer;

    public Exporter(IDbReader reader, IFileWriter writer)
    {
        _reader = reader ?? throw new ArgumentNullException(nameof(reader), "Reader cannot be null");
        _writer = writer ?? throw new ArgumentNullException(nameof(writer), "Writer cannot be null");
    }

    public void Export()
    {
        var lines = _reader.Read();
        _writer.Write(lines);
    }
}