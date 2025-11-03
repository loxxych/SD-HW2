namespace SD_HW2.Category;

public enum Type
{
    Deposit,
    Withdrawal,
}

public class Category
{
    public int Id { get; } = Guid.NewGuid().GetHashCode();
    public Type Type { get; set; }
    public string Name { get; set; } = "N/A";

    public Category() { }

    public Category(Type type, string name)
    {
        Id = Guid.NewGuid().GetHashCode();
        Type = type;
        Name = name;
    }
}