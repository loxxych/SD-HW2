namespace SD_HW2.FileWork.Files;

/// <summary>
/// Представляет Csv-файл
/// </summary>
public class CsvFile(string name) : IFile
{
    public string Name { get; private set; } = name;
}