namespace SD_HW2.FileWork.Files;

/// <summary>
/// Представляет Json-файл
/// </summary>
public class JsonFile(string name) : IFile
{
    public string Name { get; } = name;
}