using SD_HW2.BankAccount;
using SD_HW2.Category;
using Spectre.Console;

namespace SD_HW2;

public static class ConsoleCommands
{
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

    public static  Category.Category ChooseCategory()
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

    public static Double ChooseAmount()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<double>("[yellow]Введите сумму:[/]")
                .PromptStyle("yellow"));
    }

    public static string ChooseDescription()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Введите описание:[/]")
                .PromptStyle("yellow")
                .AllowEmpty());
    }

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
    
    public static string InputFileName()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Введите название файла:[/]")
                .PromptStyle("yellow"));
    }
    
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
    
    public static void AwaitInput()
    {
        AnsiConsole.Prompt(
            new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                .AllowEmpty());
    }
}