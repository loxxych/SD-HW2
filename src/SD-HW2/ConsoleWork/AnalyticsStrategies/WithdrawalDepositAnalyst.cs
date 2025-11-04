using SD_HW2.Analytics;
using Spectre.Console;

namespace SD_HW2;

/// <summary>
/// Стратегия аналитики доходов и расходов
/// </summary>
public class WithdrawalDepositAnalyst : IAnalyst
{
    /// <summary>
    /// Выводит информацию о соотношении расходов и доходов 
    /// </summary>
    public void DisplayAnalytics()
    {
        AnsiConsole.MarkupLine("[yellow]Сравнение расходов и доходов[/]");
        var breakdownChart = new BreakdownChart()
            .Width(60)
            .AddItem("Доходы", AnalyticsService.SumOfDeposits, Color.Green)
            .AddItem("Расходы", AnalyticsService.SumOfWithdrawals, Color.Red);
        AnsiConsole.Write(breakdownChart);
    }
}