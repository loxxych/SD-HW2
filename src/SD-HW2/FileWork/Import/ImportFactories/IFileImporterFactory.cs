using SD_HW2.FileWork.Files;
using SD_HW2.FileWork.Import.FileImporters;

namespace SD_HW2.FileWork.Import.ImportFactories;

/// <summary>
/// Интерфейс фабрики импортера 
/// </summary>
public interface IFileImporterFactory
{
    public FileImporter CreateImporter(IFile file);
}