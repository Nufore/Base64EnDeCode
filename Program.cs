using Microsoft.Extensions.Configuration;

namespace Base64EnDeCode;

class Program
{
    static void Main(string[] args)
    {
        var logger = new CustomLogger();
        logger.Log("Запуск приложения.");
        
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true)
            .Build();

        var settings = config
            .GetSection("Config")
            .Get<Config>();
        
        Console.WriteLine("End!");
    }
}