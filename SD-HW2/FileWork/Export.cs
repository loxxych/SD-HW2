using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace SD_HW2.FileWork;

public interface IVisitor
{
    void Visit(Operation.Operation operation);
    void Visit(BankAccount.BankAccount bankAccount);
}

public interface IComponent
{
    void Accept(IVisitor visitor);
}

public abstract class ExportVisitor : IVisitor
{
    protected StreamWriter Writer { get; private set; }
    protected bool IsFirstElement { get; set; } = true;

    protected ExportVisitor(IFile file)
    {
        Writer = new StreamWriter(file.Name);
    }
    
    public abstract void Visit(Operation.Operation operation);
    public abstract void Visit(BankAccount.BankAccount bankAccount);
    public abstract void FinalizeExport();
}

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

/// <summary>
/// Класс-посетитель для экспорта данных в json-файл
/// </summary>
public class JsonExportVisitor : ExportVisitor
{
    private readonly List<object> _exportData;
    private readonly JsonSerializerSettings _jsonSettings;
    
    public JsonExportVisitor(IFile file) : base(file)
    {
        _exportData = new List<object>();
        _jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatString = "yyyy-MM-ddTHH:mm:ss"
        };
    }
    
    public override void Visit(Operation.Operation operation)
    {
        var operationDto = new
        {
            Type = "Operation",
            Id = operation.Id,
            Amount = operation.Amount,
            Date = operation.Date,
            Description = operation.Description,
            BankAccount = operation.BankAccount.Name,
            Category = operation.Category.Name
        };

        WriteJsonElement(operationDto);
    }

    public override void Visit(BankAccount.BankAccount bankAccount)
    {
        var bankAccountDto = new
        {
            Type = "BankAccount",
            Id = bankAccount.Id,
            Name = bankAccount.Name,
            Balance = bankAccount.Balance
        };

        WriteJsonElement(bankAccountDto);
    }
    
    private void WriteJsonElement(object element)
    {
        string json = JsonConvert.SerializeObject(element, _jsonSettings);

        if (IsFirstElement)
        {
            Writer.WriteLine("{");
            Writer.WriteLine("  \"exportData\": [");
        }
        
        if (!IsFirstElement)
        {
            Writer.WriteLine(",");
        }
        IsFirstElement = false;
        
        Writer.Write("    " + json);
    }
    
    public override void FinalizeExport()
    {
        Writer.WriteLine();
        Writer.WriteLine("  ]");
        Writer.WriteLine("}");
        Writer.Flush();
    }
}