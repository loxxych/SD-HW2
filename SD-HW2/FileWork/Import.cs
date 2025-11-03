using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace SD_HW2.FileWork;

/// <summary>
/// Класс для экспорта данных в файл
/// </summary>
public abstract class FileImporter
{
    protected StreamReader _reader { get; private set; }
    
    /// <summary>
    /// Шаблонный метод для экспорта файла
    /// </summary>
    /// <param name="file"></param>
    public List<Operation.Operation> ImportFromFile(IFile file)
    {
        GetReader(file);
        return ReadInfoFromFile();
    }

    private void GetReader(IFile file)
    {
        _reader = new StreamReader(file.Name);
    }
    
    protected abstract List<Operation.Operation> ReadInfoFromFile();
}

public class CsvFileImporter : FileImporter
{
    protected override List<Operation.Operation> ReadInfoFromFile()
    {
        using (var csv = new CsvReader(_reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<OperationMap>();
            var operations = csv.GetRecords<Operation.Operation>().ToList();
            return operations;
        }
    }
}

public class JsonFileImporter : FileImporter
{
    protected override List<Operation.Operation> ReadInfoFromFile()
    {
        var jsonContent = _reader.ReadToEnd();
        
        var settings = new JsonSerializerSettings
        {
            DateFormatString = "yyyy-MM-ddTHH:mm:ss",
            NullValueHandling = NullValueHandling.Ignore
        };

        var operations = JsonConvert.DeserializeObject<List<Operation.Operation>>(jsonContent, settings);
        return operations ?? new List<Operation.Operation>();
    }
}