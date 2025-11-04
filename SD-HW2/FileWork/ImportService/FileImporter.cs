namespace SD_HW2.FileWork;

/// <summary>
/// Класс для экспорта данных в файл
/// </summary>
public abstract class FileImporter
{
    protected StreamReader _reader { get; private set; }
    
    /// <summary>
    /// Шаблонный метод для экспорта файла
    /// </summary>
    /// <param name="file"></param>
    public List<Operation.Operation> ImportFromFile(IFile file)
    {
        GetReader(file);
        return ReadInfoFromFile();
    }

    private void GetReader(IFile file)
    {
        _reader = new StreamReader(file.Name);
    }
    
    protected abstract List<Operation.Operation> ReadInfoFromFile();
}