namespace SD_HW2.Factories.OperationFactory;

/// <summary>
/// Фабрика операций
/// </summary>
public class OperationFactory : IOperationFactory
{
    public Operation.Operation CreateOperation(double amount, BankAccount.BankAccount bankAccount,
        DateTime date, string? description, Category.Category category)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException($"Недопустимое значение суммы: {amount}");
        }
        
        return new Operation.Operation(amount, bankAccount, date, description, category);
    }
}