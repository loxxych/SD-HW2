namespace SD_HW2.FileWork.ExportService.ExportFactories;

/// <summary>
/// Интерфейс провайдера фабрик посетителей
/// </summary>
public interface IVisitorFactoryProvider
{
    IExportVisitorFactory GetFactory(string format);
}

/// <summary>
/// Провайдера фабрик посетителей-экспортеров
/// </summary>
public class ExportVisitorFactoryProvider : IVisitorFactoryProvider
{
    /// <summary>
    /// Возвращает нужную фабрику в зависимости от переданного формата
    /// </summary>
    /// <param name="format">Формат</param>
    /// <returns>Нужную фабрику</returns>
    /// <exception cref="ArgumentException">Выбрасывается, если переданный формат не поддерживается</exception>
    public IExportVisitorFactory GetFactory(string format)
    {
        return format.ToLower() switch
        {
            "csv" => new CsvExportVisitorFactory(),
            "json" => new JsonExportVisitorFactory(),
            _ => throw new ArgumentException($"Не поддерживаемый формат: {format}")
        };
    }
}