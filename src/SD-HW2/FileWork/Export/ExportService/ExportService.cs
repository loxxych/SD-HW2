using SD_HW2.FileWork.ExportService.ExportFactories;
using SD_HW2.FileWork.Files;

namespace SD_HW2.FileWork.ExportService;

/// <summary>
/// Фасад для экпорта данных в файлы разных форматов
/// </summary>
public class ExportService(IVisitorFactoryProvider exportVisitorFactoryProvider) : IExportService
{
    /// <summary>
    /// Экспортирует операции в файл заданного формата
    /// </summary>
    /// <param name="operations">Экспортируемые операции</param>
    /// <param name="file">Файл для экспорта</param>
    /// <param name="format">Формат файла</param>
    public void ExportOperations(List<Operation.Operation> operations, IFile file, string format)
    {
        // Получаем от провайдера фабрику нужного формата
        var factory = exportVisitorFactoryProvider.GetFactory(format);
        // Получаем от фабрики посетителя
        var exportVisitor = factory.CreateVisitor(file);
        
        // Обходим и посещаем операции
        foreach (var operation in operations)
        {
            operation.Accept(exportVisitor);
        }

        // Завершаем экспорт
        exportVisitor.FinalizeExport();
    }
    
    /// <summary>
    /// Экспортирует счета в файл заданного формата
    /// </summary>
    /// <param name="accounts">Экспортируемые счета</param>
    /// <param name="file">Файл для экспорта</param>
    /// <param name="format">Формат файла</param>
    public void ExportBankAccounts(List<BankAccount.BankAccount> accounts, IFile file, string format)
    {
        // Получаем от провайдера фабрику нужного формата
        var factory = exportVisitorFactoryProvider.GetFactory(format);
        // Получаем от фабрики посетителя
        var exportVisitor = factory.CreateVisitor(file);
        
        // Обходим и посещаем счета
        foreach (var acc in accounts)
        {
            acc.Accept(exportVisitor);
        }

        // Завершаем экспорт
        exportVisitor.FinalizeExport();
    }
}