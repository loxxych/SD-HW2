using System.Globalization;
using CsvHelper;
using SD_HW2.FileWork.Files;
using SD_HW2.FileWork.Files.Maps;

namespace SD_HW2.FileWork.ExportService;

/// <summary>
/// Класс-посетитель для экспорта данных в csv-файл
/// </summary>
public class CsvExportVisitor : ExportVisitor
{
    private readonly CsvWriter _csvWriter;

    public CsvExportVisitor(IFile file) : base(file)
    {
        _csvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture);
        _csvWriter.Context.RegisterClassMap<OperationMap>();
        _csvWriter.Context.RegisterClassMap<BankAccountMap>();
    }

    /// <summary>
    /// Посещает операцию, экспортируя данные о ней
    /// </summary>
    /// <param name="operation">Экспортируемая операция</param>
    public override void Visit(Operation.Operation operation)
    {
        if (IsFirstElement)
        {
            _csvWriter.WriteHeader<Operation.Operation>();
            _csvWriter.NextRecord();
            IsFirstElement = false;
        }
        
        _csvWriter.WriteRecord(operation);
        _csvWriter.NextRecord();
        _csvWriter.Flush();
    }

    /// <summary>
    /// Посещает счет, экспортируя данные о нем
    /// </summary>
    /// <param name="bankAccount">Экспортируемый счет</param>
    public override void Visit(BankAccount.BankAccount bankAccount)
    {
        if (IsFirstElement)
        {
            _csvWriter.WriteHeader<BankAccount.BankAccount>();
            _csvWriter.NextRecord();
            IsFirstElement = false;
        }
        
        _csvWriter.WriteRecord(bankAccount);
        _csvWriter.NextRecord();
    }
    
    /// <summary>
    /// Заканчивает экспорт
    /// </summary>
    public override void FinalizeExport()
    {
        _csvWriter.Flush();
    }
}