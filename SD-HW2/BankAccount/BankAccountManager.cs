namespace SD_HW2.BankAccount;

/// <summary>
/// Класс-фасад для управления балансом 
/// </summary>
public class BankAccountManager
{
    private BankAccountRepository _bankAccountRepository;

    public BankAccountManager(BankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }

    void Withdraw(string accountName, double amount)
    {
        var account = _bankAccountRepository.FindBankAccount(accountName);
        account.Balance -= amount;
    }

    void Deposit(string accountName, double amount)
    {
        var account = _bankAccountRepository.FindBankAccount(accountName);
        account.Balance += amount;
    }
}