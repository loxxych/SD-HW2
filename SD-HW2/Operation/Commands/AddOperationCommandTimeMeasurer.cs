using System.Diagnostics;

namespace SD_HW2.Operation.Commands;

/// <summary>
/// Декоратор, подсчитывающий время выполнения операции
/// </summary>
public class AddOperationCommandTimeMeasurer : AddOperationCommandDecorator
{
    public AddOperationCommandTimeMeasurer(ICommand command) : base(command) { }

    /// <summary>
    /// Добавляет новую операцию и считает время ее выполнения
    /// </summary>
    public void Execute()
    {
        // Инициализируем таймер для измерения времени работы операции
        var stopwatch = Stopwatch.StartNew();
        
        // Выполняем команду в родителе
        base.Execute();
        
        // Останавливаем таймер
        stopwatch.Stop();
        
        // Получаем добавленную операцию
        var lastOperation = OperationRepository.Operations.LastOrDefault();
        if (lastOperation != null)
        {
            // Изменяем ее время выполнения
            lastOperation.TimeToComplete = stopwatch.Elapsed;
        }
    }
}