namespace SD_HW2.BankAccount;

public static class BankAccountRepository
{
    private static BankAccountFactory _bankAccountFactory = new BankAccountFactory();
    public static List<BankAccount> BankAccounts { get; } = [];
    
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
    
    public static void AddBankAccount(string name)
    {
        var bankAccount = _bankAccountFactory.CreateBankAccount(name);
        BankAccounts.Add(bankAccount);
    }

    public static void RemoveBankAccount(string name)
    {
        var account = FindBankAccount(name);
        BankAccounts.Remove(account);
    }

    public static BankAccount FindBankAccount(string name)
    {
        foreach (var bankAccount in BankAccounts) 
        {
            if (bankAccount.Name == name)
            {
                return bankAccount;
            }
        }
        throw new ArgumentOutOfRangeException($"Не существует счета с именем {name}");
    }
    
    public static void ChangeAccountName(string name, string newName)
    {
        var account = FindBankAccount(name);
        account.Name = newName;
    }
}