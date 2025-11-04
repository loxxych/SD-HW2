using SD_HW2.FileWork.Files;

namespace SD_HW2.FileWork.Import.FileImporters;

/// <summary>
/// Класс для экспорта данных в файл
/// </summary>
public abstract class FileImporter
{
    protected StreamReader Reader { get; private set; }
    
    /// <summary>
    /// Шаблонный метод для импорта данных в файл
    /// </summary>
    /// <param name="file">Файл, из которого импортируются данные</param>
    public List<Operation.Operation> ImportFromFile(IFile file)
    {
        GetReader(file);
        return ReadInfoFromFile();
    }

    /// <summary>
    /// Получение читателя файла
    /// </summary>
    /// <param name="file">Файл для чтения</param>
    private void GetReader(IFile file)
    {
        Reader = new StreamReader(file.Name);
    }
    
    /// <summary>
    /// Читает информацию об операциях из файла
    /// </summary>
    /// <returns>Список извлеченных операций</returns>
    protected abstract List<Operation.Operation> ReadInfoFromFile();
}