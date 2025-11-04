using SD_HW2.FileWork.Files;

namespace SD_HW2.FileWork.ExportService;

/// <summary>
/// Посетитель-экспортер
/// </summary>
/// <param name="file">Файл, в который производится экспорт</param>
public abstract class ExportVisitor(IFile file) : IVisitor
{
    protected StreamWriter Writer { get; } = new(file.Name);
    protected bool IsFirstElement { get; set; } = true;

    public abstract void Visit(Operation.Operation operation);
    public abstract void Visit(BankAccount.BankAccount bankAccount);
    public abstract void FinalizeExport();
}