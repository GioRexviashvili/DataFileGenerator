using DataFileGenerator.Interfaces;

namespace DataFileGenerator.Exporters;

public sealed class Exporter
{
    private readonly IDataSourceReader _reader;
    private readonly IFileWriter _writer;

    public Exporter(IDataSourceReader reader, IFileWriter writer)
    {
        _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        _writer = writer ?? throw new ArgumentNullException(nameof(writer));
    }

    public void Export()
    {
        var lines = _reader.Read();
        _writer.Write(lines);
    }
}