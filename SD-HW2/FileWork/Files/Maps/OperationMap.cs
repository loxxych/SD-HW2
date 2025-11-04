using CsvHelper.Configuration;

namespace SD_HW2.FileWork.Files.Maps;

/// <summary>
/// Вспомогательный класс для форматирования операций при экспорте и импорте
/// </summary>
public sealed class OperationMap : ClassMap<Operation.Operation>
{
    public OperationMap()
    {
        Map(m => m.Id).Name("ID");
        Map(m => m.Amount).Name("Amount");
        Map(m => m.BankAccount.Name).Name("BankAccount");
        Map(m => m.Date).Name("Date");
        Map(m => m.Description).Name("Description");
        Map(m => m.Category.Type).Name("Type");
        Map(m => m.Category.Name).Name("CategoryName");
    }
}