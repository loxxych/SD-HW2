namespace SD_HW2.Factories.OperationFactory;

/// <summary>
/// Интерфейс фабрики операций
/// </summary>
public interface IOperationFactory
{
    public Operation.Operation CreateOperation(double amount, BankAccount.BankAccount _bankAccount,
        DateTime date, string? description, Category.Category _category);
}