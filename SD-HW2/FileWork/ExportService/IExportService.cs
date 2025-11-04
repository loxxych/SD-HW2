namespace SD_HW2.FileWork.ExportService;

public interface IExportService
{
    public void ExportOperations(List<Operation.Operation> operations, IFile file, string format);
    public void ExportBankAccounts(List<BankAccount.BankAccount> accounts, IFile file, string format);
}