using Newtonsoft.Json;
using SD_HW2.FileWork.Import.FileImporters;

namespace SD_HW2.FileWork.Import;

/// <summary>
/// Импортер из json файлов
/// </summary>
public class JsonFileImporter : FileImporter
{
    /// <summary>
    /// Читает информацию об операциях из файла
    /// </summary>
    /// <returns>Список извлеченных операций</returns>
    protected override List<Operation.Operation> ReadInfoFromFile()
    {
        var jsonContent = Reader.ReadToEnd();
        
        var settings = new JsonSerializerSettings
        {
            DateFormatString = "yyyy-MM-ddTHH:mm:ss",
            NullValueHandling = NullValueHandling.Ignore
        };

        var operations = JsonConvert.DeserializeObject<List<Operation.Operation>>(jsonContent, settings);
        return operations ?? new List<Operation.Operation>();
    }
}