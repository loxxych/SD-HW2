using Microsoft.Extensions.DependencyInjection;
using SD_HW2.ConsoleWork;
using SD_HW2.Factories.BankAccountFactory;
using SD_HW2.Factories.CategoryFactory;
using SD_HW2.Factories.OperationFactory;
using SD_HW2.FileWork;
using SD_HW2.FileWork.ExportService;
using SD_HW2.FileWork.ExportService.ExportFactories;
using SD_HW2.FileWork.Import;
using SD_HW2.FileWork.Import.FileImporters;
using SD_HW2.FileWork.Import.ImportFactories;
using SD_HW2.FileWork.ImportService;

namespace SD_HW2.CompositionRoot
{
    /// <summary>
    /// Синглтон-класс DI контейнера
    /// </summary>
    public static class CompositionRoot
    {
        private static IServiceProvider? _services;
        public static IServiceProvider Services => _services ??= CreateServiceProvider();

        private static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();
            
            // Регистрируем фабрики основных моделей
            services.AddSingleton<IBankAccountFactory, BankAccountFactory>();
            services.AddSingleton<ICategoryFactory, CategoryFactory>();
            services.AddSingleton<IOperationFactory, OperationFactory>();
            
            // Регистрируем сервисы для импорта
            services.AddSingleton<CsvFileImporter>();
            services.AddSingleton<JsonFileImporter>();
            services.AddSingleton<IImportService, ImportService>();
            services.AddSingleton<JsonFileImporterFactory>();
            services.AddSingleton<CsvFileImporterFactory>();
            
            // Регистрируем сервисы для экспорта
            services.AddSingleton<IVisitorFactoryProvider, ExportVisitorFactoryProvider>();
            services.AddSingleton<IExportService, ExportService>();
            services.AddSingleton<CsvExportVisitorFactory>();
            services.AddSingleton<JsonExportVisitorFactory>();
            
            // Регистрируем аналитические сервисы
            services.AddSingleton<OperationsAnalyst>();
            services.AddSingleton<WithdrawalDepositAnalyst>();
            
            // Регистрируем основной консольный сервис
            services.AddSingleton<ConsoleService>();
            
            return services.BuildServiceProvider();
        }
    }
}