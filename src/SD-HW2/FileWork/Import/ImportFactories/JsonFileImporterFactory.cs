using SD_HW2.FileWork.Files;
using SD_HW2.FileWork.Import.FileImporters;

namespace SD_HW2.FileWork.Import.ImportFactories;

/// <summary>
/// Фабрика json-импортеров
/// </summary>
public class JsonFileImporterFactory : IFileImporterFactory
{
    /// <summary>
    /// Создает json импортера
    /// </summary>
    public FileImporter CreateImporter(IFile file)
    {
        return new JsonFileImporter();
    }
}