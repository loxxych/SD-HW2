using SD_HW2.BankAccount;
using SD_HW2.Category;
using SD_HW2.FileWork;
using SD_HW2.FileWork.ExportService;
using SD_HW2.FileWork.Files;
using SD_HW2.Operation;
using SD_HW2.Operation.Commands;
using Spectre.Console;
using Type = SD_HW2.Category.Type;

namespace SD_HW2.ConsoleWork;

/// <summary>
/// Класс-фасад для работы с консолью
/// </summary>
public class ConsoleService(
    IExportService exportService,
    IImportService importService,
    OperationsAnalyst operationsAnalyst,
    WithdrawalDepositAnalyst withdrawalDepositAnalyst)
{
    /// <summary>
    /// Сервис для экспорта
    /// </summary>
    private readonly IExportService _exportService = exportService;
    
    /// <summary>
    /// Сервис для импорта
    /// </summary>
    private readonly IImportService _importService = importService;
    
    /// <summary>
    /// Стратегия аналитики операций
    /// </summary>
    private readonly OperationsAnalyst _operationsAnalyst = operationsAnalyst;
    
    /// <summary>
    /// Стратегия аналитики доходов и расходов
    /// </summary>
    private readonly WithdrawalDepositAnalyst _withdrawalDepositAnalyst = withdrawalDepositAnalyst;

    /// <summary>
    /// Отображает главное меню
    /// </summary>
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

    /// <summary>
    /// Отображает меню банковских счетов
    /// </summary>
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

    /// <summary>
    /// Отображает информацию о доступных счетах
    /// </summary>
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
            // Форматируем как валюту
            var balance = account.Balance.ToString("C");
        
            table.AddRow(id, name, balance);
        }
            
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();

        ConsoleCommands.AwaitInput();
        // Вернуться в меню счетов
        ShowBankAccountMenu();
    }
    
    /// <summary>
    /// Отображает меню с добавлением нового счета
    /// </summary>
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
        // Вернуться в меню счетов
        ShowBankAccountMenu();
    }

    /// <summary>
    /// Отображает меню с удалением счета
    /// </summary>
    private void ShowDeleteBankAccountMenu()
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
        // Возврат в меню счетов
        ShowBankAccountMenu();
    }

    /// <summary>
    /// Отображает меню с изменением счета
    /// </summary>
    private void ShowEditBankAccountMenu()
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
        
        BankAccountManager.ChangeAccountName(account, name);
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Название изменено[/]");
        
        ConsoleCommands.AwaitInput();
        ShowBankAccountMenu();
    }

    /// <summary>
    /// Отображает меню категорий
    /// </summary>
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

    /// <summary>
    /// Отображает доступные категории
    /// </summary>
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
    
    /// <summary>
    /// Отображает меню с добавлением категории
    /// </summary>
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
    
    /// <summary>
    /// Отображает меню с удалением категории
    /// </summary>
    private void ShowDeleteCategoryMenu()
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
    
    /// <summary>
    /// Отображает меню с редактированием категории
    /// </summary>
    private void ShowEditCategoryMenu()
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
                CategoryManager.ChangeCategoryName(category, name);
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
                CategoryManager.ChangeCategoryType(category, type);
                break;
        }
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Название изменено[/]");
        
        ConsoleCommands.AwaitInput();
        ShowCategoryMenu();
    }
    
    /// <summary>
    /// Отображает меню с операциями
    /// </summary>
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

    /// <summary>
    /// Отображает произведенные операции
    /// </summary>
    private void ShowOperations()
    {
        AnsiConsole.Clear();
        
        AnsiConsole.MarkupLine("[bold green]Текущие операции:[/]");
        AnsiConsole.WriteLine();
        
        var operations = OperationRepository.Operations;

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

    /// <summary>
    /// Отображает меню с добавлением операции
    /// </summary>
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

    /// <summary>
    /// Отображает меню с удалением операции
    /// </summary>
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

    /// <summary>
    /// Отображает меню с редактированием операции
    /// </summary>
    private void ShowEditOperationMenu()
    {
        AnsiConsole.Clear();
        
        var operations = OperationRepository.OperationsInfo;
        
        // Проверяем, есть ли операции для редактирования
        if (operations.Count == 0)
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

    /// <summary>
    /// Отображает меню экспорта
    /// </summary>
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
    
    /// <summary>
    /// Отображает меню импорта
    /// </summary>
    private void ShowImportMenu()
    {
        AnsiConsole.Clear();

        const string format = "Csv";
        AnsiConsole.MarkupLine("[bold blue]Импорт из csv файла[/]");
        var fileName = ConsoleCommands.InputFileName();
        
        IFile file = new CsvFile(fileName);
        
        var ops = _importService.ImportOperations(file, format);
        OperationRepository.ReplaceOperations(ops);
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Данные успешно импортированы![/]");
        ConsoleCommands.AwaitInput();
        ShowMainMenu();
    }

    /// <summary>
    /// Отображает аналитику
    /// </summary>
    private void ShowAnalytics()
    {
        AnsiConsole.Clear();
        var strategy = ConsoleCommands.ChooseAnalyticsStrategy();

        if (strategy == "По времени выполнения операций")
        {
            AnalyticsDisplayer.SetAnalyst(_operationsAnalyst);
        }
        else
        {
            AnalyticsDisplayer.SetAnalyst(_withdrawalDepositAnalyst);
        }
        
        AnalyticsDisplayer.DisplayAnalytics();
        
        ConsoleCommands.AwaitInput();
    }
}