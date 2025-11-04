using SD_HW2.Analytics;
using SD_HW2.BankAccount;
using SD_HW2.Category;
using SD_HW2.FileWork;
using SD_HW2.FileWork.ExportService;
using SD_HW2.Operation;
using SD_HW2.Operation.Commands;
using Spectre.Console;
using Type = SD_HW2.Category.Type;

namespace SD_HW2;

public class ConsoleService
{
    
    private IExportService _exportService;
    private IImportService _importService;
    
    public ConsoleService(IExportService exportService, IImportService importService)
    {
        _exportService = exportService;
        _importService = importService;
    }
    
    public void ShowMainMenu()
    {
        while (true)
        {
            AnsiConsole.Clear();
                
            AnsiConsole.MarkupLine("[bold green]ВШЭ-Банк[/]");
            AnsiConsole.MarkupLine("[grey]Учет финансов[/]");
            AnsiConsole.WriteLine();
                
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Выберите секцию:[/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Счета",
                        "Категории",
                        "Операции",
                        "Аналитика",
                        "Экспорт в файл",
                        "Импорт из файла",
                        "Выход"
                    }));
                
            switch (choice)
            {
                case "Счета":
                    ShowBankAccountMenu();
                    break;
                case "Категории":
                    ShowCategoryMenu();
                    break;
                case "Операции":
                    ShowOperationMenu();
                    break;
                case "Аналитика":
                    ShowAnalytics();
                    break;
                case "Экспорт в файл":
                    ShowExportMenu();
                    break;
                case "Импорт из файла":
                    ShowImportMenu();
                    break;
                case "Выход":
                    return;
            }
        }
    }

    private void ShowBankAccountMenu()
    {
        AnsiConsole.Clear();
        
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите действие:[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Просмотр счетов",
                    "Добавление счета",
                    "Удаление счета",
                    "Редактирование счета",
                    "<- Назад"
                }));
                
        switch (choice)
        {
            case "Просмотр счетов":
                ShowBankAccounts();
                break;
            case "Добавление счета":
                ShowAddBankAccountMenu();
                break;
            case "Удаление счета":
                ShowDeleteBankAccountMenu();
                break;
            case "Редактирование счета":
                ShowEditBankAccountMenu();
                break;
            case "<- Назад":
                ShowMainMenu();
                break;
        }
    }

    private void ShowBankAccounts()
    {
        AnsiConsole.Clear();
        
        AnsiConsole.MarkupLine("[bold green]Доступные счета:[/]");
        AnsiConsole.WriteLine();
        
        var bankAccounts = BankAccountRepository.BankAccounts;

        // Создаем таблицу для отображения
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Название");
        table.AddColumn("Баланс");

        foreach (var account in bankAccounts)
        {
            var id = account.Id.ToString();
            var name = account.Name;
            var balance = account.Balance.ToString("C"); // Форматируем как валюту
        
            table.AddRow(id, name, balance);
        }
            
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();

        ConsoleCommands.AwaitInput();
        
        ShowBankAccountMenu();
    }
    
    private void ShowAddBankAccountMenu()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold green]Добавление счета[/]");
        AnsiConsole.WriteLine();
        
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Введите название счета:[/]")
                .PromptStyle("green"));
        
        BankAccountRepository.AddBankAccount(name);
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold green]Счет успешно добавлен![/]");
        ConsoleCommands.AwaitInput();
        
        ShowBankAccountMenu();
    }

    void ShowDeleteBankAccountMenu()
    {
        AnsiConsole.Clear();
        
        var bankAccounts = BankAccountRepository.BankAccountNames;
        
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold red]Выберите счет для удаления:[/]")
                .PageSize(10)
                .AddChoices(bankAccounts));

        BankAccountRepository.RemoveBankAccount(choice);
        
        AnsiConsole.Clear();
        
        AnsiConsole.MarkupLine("[green]Счет удален[/]");
        
        ConsoleCommands.AwaitInput();
        ShowBankAccountMenu();
    }

    void ShowEditBankAccountMenu()
    {
        AnsiConsole.Clear();
        
        var bankAccounts = BankAccountRepository.BankAccountNames;
        
        var account = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите счет для редактирования:[/]")
                .PageSize(10)
                .AddChoices(bankAccounts));
                
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Введите новое название:[/]")
                .PromptStyle("green"));
        
        BankAccountRepository.ChangeAccountName(account, name);
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Название изменено[/]");
        
        ConsoleCommands.AwaitInput();
        ShowBankAccountMenu();
    }

    private void ShowCategoryMenu()
    {
        AnsiConsole.Clear();
        
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите действие:[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Просмотр категорий",
                    "Добавление категории",
                    "Удаление категории",
                    "Редактирование категории",
                    "<- Назад"
                }));
                
        switch (choice)
        {
            case "Просмотр категорий":
                ShowCategories();
                break;
            case "Добавление категории":
                ShowAddCategoryMenu();
                break;
            case "Удаление категории":
                ShowDeleteCategoryMenu();
                break;
            case "Редактирование категории":
                ShowEditCategoryMenu();
                break;
            case "<- Назад":
                ShowMainMenu();
                break;
        }
    }

    private void ShowCategories()
    {
        AnsiConsole.Clear();
        
        AnsiConsole.MarkupLine("[bold green]Категории:[/]");
        AnsiConsole.WriteLine();
        
        var categories = CategoryRepository.Categories;

        // Создаем таблицу для отображения
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Название");
        table.AddColumn("Тип");

        foreach (var category in categories)
        {
            var id = category.Id.ToString();
            var name = category.Name;
            var type = category.Type.ToString();
        
            table.AddRow(id, name, type);
        }
            
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();

        ConsoleCommands.AwaitInput();
        
        ShowCategoryMenu();
    }
    
    private void ShowAddCategoryMenu()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold green]Добавление категории[/]");
        AnsiConsole.WriteLine();
        
        var typeStr = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Выберите тип:[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Доход",
                    "Расход",
                }));
        
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Введите название:[/]")
                .PromptStyle("green"));
        
        var type = typeStr == "Доход"? Type.Deposit : Type.Withdrawal;
        
        CategoryRepository.AddCategory(type, name);
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold green]Категория успешно добавлена![/]");
        ConsoleCommands.AwaitInput();
        
        ShowCategoryMenu();
    }
    
    void ShowDeleteCategoryMenu()
    {
        AnsiConsole.Clear();
        
        var categories = CategoryRepository.CategoriesNames;
        
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold red]Выберите категорию для удаления:[/]")
                .PageSize(10)
                .AddChoices(categories));

        CategoryRepository.RemoveCategory(choice);
        
        AnsiConsole.Clear();
        
        AnsiConsole.MarkupLine("[green]Категория удалена[/]");
        
        ConsoleCommands.AwaitInput();
        ShowCategoryMenu();
    }
    
    void ShowEditCategoryMenu()
    {
        AnsiConsole.Clear();
        
        var categories = CategoryRepository.CategoriesNames;
        
        var category = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите категорию для редактирования:[/]")
                .PageSize(10)
                .AddChoices(categories));
            
        var field = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите что отредактировать:[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Название",
                    "Тип"
                }));
        
        switch (field)
        {
            case "Название":
                var name = AnsiConsole.Prompt(
                    new TextPrompt<string>("[yellow]Введите новое название:[/]")
                        .PromptStyle("green"));
                CategoryRepository.ChangeCategoryName(category, name);
                break;
            case "Тип":
                var typeStr = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Выберите тип:[/]")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "Доход",
                            "Расход",
                        }));
        
                var type = typeStr == "Доход"? Type.Deposit : Type.Withdrawal;
                CategoryRepository.ChangeCategoryType(category, type);
                break;
        }
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Название изменено[/]");
        
        ConsoleCommands.AwaitInput();
        ShowCategoryMenu();
    }
    
    private void ShowOperationMenu()
    {
        AnsiConsole.Clear();
        
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите действие:[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Просмотр операций",
                    "Добавление операции",
                    "Удаление операции",
                    "Редактирование операции",
                    "<- Назад"
                }));
                
        switch (choice)
        {
            case "Просмотр операций":
                ShowOperations();
                break;
            case "Добавление операции":
                ShowAddOperationMenu();
                break;
            case "Удаление операции":
                ShowDeleteOperationMenu();
                break;
            case "Редактирование операции":
                ShowEditOperationMenu();
                break;
            case "<- Назад":
                ShowMainMenu();
                break;
        }
    }

    private void ShowOperations()
    {
        AnsiConsole.Clear();
        
        AnsiConsole.MarkupLine("[bold green]Текущие операции:[/]");
        AnsiConsole.WriteLine();
        
        var operations = OperationRepository.Operations;

        // Создаем таблицу для отображения
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Счет");
        table.AddColumn("Сумма");
        table.AddColumn("Дата");
        table.AddColumn("Описание");
        table.AddColumn("Категория");
        table.AddColumn("Тип");

        foreach (var op in operations)
        {
            var id = op.Id.ToString();
            var account = op.BankAccount.Name;
            var amount = op.Amount.ToString("C");
            var date = op.Date.ToString("yyyy-MM-ddTHH:mm:ss");
            var description = op.Description;
            var category = op.Category.Name;
            var type = op.Category.Type;
            var typeStr = type == Type.Withdrawal? "Расход" : "Доход";
        
            table.AddRow(id, account, amount, date, description, category, typeStr);
        }
            
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();

        ConsoleCommands.AwaitInput();
        
        ShowOperationMenu();
    }

    
    private void ShowAddOperationMenu()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold green]Добавление операции[/]");
        AnsiConsole.WriteLine();

        var account = ConsoleCommands.ChooseAccount();
        var category = ConsoleCommands.ChooseCategory();
        var amount = ConsoleCommands.ChooseAmount();
        var description = ConsoleCommands.ChooseDescription();
        
        var addOperationCommand = new AddOperationCommand(amount, account, description, category);
        addOperationCommand.Execute();
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold green]Операция успешно добавлена![/]");
        ConsoleCommands.AwaitInput();
        
        ShowOperationMenu();
    }

    private void ShowDeleteOperationMenu()
    {
        AnsiConsole.Clear();
        
        var operations = OperationRepository.OperationsInfo;
        
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold red]Выберите операцию для удаления:[/]")
                .PageSize(10)
                .AddChoices(operations));

        OperationRepository.RemoveOperation(int.Parse(choice.Split(':')[0].Trim()));
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Операция удалена[/]");
        ConsoleCommands.AwaitInput();
        
        ShowOperationMenu();
    }

    private void ShowEditOperationMenu()
    {
        AnsiConsole.Clear();
        
        var operations = OperationRepository.OperationsInfo;
        
        // Проверяем, есть ли операции для редактирования
        if (!operations.Any())
        {
            AnsiConsole.MarkupLine("[red]Нет доступных операций для редактирования![/]");
            AnsiConsole.MarkupLine("[yellow]Сначала создайте операции в меню добавления операций.[/]");
            AnsiConsole.Prompt(
                new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                    .AllowEmpty());
            ShowOperationMenu();
            return;
        }
        
        var op = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold green]Выберите операцию для редактирования:[/]")
                .PageSize(10)
                .AddChoices(operations));
                
        var id = int.Parse(op.Split(':')[0]);
        
        var field = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите что отредактировать:[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Счет",
                    "Категория",
                    "Сумма",
                    "Описание"
                }));
        
        switch (field)
        {
            case "Счет":
                var account = ConsoleCommands.ChooseAccount();
                OperationManager.ChangeAccount(id, account);
                break;
            case "Категория":
                var category = ConsoleCommands.ChooseCategory();
                OperationManager.ChangeCategory(id, category);
                break;
            case "Сумма":
                var amount = ConsoleCommands.ChooseAmount();
                OperationManager.ChangeAmount(id, amount);
                break;
            case "Описание":
                var description = ConsoleCommands.ChooseDescription();
                OperationManager.ChangeDescription(id, description);
                break;
        }
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Поле изменено[/]");
        ConsoleCommands.AwaitInput();
        
        ShowOperationMenu();
    }

    private void ShowExportMenu()
    {
        AnsiConsole.Clear();

        var exportData = ConsoleCommands.ChooseExportData();
        var format = ConsoleCommands.ChooseFormat();
        var fileName = ConsoleCommands.InputFileName();
        
        IFile file = new CsvFile("");
        
        switch(format)
        {
            case "Csv":
                file = new CsvFile(fileName);
                break;
            case "Json":
                file = new JsonFile(fileName);
                break;
        }

        switch (exportData)
        {
            case "Операции":
                var ops = OperationRepository.Operations;
                _exportService.ExportOperations(ops, file, format);
                break;
            case "Счета":
                var accounts = BankAccountRepository.BankAccounts;
                _exportService.ExportBankAccounts(accounts, file, format);
                break;
        }
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Данные успешно экспортированы![/]");
        ConsoleCommands.AwaitInput();
        ShowMainMenu();
    }
    
    private void ShowImportMenu()
    {
        AnsiConsole.Clear();
        
        var format = ConsoleCommands.ChooseFormat();
        var fileName = ConsoleCommands.InputFileName();
        
        IFile file = new CsvFile("");
        switch(format)
        {
            case "Csv":
                file = new CsvFile(fileName);
                break;
            case "Json":
                file = new JsonFile(fileName);
                break;
        }
        
        var ops = _importService.ImportOperations(file, format);
        OperationRepository.AddOperations(ops);
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Данные успешно импортированы![/]");
        ConsoleCommands.AwaitInput();
        ShowMainMenu();
    }

    private void ShowAnalytics()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold cyan]Аналитика[/]");
        AnsiConsole.WriteLine();
        
        AnsiConsole.MarkupLine("[yellow]Сравнение расходов и доходов[/]");
        var breakdownChart = new BreakdownChart()
            .Width(60)
            .AddItem("Доходы", AnalyticsService.SumOfDeposits, Color.Green)
            .AddItem("Расходы", AnalyticsService.SumOfWithdrawals, Color.Red);
        AnsiConsole.Write(breakdownChart);
        
        AnsiConsole.WriteLine();
        
        AnsiConsole.MarkupLine("[yellow]Время выполнения операций[/]");
        // Создаем таблицу для отображения операций с временем выполнения
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Время выполнения");
        
        var ops = AnalyticsService.OperationsWithStatistics;
        foreach (var op in ops)
        {
            var id = op.Id.ToString();
            var timeToComplete = op.TimeToComplete.ToString();
        
            table.AddRow(id, timeToComplete);
        }
            
        AnsiConsole.Write(table);
        
        ConsoleCommands.AwaitInput();
    }
}