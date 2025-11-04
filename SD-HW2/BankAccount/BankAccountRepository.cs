using SD_HW2.Factories.BankAccountFactory;

namespace SD_HW2.BankAccount;

/// <summary>
/// Представляет фасад-репозиторий банковских счетов
/// </summary>
public static class BankAccountRepository
{
    /// <summary>
    /// Фабрика для создания счетов
    /// </summary>
    private static readonly BankAccountFactory BankAccountFactory = new();
    
    /// <summary>
    /// Зарегистрированные счета
    /// </summary>
    public static List<BankAccount> BankAccounts { get; } = [];
    
    /// <summary>
    /// Список названий счетов
    /// </summary>
    public static List<String> BankAccountNames
    {
        get
        {
            List<String> bankAccountNames = [];

            foreach (var account in BankAccounts)
            {
                bankAccountNames.Add(account.Name);
            }

            return bankAccountNames;
        }
    }
    
    /// <summary>
    /// Добавляет новый счет
    /// </summary>
    /// <param name="name">Название счета</param>
    public static void AddBankAccount(string name)
    {
        var bankAccount = BankAccountFactory.CreateBankAccount(name);
        BankAccounts.Add(bankAccount);
    }

    /// <summary>
    /// Удаляет счет
    /// </summary>
    /// <param name="name">Название счета</param>
    public static void RemoveBankAccount(string name)
    {
        var account = FindBankAccount(name);
        BankAccounts.Remove(account);
    }

    /// <summary>
    /// Находит счет по названию (с учетом того, что все названия различны)
    /// </summary>
    /// <param name="name">Название счета</param>
    /// <returns>Банковский счет</returns>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если не найден счет с переданным названием</exception>
    public static BankAccount FindBankAccount(string name)
    {
        foreach (var bankAccount in BankAccounts.Where(bankAccount => bankAccount.Name == name))
        {
            return bankAccount;
        }

        throw new ArgumentOutOfRangeException($"Не существует счета с названием {name}");
    }
}