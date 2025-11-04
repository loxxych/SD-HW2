using SD_HW2.Operation;
using Type = SD_HW2.Category.Type;

namespace SD_HW2.Analytics;

/// <summary>
/// Фасад для работы с аналитикой
/// </summary>
public static class AnalyticsService
{
    /// <summary>
    /// Массив произведенных операций с статистикой (временем исполнения) 
    /// </summary>
    public static List<Operation.Operation> OperationsWithStatistics = OperationRepository.Operations;

    /// <summary>
    /// Среднее время выполнения операций
    /// </summary>
    public static TimeSpan AverageTimeToComplete
    {
        get
        {
            // Проверяем, есть ли операции
            if (OperationsWithStatistics.Count == 0)
                return TimeSpan.Zero;

            // Суммируем время выполнения всех операций
            var totalTime = OperationsWithStatistics.Aggregate(TimeSpan.Zero, (current, op) => current + op.TimeToComplete);

            // Вычисляем среднее время
            return TimeSpan.FromTicks(totalTime.Ticks / OperationsWithStatistics.Count);
        }
    }

    /// <summary>
    /// Суммарные расходы
    /// </summary>
    public static double SumOfWithdrawals
    {
        get
        {
            double sum = 0;
            foreach (var op in OperationsWithStatistics)
            {
                // Если тип операции - расход
                if (op.Category.Type == Type.Withdrawal)
                {
                    // Добавляем его сумму в результат
                    sum += op.Amount;
                }
            }
            return sum;
        }
    }
    
    /// <summary>
    /// Суммарные доходы
    /// </summary>
    public static double SumOfDeposits
    {
        get
        {
            double sum = 0;
            foreach (var op in OperationsWithStatistics)
            {
                // Если тип операции - доход
                if (op.Category.Type == Type.Deposit)
                {
                    // Добавляем его сумму в результат
                    sum += op.Amount;
                }
            }
            return sum;
        }
    }
}