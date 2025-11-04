namespace SD_HW2.Operation.Commands;

/// <summary>
/// Базовый декоратор для команды добавления новой операции
/// </summary>
public class AddOperationCommandDecorator : ICommand
{
    private readonly ICommand _command;

    public AddOperationCommandDecorator(ICommand command)
    {
        _command = command;
    }
    
    /// <summary>
    /// Добавляет новую операцию
    /// </summary>
    public void Execute()
    {
        _command.Execute();
    }
}