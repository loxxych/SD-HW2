namespace SD_HW2.Operation.Commands;

public class AddOperationCommand : ICommand
{
    private readonly OperationRepository _operationRepository;
    private readonly double _amount;
    private readonly BankAccount.BankAccount _bankAccount;
    private readonly string? _description;
    private readonly Category.Category _category;

    public AddOperationCommand(OperationRepository operationRepository,
        double amount, BankAccount.BankAccount bankAccount, string? description,
        Category.Category category)
    {
        _operationRepository = operationRepository;
        _amount = amount;
        _bankAccount = bankAccount;
        _description = description;
        _category = category;
    }
    
    public void Execute()
    {
        _operationRepository.AddOperation(_amount, _bankAccount, _description, _category);
    }
}