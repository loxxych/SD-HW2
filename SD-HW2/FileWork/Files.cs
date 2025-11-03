using CsvHelper.Configuration;

namespace SD_HW2.FileWork;

public interface IFile
{
    public string Name { get; }
}

public class CsvFile : IFile
{
    public string Name { get; private set; }

    public CsvFile(string name)
    {
        Name = name;
    }
}

public class JsonFile : IFile
{
    public string Name { get; private set; }
    public JsonFile(string name)
    {
        Name = name;
    }
}

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

public sealed class BankAccountMap : ClassMap<BankAccount.BankAccount>
{
    public BankAccountMap()
    {
        Map(m => m.Id).Name("ID");
        Map(m => m.Name).Name("Name");
        Map(m => m.Balance).Name("Balance");
    }
}