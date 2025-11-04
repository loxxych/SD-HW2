namespace SD_HW2.FileWork.ImportFactories;

public class FileImporterFactoryProvider
{
    public IFileImporterFactory GetFactory(string format)
    {
        return format.ToLower() switch
        {
            "csv" => new CsvFileImporterFactory(),
            "json" => new JsonFileImporterFactory(),
            _ => throw new ArgumentException($"Не поддерживаемый формат: {format}")
        };
    }
}