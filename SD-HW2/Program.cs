using Microsoft.Extensions.DependencyInjection;

namespace SD_HW2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            _ = CompositionRoot.CompositionRoot.Services;
            var consoleService = CompositionRoot.CompositionRoot.Services.GetRequiredService<ConsoleService>();
            
            consoleService.ShowMainMenu();
        }
    }
}