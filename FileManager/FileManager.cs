namespace FileManager;

public class FileManager
{
    private readonly Dictionary<string, Func<string, List<Record>>> formatReaders = new();
    private readonly Dictionary<string, Action<string, List<Record>>> formatWriters = new();
    private readonly Dictionary<string, Action<string, Record>> formatAddRecord = new();
    private readonly Dictionary<string, Action<string, string>> formatDeleteRecord = new();

    public void RegisterFormat(string formatName, Func<string, List<Record>> readFunc, Action<string, List<Record>> writeFunc, Action<string, Record> addRecordFunc, Action<string, string> delFunc)
    {
        if (!formatReaders.ContainsKey(formatName))
        {
            formatReaders[formatName] = readFunc;
            formatWriters[formatName] = writeFunc;
            formatAddRecord[formatName] = addRecordFunc;
            formatDeleteRecord[formatName] = delFunc;
        }
        else
            throw new Exception($"Format '{formatName}' is already registered.");
    }

    public void RegisterXmlFormat() => RegisterFormat("xml", FileFormats.ReadXmlFile, FileFormats.WriteToXmlFile, FileFormats.AddRecordXml, FileFormats.RemoveRecordXml);

    public void RegisterJsonFormat() => RegisterFormat("json", FileFormats.ReadJsonFile, FileFormats.WriteToJsonFile, FileFormats.AddToJsonFile, FileFormats.RemoveRecordJson);

    public List<Record> ReadFile(string filePath, string formatName)
    {
        if (formatReaders.TryGetValue(formatName, out var reader))
            return reader(filePath);
        else
            throw new Exception($"Unsupported format: {formatName}");
    }

    public void WriteFile(string filePath, string formatName, List<Record> data)
    {
        if (formatWriters.TryGetValue(formatName, out var writer))
            writer(filePath, data);
        else
            throw new Exception($"Unsupported format: {formatName}");
    }

    public void AddRecordToFile(string filePath, string formatName, Record record)
    {
        if (formatAddRecord.TryGetValue(formatName, out var addRecord))
            addRecord(filePath, record);
        else
            throw new Exception($"Unsupported format for adding records: {formatName}");
    }

    public void DeleteRecord(string filePath, string formatName, string brandNameToRemove)
    {
        if (formatDeleteRecord.TryGetValue(formatName, out var deleteRecord))
            deleteRecord(filePath, brandNameToRemove);
        else
            throw new Exception($"Unsupported format for deleting records: {formatName}");
    }
    
    public void ConvertFile(string sourceFilePath, string sourceFormat, string destinationFilePath, string destinationFormat)
    {
        if (!formatReaders.ContainsKey(sourceFormat))
            throw new Exception($"Unsupported source format: {sourceFormat}");

        if (!formatWriters.ContainsKey(destinationFormat))
            throw new Exception($"Unsupported destination format: {destinationFormat}");

        List<Record> data = ReadFile(sourceFilePath, sourceFormat);

        WriteFile(destinationFilePath, destinationFormat, data);
    }
}