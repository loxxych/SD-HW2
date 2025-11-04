namespace SD_HW2.Operation;

/// <summary>
/// Фасад для изменения полей операции
/// </summary>
public static class OperationManager
{
    public static void ChangeAmount(int id, double amount)
    {
        var op = OperationRepository.OperationById(id);
        op.Amount = amount;
    }

    public static void ChangeAccount(int id, BankAccount.BankAccount bankAccount)
    {
        var op = OperationRepository.OperationById(id);
        op.BankAccount = bankAccount;
    }

    public static void ChangeCategory(int id, Category.Category category)
    {
        var op = OperationRepository.OperationById(id);
        op.Category = category;
    }
    
    public static void ChangeDescription(int id, string description)
    {
        var op = OperationRepository.OperationById(id);
        op.Description = description;
    }
}