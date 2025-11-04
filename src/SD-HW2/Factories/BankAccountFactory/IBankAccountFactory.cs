namespace SD_HW2.Factories.BankAccountFactory;

/// <summary>
/// Интерфейс фабрики счетов
/// </summary>
public interface IBankAccountFactory
{
    public BankAccount.BankAccount CreateBankAccount(string name);
}