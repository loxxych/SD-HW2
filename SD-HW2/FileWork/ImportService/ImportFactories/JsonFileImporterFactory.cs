namespace SD_HW2.FileWork.ImportFactories;

public class JsonFileImporterFactory : IFileImporterFactory
{
    public FileImporter CreateImporter(IFile file)
    {
        return new JsonFileImporter();
    }
}