namespace SD_HW2.BankAccount;

/// <summary>
/// Класс-фасад для управления балансом 
/// </summary>
public static class BankAccountManager
{
    public static void Withdraw(BankAccount account, double amount)
    {
        account.Balance -= amount;
    }

    public static void Deposit(BankAccount account, double amount)
    {
        account.Balance += amount;
    }
}