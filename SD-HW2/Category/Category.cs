namespace SD_HW2.Category;

public enum Type
{
    Deposit,
    Withdrawal,
}

public class Category
{
    public int Id;
    public Type Type { get; set; }
    public string Name { get; set; }

    public Category()
    {
        Id =  Guid.NewGuid().GetHashCode();
    }

    public Category(Type type, string name)
    {
        Id = Guid.NewGuid().GetHashCode();
        Type = type;
        Name = name;
    }
}