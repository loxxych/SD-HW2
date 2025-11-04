using SD_HW2.BankAccount;
using SD_HW2.Category;
using Spectre.Console;

namespace SD_HW2;

/// <summary>
/// Представляет класс некоторых операций с консолью
/// </summary>
public static class ConsoleCommands
{
    /// <summary>
    /// Выбор счета из доступных
    /// </summary>
    /// <returns>Выбранный счет</returns>
    public static BankAccount.BankAccount ChooseAccount()
    {
        var accounts = BankAccountRepository.BankAccountNames;
        
        var accountStr = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите счет:[/]")
                .PageSize(10)
                .AddChoices(accounts));
        
        return BankAccountRepository.FindBankAccount(accountStr);
    }

    /// <summary>
    /// Выбор категории из доступных
    /// </summary>
    /// <returns>Выбранная категория</returns>
    public static Category.Category ChooseCategory()
    {
        var type = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите тип:[/]")
                .PageSize(10)
                .AddChoices(new []
                {
                    "Доход",
                    "Расход"
                }));
        
        var typeCategories = CategoryRepository.CategoriesByType(type);
        
        var categoryStr = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите категорию:[/]")
                .PageSize(10)
                .AddChoices(typeCategories));
        
        return CategoryRepository.FindCategory(categoryStr);
    }

    /// <summary>
    /// Ввод суммы
    /// </summary>
    /// <returns>Введенная сумма</returns>
    public static double ChooseAmount()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<double>("[yellow]Введите сумму:[/]")
                .PromptStyle("yellow"));
    }

    /// <summary>
    /// Ввод описания
    /// </summary>
    /// <returns>Введенное описание</returns>
    public static string ChooseDescription()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Введите описание:[/]")
                .PromptStyle("yellow")
                .AllowEmpty());
    }

    /// <summary>
    /// Выбор формата файла из доступных
    /// </summary>
    /// <returns>Выбранный формат</returns>
    public static string ChooseFormat()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите формат:[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Csv",
                    "Json",
                }));
    }
    
    /// <summary>
    /// Ввод названия файла
    /// </summary>
    /// <returns>Введенное название</returns>
    public static string InputFileName()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Введите название файла:[/]")
                .PromptStyle("yellow"));
    }
    
    /// <summary>
    /// Выбор экспортируемых обьектов
    /// </summary>
    /// <returns>Выбранный тип обьекта</returns>
    public static string ChooseExportData()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите что экспортировать:[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Операции",
                    "Счета",
                }));
    }
    
    /// <summary>
    /// Выбор стратегии для аналитики
    /// </summary>
    /// <returns>Выбранная стратегия</returns>
    public static string ChooseAnalyticsStrategy()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите, какую аналитику посмотреть:[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "По времени выполнения операций",
                    "По доходам и расходам",
                }));
    }
    
    /// <summary>
    /// Ожидание ввода пользователя для возвращения
    /// </summary>
    public static void AwaitInput()
    {
        AnsiConsole.Prompt(
            new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                .AllowEmpty());
    }
}