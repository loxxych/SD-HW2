using SD_HW2.FileWork.ImportFactories;

namespace SD_HW2.FileWork;

public class ImportService : IImportService
{
    private FileImporterFactoryProvider _fileImporterFactoryProvider;

    public ImportService(FileImporterFactoryProvider fileImporterFactoryProvider)
    {
        _fileImporterFactoryProvider = fileImporterFactoryProvider;
    }

    public List<Operation.Operation> ImportOperations(IFile file, string format)
    {
        // Получаем от провайдера нужную фабрику в зависимости от формата
        var factory = _fileImporterFactoryProvider.GetFactory(format);
        // Создаем фабрикой импортера 
        var importer = factory.CreateImporter(file);
        // Импортируем из файла
        return importer.ImportFromFile(file);
    }
}