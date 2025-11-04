namespace SD_HW2.Operation.Commands;

/// <summary>
/// Команда для добавления новой операции
/// </summary>
public class AddOperationCommand(
    double amount,
    BankAccount.BankAccount bankAccount,
    string? description,
    Category.Category category)
    : ICommand
{
    /// <summary>
    /// Добавляет новую операцию
    /// </summary>
    public void Execute()
    {
        OperationRepository.AddOperation(amount, bankAccount, description, category);
    }
}