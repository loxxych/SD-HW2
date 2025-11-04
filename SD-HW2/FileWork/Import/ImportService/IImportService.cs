using SD_HW2.FileWork.Files;

namespace SD_HW2.FileWork;

/// <summary>
/// Интерфейс сервиса для импорта операций
/// </summary>
public interface IImportService
{
    public List<Operation.Operation> ImportOperations(IFile file, string format);
}