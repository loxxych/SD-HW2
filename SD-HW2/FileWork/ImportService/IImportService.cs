namespace SD_HW2.FileWork;

public interface IImportService
{
    public List<Operation.Operation> ImportOperations(IFile file, string format);
}