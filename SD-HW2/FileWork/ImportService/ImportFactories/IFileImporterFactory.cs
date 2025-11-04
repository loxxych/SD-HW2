namespace SD_HW2.FileWork.ImportFactories;

public interface IFileImporterFactory
{
    public FileImporter CreateImporter(IFile file);
}