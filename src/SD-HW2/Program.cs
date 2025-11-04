using Microsoft.Extensions.DependencyInjection;
using SD_HW2.ConsoleWork;

namespace SD_HW2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Регистрируем зависимости через соответствующий класс-синглтон
            _ = CompositionRoot.CompositionRoot.Services;
            var consoleService = CompositionRoot.CompositionRoot.Services.GetRequiredService<ConsoleService>();
            
            // Показываем главное меню
            consoleService.ShowMainMenu();
        }
    }
}