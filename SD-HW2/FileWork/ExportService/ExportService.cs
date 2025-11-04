using SD_HW2.FileWork.ExportService.ExportFactories;

namespace SD_HW2.FileWork.ExportService;

public class ExportService : IExportService
{
    private IVisitorFactoryProvider _exportVisitorFactoryProvider;

    public ExportService(IVisitorFactoryProvider exportVisitorFactoryProvider)
    {
        _exportVisitorFactoryProvider = exportVisitorFactoryProvider;
    }
    
    public void ExportOperations(List<Operation.Operation> operations, IFile file, string format)
    {
        var factory = _exportVisitorFactoryProvider.GetFactory(format);
        var exportVisitor = factory.CreateVisitor(file);
        
        foreach (var operation in operations)
        {
            operation.Accept(exportVisitor);
        }

        exportVisitor.FinalizeExport();
    }
    
    public void ExportBankAccounts(List<BankAccount.BankAccount> accounts, IFile file, string format)
    {
        var factory = _exportVisitorFactoryProvider.GetFactory(format);
        var exportVisitor = factory.CreateVisitor(file);
        
        foreach (var acc in accounts)
        {
            acc.Accept(exportVisitor);
        }

        exportVisitor.FinalizeExport();
    }
}