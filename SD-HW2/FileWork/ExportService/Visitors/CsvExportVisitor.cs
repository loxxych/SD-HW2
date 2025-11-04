using System.Globalization;
using CsvHelper;

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
    
    public override void FinalizeExport()
    {
        _csvWriter.Flush();
    }
}