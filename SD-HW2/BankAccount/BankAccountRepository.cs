namespace SD_HW2.BankAccount;

public class BankAccountRepository
{
    private IBankAccountFactory _bankAccountFactory;
    public List<BankAccount> BankAccounts { get; }

    public BankAccountRepository(IBankAccountFactory bankAccountFactory)
    {
        _bankAccountFactory = bankAccountFactory;
        BankAccounts = [];
    }
    
    public List<String> BankAccountNames
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
    
    public void AddBankAccount(string name)
    {
        var bankAccount = _bankAccountFactory.createBankAccount(name);
        BankAccounts.Add(bankAccount);
    }

    public void RemoveBankAccount(string name)
    {
        var account = FindBankAccount(name);
        BankAccounts.Remove(account);
    }

    public BankAccount FindBankAccount(string name)
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
    
    public void ChangeAccountName(string name, string newName)
    {
        var account = FindBankAccount(name);
        account.Name = newName;
    }
}