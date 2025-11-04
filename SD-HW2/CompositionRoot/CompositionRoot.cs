using Microsoft.Extensions.DependencyInjection;
using SD_HW2.Analytics;
using SD_HW2.FileWork;
using SD_HW2.FileWork.ExportService;
using SD_HW2.FileWork.ExportService.ExportFactories;
using SD_HW2.FileWork.ImportFactories;

namespace SD_HW2.CompositionRoot
{
    public static class CompositionRoot
    {
        private static IServiceProvider? _services;
        public static IServiceProvider Services => _services ??= CreateServiceProvider();

        private static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();
            
            // Регистрируем фабрики
            services.AddSingleton<IBankAccountFactory, BankAccountFactory>();
            services.AddSingleton<ICategoryFactory, CategoryFactory>();
            services.AddSingleton<IOperationFactory, OperationFactory>();
            
            // Регистрируем сервисы для работы с файлами
            services.AddSingleton<CsvFileImporter>();
            services.AddSingleton<JsonFileImporter>();
            
            services.AddSingleton<IVisitorFactoryProvider, ExportVisitorFactoryProvider>();
            services.AddSingleton<FileImporterFactoryProvider>();
            services.AddSingleton<IExportService, ExportService>();
            services.AddSingleton<IImportService, ImportService>();
            services.AddSingleton<CsvExportVisitorFactory>();
            services.AddSingleton<JsonExportVisitorFactory>();
            services.AddSingleton<JsonFileImporterFactory>();
            services.AddSingleton<CsvFileImporterFactory>();
            
            // Регистрируем основной сервис
            services.AddSingleton<ConsoleService>();
            
            return services.BuildServiceProvider();
        }
    }
}