namespace SD_HW2.FileWork.Import.ImportFactories;

/// <summary>
/// Провайдер фабрик импортеров
/// </summary>
public static class FileImporterFactoryProvider
{
    /// <summary>
    /// Возвращает нужную фабрику в зависимости от переданного формата
    /// </summary>
    /// <param name="format">Формат</param>
    /// <returns>Нужную фабрику</returns>
    /// <exception cref="ArgumentException">Выбрасывается, если переданный формат не поддерживается</exception>
    public static IFileImporterFactory GetFactory(string format)
    {
        return format.ToLower() switch
        {
            "csv" => new CsvFileImporterFactory(),
            "json" => new JsonFileImporterFactory(),
            _ => throw new ArgumentException($"Не поддерживаемый формат: {format}")
        };
    }
}