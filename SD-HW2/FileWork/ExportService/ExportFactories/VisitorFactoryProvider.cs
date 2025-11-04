namespace SD_HW2.FileWork.ExportService.ExportFactories;

public interface IVisitorFactoryProvider
{
    ExportVisitorFactory GetFactory(string format);
}

public class ExportVisitorFactoryProvider : IVisitorFactoryProvider
{
    public ExportVisitorFactory GetFactory(string format)
    {
        return format.ToLower() switch
        {
            "csv" => new CsvExportVisitorFactory(),
            "json" => new JsonExportVisitorFactory(),
            _ => throw new ArgumentException($"Не поддерживаемый формат: {format}")
        };
    }
}