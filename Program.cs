using Microsoft.Extensions.Configuration;

namespace Base64EnDeCode;

class Program
{
    static void Main(string[] args)
    {
        var logger = new CustomLogger();
        logger.Log("Запуск приложения.");

        try
        {
            logger.Log("Попытка инициализации настроек.");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true)
                .Build();

            var settings = config
                .GetSection("Config")
                .Get<Config>();

            if (settings is null)
            {
                logger.Log("Не удалось проинициализировать настройки, проверьте файл AppSettings.json");
                return;
            }

            logger.Log("Настройки проинициализированы.");
            logger.Log("Введите команду:\n1 - base64ToFile\n2 - fileToBase64");
            var command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    logger.Log("Выбрана команда base64ToFile");
                    break;
                case "2":
                    logger.Log("Выбрана команда fileToBase64");
                    var dirAbsPath = Directory.GetCurrentDirectory();
                    Converter.ConvertToBase64(settings.encoder.dirFrom, logger);
                    break;
                default:
                    logger.Log("Некорректная команда.");
                    break;
            }
        }
        catch (Exception ex)
        {
            logger.Log($"Приложение завершило работу с ошибкой: {ex.Message}");
            Console.ReadLine();
        }
    }
}