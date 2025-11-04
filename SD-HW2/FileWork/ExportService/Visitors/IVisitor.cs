namespace SD_HW2.FileWork.ExportService;

public interface IVisitor
{
    void Visit(Operation.Operation operation);
    void Visit(BankAccount.BankAccount bankAccount);
}