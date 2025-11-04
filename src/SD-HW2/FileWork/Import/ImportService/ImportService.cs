using SD_HW2.FileWork.Files;
using SD_HW2.FileWork.Import.ImportFactories;

namespace SD_HW2.FileWork.ImportService;

/// <summary>
/// Класс-фасад для импорта данных из файлов
/// </summary>
public class ImportService : IImportService
{
    /// <summary>
    /// Импортирует операции из файла
    /// </summary>
    /// <param name="file">Файл, из которого происходит импорт</param>
    /// <param name="format">Формат файла</param>
    /// <returns>Список извлеченных операций</returns>
    public List<Operation.Operation> ImportOperations(IFile file, string format)
    {
        // Получаем от провайдера нужную фабрику в зависимости от формата
        var factory = FileImporterFactoryProvider.GetFactory(format);
        // Создаем фабрикой импортера 
        var importer = factory.CreateImporter(file);
        // Импортируем из файла
        return importer.ImportFromFile(file);
    }
}