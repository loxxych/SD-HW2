namespace SD_HW2.BankAccount;

/// <summary>
/// Класс-фасад для управления балансом 
/// </summary>
public class BankAccountManager
{
    public void Withdraw(BankAccount account, double amount)
    {
        account.Balance -= amount;
    }

    public void Deposit(BankAccount account, double amount)
    {
        account.Balance += amount;
    }
}