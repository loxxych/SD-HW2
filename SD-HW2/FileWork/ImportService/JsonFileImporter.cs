using Newtonsoft.Json;

namespace SD_HW2.FileWork;


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