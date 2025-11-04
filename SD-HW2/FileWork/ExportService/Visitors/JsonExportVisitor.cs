using Newtonsoft.Json;

namespace SD_HW2.FileWork.ExportService;

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
            DateFormatString = "yyyy-MM-ddTHH:mm:ss",
        };
    }
    
    public override void Visit(Operation.Operation operation)
    {
        var operationDto = new
        {
            Type = "Operation",
            operation.Id,
            operation.Amount,
            operation.Date,
            operation.Description,
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
            bankAccount.Id,
            bankAccount.Name,
            bankAccount.Balance
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
        Writer.Dispose();
    }
}