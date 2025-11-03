using Microsoft.Extensions.DependencyInjection;
using SD_HW2;
using SD_HW2.BankAccount;
using SD_HW2.Category;
using SD_HW2.Operation;
using SD_HW2.FileWork;
using SD_HW2.Operation.Commands;
using ICommand = System.Windows.Input.ICommand;

namespace SD_HW2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Создаем коллекцию сервисов
            var services = new ServiceCollection();
            
            // Регистрируем фабрики
            services.AddSingleton<IBankAccountFactory, BankAccountFactory>();
            services.AddSingleton<ICategoryFactory, CategoryFactory>();
            services.AddSingleton<IOperationFactory, OperationFactory>();
            
            // Регистрируем менеджеры
            services.AddSingleton<BankAccountManager>();
            
            // Регистрируем репозитории
            services.AddSingleton<BankAccountRepository>();
            services.AddSingleton<CategoryRepository>();
            services.AddSingleton<OperationRepository>();
            services.AddSingleton<OperationManager>();
            
            // Регистрируем сервисы для работы с файлами
            //services.AddSingleton<CsvFileExporter>();
            //services.AddSingleton<JsonFileExporter>();
            services.AddSingleton<CsvFileImporter>();
            services.AddSingleton<JsonFileImporter>();
            
            // Регистрируем основной сервис
            services.AddSingleton<ConsoleService>();
            
            // Строим провайдер сервисов
            var provider = services.BuildServiceProvider();
            
            // Получаем основной сервис и запускаем приложение
            var consoleService = provider.GetRequiredService<ConsoleService>();
            consoleService.ShowMainMenu();
        }
    }
}