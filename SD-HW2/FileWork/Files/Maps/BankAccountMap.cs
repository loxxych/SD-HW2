using CsvHelper.Configuration;

namespace SD_HW2.FileWork.Files.Maps;

/// <summary>
/// Вспомогательный класс для форматирования счетов при экспорте и импорте
/// </summary>
public sealed class BankAccountMap : ClassMap<BankAccount.BankAccount>
{
    public BankAccountMap()
    {
        Map(m => m.Id).Name("ID");
        Map(m => m.Name).Name("Name");
        Map(m => m.Balance).Name("Balance");
    }
}