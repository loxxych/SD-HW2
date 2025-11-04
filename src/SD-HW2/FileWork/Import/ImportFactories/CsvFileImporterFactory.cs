using SD_HW2.FileWork.Files;
using SD_HW2.FileWork.Import.FileImporters;

namespace SD_HW2.FileWork.Import.ImportFactories;

/// <summary>
/// Фабрика csv-импортеров
/// </summary>
public class CsvFileImporterFactory : IFileImporterFactory
{
    /// <summary>
    /// Создает csv импортера
    /// </summary>
    public FileImporter CreateImporter(IFile file)
    {
        return new CsvFileImporter();
    }
}