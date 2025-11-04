using SD_HW2.Analytics;
using Spectre.Console;

namespace SD_HW2;

/// <summary>
/// Стратегия аналитики операций
/// </summary>
public class OperationsAnalyst : IAnalyst
{
    /// <summary>
    /// Выводит информацию о времени выполнении операций
    /// </summary>
    public void DisplayAnalytics()
    {
        AnsiConsole.MarkupLine("[yellow]Время выполнения операций[/]");
        // Создаем таблицу для отображения операций с временем выполнения
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Время выполнения");
        
        // Получаем операции с статистикой
        var ops = AnalyticsService.OperationsWithStatistics;
        foreach (var op in ops)
        {
            var id = op.Id.ToString();
            var timeToComplete = op.TimeToComplete.ToString("G");
        
            table.AddRow(id, timeToComplete);
        }
            
        // Отображаем таблицу
        AnsiConsole.Write(table);
        
        AnsiConsole.WriteLine();
        
        // Выводим среднее время выполнения операций
        AnsiConsole.MarkupLine($@"[yellow]Среднее время выполнения операций: [bold green]{AnalyticsService.AverageTimeToComplete:hh\:mm\:ss\.ffffff}[/][/]");
    }
}