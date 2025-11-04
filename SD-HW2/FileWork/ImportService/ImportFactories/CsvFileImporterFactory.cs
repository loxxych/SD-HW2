namespace SD_HW2.FileWork.ImportFactories;

public class CsvFileImporterFactory : IFileImporterFactory
{
    public FileImporter CreateImporter(IFile file)
    {
        return new CsvFileImporter();
    }
}