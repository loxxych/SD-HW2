using Spectre.Console;

namespace SD_HW2;

/// <summary>
/// Класс-контекст для вывода аналитики
/// </summary>
public static class AnalyticsDisplayer
{
    /// <summary>
    /// Стратегия аналитики
    /// </summary>
    private static IAnalyst? _analyst;
    
    /// <summary>
    /// Задает текущего аналитика
    /// </summary>
    /// <param name="analyst">Новый аналитик</param>
    public static void SetAnalyst(IAnalyst analyst)
    {
        _analyst = analyst;
    }

    /// <summary>
    /// Выводит в консоль аналитику
    /// </summary>
    /// <exception cref="Exception">Выбрасывается, если не указана стратегия аналитики</exception>
    public static void DisplayAnalytics()
    {
        if (_analyst == null)
        {
            throw new Exception("Нет аналитика у класса AnalyticsDisplayer");
        }
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold cyan]Аналитика[/]");
        AnsiConsole.WriteLine();
        
        // Отображаем нужную аналитику
        _analyst.DisplayAnalytics();
    }
}