using Newtonsoft.Json;
using SD_HW2.FileWork.Files;

namespace SD_HW2.FileWork.ExportService.Visitors;

/// <summary>
/// Класс-посетитель для экспорта данных в json-файл
/// </summary>
public class JsonExportVisitor(IFile file) : ExportVisitor(file)
{
    private readonly List<object> _exportData = [];
    private readonly JsonSerializerSettings _jsonSettings = new()
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore,
        DateFormatString = "yyyy-MM-ddTHH:mm:ss",
    };

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
            Category = operation.Category.Name,
        };

        _exportData.Add(operationDto);
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

        _exportData.Add(bankAccountDto);
    }
    
    public override void FinalizeExport()
    {
        try
        {
            var json = JsonConvert.SerializeObject(_exportData, _jsonSettings);
            Writer.Write(json);
            Writer.Flush();
        }
        finally
        {
            Writer?.Dispose();
        }
    }
}