using SD_HW2.FileWork;

namespace SD_HW2.BankAccount;

public class BankAccount : IComponent
{
    public int Id { get; } = Guid.NewGuid().GetHashCode();
    public string Name { get; set; } = "N/A";
    public double Balance { get; set; } = 0;

    public BankAccount() { }
    
    public BankAccount(string name)
    {
        Name = name;
    }
    
    public BankAccount(string name, double balance)
    {
        Name = name;
        Balance = balance;
    }
    
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}