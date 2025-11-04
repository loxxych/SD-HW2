using SD_HW2.BankAccount;
using Type = SD_HW2.Category.Type;

namespace SD_HW2.Operation;

/// <summary>
/// Фасад для изменения полей операции
/// </summary>
public static class OperationManager
{
    /// <summary>
    /// Изменение суммы операции
    /// </summary>
    /// <param name="id">ID операции</param>
    /// <param name="amount">Новая сумма</param>
    public static void ChangeAmount(int id, double amount)
    {
        var operation = OperationRepository.OperationById(id);
        
        if (operation.Category.Type == Type.Withdrawal)
        {
            BankAccountManager.Deposit(operation.BankAccount, operation.Amount);
            BankAccountManager.Withdraw(operation.BankAccount, amount);
        }
        else
        {
            BankAccountManager.Withdraw(operation.BankAccount, operation.Amount);
            BankAccountManager.Deposit(operation.BankAccount, amount);
        }
        
        operation.Amount = amount;
    }

    /// <summary>
    /// Изменение счета операции
    /// </summary>
    /// <param name="id">ID операции</param>
    /// <param name="bankAccount">Новый счет</param>
    public static void ChangeAccount(int id, BankAccount.BankAccount bankAccount)
    {
        var operation = OperationRepository.OperationById(id);
        
        if (operation.Category.Type == Type.Withdrawal)
        {
            BankAccountManager.Deposit(operation.BankAccount, operation.Amount);
            BankAccountManager.Withdraw(bankAccount, operation.Amount);
        }
        else
        {
            BankAccountManager.Withdraw(operation.BankAccount, operation.Amount);
            BankAccountManager.Deposit(bankAccount, operation.Amount);
        }
        
        operation.BankAccount = bankAccount;
    }

    /// <summary>
    /// Изменение категории операции
    /// </summary>
    /// <param name="id">ID операции</param>
    /// <param name="category">Новая категория</param>
    public static void ChangeCategory(int id, Category.Category category)
    {
        var operation = OperationRepository.OperationById(id);
        
        if (operation.Category.Type == Type.Withdrawal && category.Type == Type.Deposit)
        {
            BankAccountManager.Deposit(operation.BankAccount, 2 * operation.Amount);
        }
        else if (operation.Category.Type == Type.Deposit && category.Type == Type.Withdrawal)
        {
            BankAccountManager.Withdraw(operation.BankAccount, 2 * operation.Amount);
        }
        
        operation.Category = category;
    }
    
    /// <summary>
    /// Изменение описания операции
    /// </summary>
    /// <param name="id">ID операции</param>
    /// <param name="description">Новое описание</param>
    public static void ChangeDescription(int id, string description)
    {
        var op = OperationRepository.OperationById(id);
        op.Description = description;
    }
}