namespace SD_HW2.Operation.Commands;

public class AddOperationCommand : ICommand
{
    private readonly double _amount;
    private readonly BankAccount.BankAccount _bankAccount;
    private readonly string? _description;
    private readonly Category.Category _category;

    public AddOperationCommand(double amount, BankAccount.BankAccount bankAccount, string? description,
        Category.Category category)
    {
        _amount = amount;
        _bankAccount = bankAccount;
        _description = description;
        _category = category;
    }
    
    public void Execute()
    {
        OperationRepository.AddOperation(_amount, _bankAccount, _description, _category);
    }
}