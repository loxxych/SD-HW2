namespace SD_HW2.Operation;

/// <summary>
/// Фасад для изменения полей операции
/// </summary>
public class OperationManager
{
    private OperationRepository _operationRepository;

    public OperationManager(OperationRepository operationRepository)
    {
        _operationRepository = operationRepository;
    }

    public void ChangeAmount(int id, double amount)
    {
        var op = _operationRepository.OperationById(id);
        op.Amount = amount;
    }

    public void ChangeAccount(int id, BankAccount.BankAccount bankAccount)
    {
        var op = _operationRepository.OperationById(id);
        op.BankAccount = bankAccount;
    }

    public void ChangeCategory(int id, Category.Category category)
    {
        var op = _operationRepository.OperationById(id);
        op.Category = category;
    }
    
    public void ChangeDescription(int id, string description)
    {
        var op = _operationRepository.OperationById(id);
        op.Description = description;
    }
}