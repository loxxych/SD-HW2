using SD_HW2.BankAccount;
using SD_HW2.Category;
using SD_HW2.Operation;
using Spectre.Console;
using Type = SD_HW2.Category.Type;

namespace SD_HW2;

public class ConsoleService
{
    private CategoryRepository _categoryRepository;
    private BankAccountRepository _bankAccountRepository;
    private OperationRepository _operationRepository;

    public ConsoleService(CategoryRepository categoryRepository,
        BankAccountRepository bankAccountRepository,
        OperationRepository operationRepository)
    {
        _categoryRepository = categoryRepository;
        _bankAccountRepository = bankAccountRepository;
        _operationRepository = operationRepository;
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
                    //ShowAnalytics();
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
        
        var bankAccounts = _bankAccountRepository.BankAccounts;

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

        AnsiConsole.Prompt(
            new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                .AllowEmpty());
        
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
        
        _bankAccountRepository.AddBankAccount(name);
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold green]Счет успешно добавлен![/]");
        AnsiConsole.Prompt(
            new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                .AllowEmpty());
        
        ShowBankAccountMenu();
    }

    void ShowDeleteBankAccountMenu()
    {
        AnsiConsole.Clear();
        
        var bankAccounts = _bankAccountRepository.BankAccountNames;
        
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold red]Выберите счет для удаления:[/]")
                .PageSize(10)
                .AddChoices(bankAccounts));

        _bankAccountRepository.RemoveBankAccount(choice);
        
        AnsiConsole.Clear();
        
        AnsiConsole.MarkupLine("[green]Счет удален[/]");
        
        AnsiConsole.Prompt(
            new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                .AllowEmpty());
        ShowBankAccountMenu();
    }

    void ShowEditBankAccountMenu()
    {
        AnsiConsole.Clear();
        
        var bankAccounts = _bankAccountRepository.BankAccountNames;
        
        var account = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите счет для редактирования:[/]")
                .PageSize(10)
                .AddChoices(bankAccounts));
                
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Введите новое название:[/]")
                .PromptStyle("green"));
        
        _bankAccountRepository.ChangeAccountName(account, name);
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Название изменено[/]");
        
        AnsiConsole.Prompt(
            new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                .AllowEmpty());
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
        
        var categories = _categoryRepository.Categories;

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

        AnsiConsole.Prompt(
            new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                .AllowEmpty());
        
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
        
        _categoryRepository.AddCategory(type, name);
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold green]Категория успешно добавлена![/]");
        AnsiConsole.Prompt(
            new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                .AllowEmpty());
        
        ShowCategoryMenu();
    }
    
    void ShowDeleteCategoryMenu()
    {
        AnsiConsole.Clear();
        
        var categories = _categoryRepository.CategoriesNames;
        
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold red]Выберите категорию для удаления:[/]")
                .PageSize(10)
                .AddChoices(categories));

        _categoryRepository.RemoveCategory(choice);
        
        AnsiConsole.Clear();
        
        AnsiConsole.MarkupLine("[green]Категория удалена[/]");
        
        AnsiConsole.Prompt(
            new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                .AllowEmpty());
        ShowCategoryMenu();
    }
    
    void ShowEditCategoryMenu()
    {
        AnsiConsole.Clear();
        
        var categories = _categoryRepository.CategoriesNames;
        
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
                _categoryRepository.ChangeCategoryName(category, name);
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
                _categoryRepository.ChangeCategoryType(category, type);
                break;
        }
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Название изменено[/]");
        
        AnsiConsole.Prompt(
            new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                .AllowEmpty());
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
            case "Просмотр счетов":
                ShowOperations();
                break;
            case "Добавление счета":
                ShowAddOperationMenu();
                break;
            case "Удаление счета":
                ShowDeleteOperationMenu();
                break;
            case "Редактирование счета":
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
        
        var operations = _operationRepository.Operations;

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
            var date = op.Date.ToString("yyyy/MM/dd");
            var description = op.Description;
            var category = op.Category.Name;
            var type = op.Category.Type;
            var typeStr = type == Type.Withdrawal? "Расход" : "Доход";
        
            table.AddRow(id, account, amount,  date, description, category, typeStr);
        }
            
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();

        AnsiConsole.Prompt(
            new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                .AllowEmpty());
        
        ShowBankAccountMenu();
    }
    
    private void ShowAddOperationMenu()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold green]Добавление операции[/]");
        AnsiConsole.WriteLine();

        var accounts = _bankAccountRepository.BankAccountNames;
        
        var accountStr = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите счет:[/]")
                .PageSize(10)
                .AddChoices(accounts));
        
        var account = _bankAccountRepository.FindBankAccount(accountStr);
        
        var type = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите тип:[/]")
                .PageSize(10)
                .AddChoices(new []
                {
                    "Доход",
                    "Расход"
                }));
        
        var typeCategories = _categoryRepository.CategoriesByType(type);
        
        var categoryStr = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите категорию:[/]")
                .PageSize(10)
                .AddChoices(typeCategories));
        var category = _categoryRepository.FindCategory(categoryStr);
        
        var amount = AnsiConsole.Prompt(
            new TextPrompt<double>("[yellow]Введите сумму:[/]")
                .PromptStyle("yellow"));
        
        var description = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Введите описание:[/]")
                .PromptStyle("yellow"));
        
        _operationRepository.AddOperation(amount, account, description, category);
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold green]Операция успешно добавлена![/]");
        AnsiConsole.Prompt(
            new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                .AllowEmpty());
        
        ShowBankAccountMenu();
    }

    void ShowDeleteBankAccountMenu()
    {
        AnsiConsole.Clear();
        
        var bankAccounts = _bankAccountRepository.BankAccountNames;
        
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold red]Выберите счет для удаления:[/]")
                .PageSize(10)
                .AddChoices(bankAccounts));

        _bankAccountRepository.RemoveBankAccount(choice);
        
        AnsiConsole.Clear();
        
        AnsiConsole.MarkupLine("[green]Счет удален[/]");
        
        AnsiConsole.Prompt(
            new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                .AllowEmpty());
        ShowBankAccountMenu();
    }

    void ShowEditBankAccountMenu()
    {
        AnsiConsole.Clear();
        
        var bankAccounts = _bankAccountRepository.BankAccountNames;
        
        var account = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите счет для редактирования:[/]")
                .PageSize(10)
                .AddChoices(bankAccounts));
                
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Введите новое название:[/]")
                .PromptStyle("green"));
        
        _bankAccountRepository.ChangeAccountName(account, name);
        
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Название изменено[/]");
        
        AnsiConsole.Prompt(
            new TextPrompt<string>("Нажмите Enter чтобы вернуться")
                .AllowEmpty());
        ShowBankAccountMenu();
    }
}