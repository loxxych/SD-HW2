using SD_HW2.FileWork;

namespace SD_HW2.BankAccount;

public class BankAccount : IComponent
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public double Balance { get; set; }

    public BankAccount()
    {
        Id = Guid.NewGuid().GetHashCode();
        Name = "N/A";
        Balance = 0;
    }
    
    public BankAccount(string name)
    {
        Id = Guid.NewGuid().GetHashCode();
        Name = name;
        Balance = 0;
    }
    
    public BankAccount(string name, double balance)
    {
        Id = Guid.NewGuid().GetHashCode();
        Name = name;
        Balance = balance;
    }
    
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}