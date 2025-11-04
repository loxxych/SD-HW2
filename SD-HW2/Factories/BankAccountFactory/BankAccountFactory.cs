namespace SD_HW2.Factories.BankAccountFactory;

/// <summary>
/// Фабрика счетов
/// </summary>
public class BankAccountFactory : IBankAccountFactory
{
    public BankAccount.BankAccount CreateBankAccount(string name)
    {
        return new BankAccount.BankAccount(name);
    }

}