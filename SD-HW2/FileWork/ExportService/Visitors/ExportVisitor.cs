namespace SD_HW2.FileWork.ExportService;

public abstract class ExportVisitor : IVisitor
{
    protected StreamWriter Writer { get; init; }
    protected bool IsFirstElement { get; set; } = true;

    protected ExportVisitor(IFile file)
    {
        Writer = new StreamWriter(file.Name);
    }
    
    public abstract void Visit(Operation.Operation operation);
    public abstract void Visit(BankAccount.BankAccount bankAccount);
    public abstract void FinalizeExport();
}