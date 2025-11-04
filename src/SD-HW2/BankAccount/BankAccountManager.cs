namespace SD_HW2.BankAccount;

/// <summary>
/// Класс-фасад для управления полями счетов
/// </summary>
public static class BankAccountManager
{
    /// <summary>
    /// Снять деньги со счета
    /// </summary>
    /// <param name="account">Счет</param>
    /// <param name="amount">Снимаемая сумма</param>
    public static void Withdraw(BankAccount account, double amount)
    {
        account.Balance -= amount;
    }

    /// <summary>
    /// Положить деньги на счет
    /// </summary>
    /// <param name="account">Счет</param>
    /// <param name="amount">Вносимая сумма</param>
    public static void Deposit(BankAccount account, double amount)
    {
        account.Balance += amount;
    }
    
    /// <summary>
    /// Меняет название счета
    /// </summary>
    /// <param name="name">Старое название счета</param>
    /// <param name="newName">Новое название счета</param>
    public static void ChangeAccountName(string name, string newName)
    {
        var account = BankAccountRepository.FindBankAccount(name);
        account.Name = newName;
    }
}