namespace SD_HW2;

public interface IBankAccountFactory
{
    public BankAccount.BankAccount createBankAccount(string name);
    public BankAccount.BankAccount createBankAccount(string name, double balance);
}

public class BankAccountFactory : IBankAccountFactory
{
    public BankAccount.BankAccount createBankAccount(string name)
    {
        return new BankAccount.BankAccount(name);
    }
    
    public BankAccount.BankAccount createBankAccount(string name, double balance)
    {
        return new BankAccount.BankAccount(name, balance);
    }
}

public interface ICategoryFactory
{
    public Category.Category createCategory(Category.Type type, string name);
}

public class CategoryFactory : ICategoryFactory
{
    public Category.Category createCategory(Category.Type type, string name)
    {
        return new Category.Category(type, name);
    }
}

public interface IOperationFactory
{
    public Operation.Operation createOperation(double amount, BankAccount.BankAccount _bankAccount,
        DateTime date, string? description, Category.Category _category);
}

public class OperationFactory : IOperationFactory
{
    public Operation.Operation createOperation(double amount, BankAccount.BankAccount _bankAccount,
        DateTime date, string? description, Category.Category _category)
    {
        return new Operation.Operation(amount, _bankAccount, date, description, _category);
    }
}