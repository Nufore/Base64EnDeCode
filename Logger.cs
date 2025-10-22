using Serilog;

namespace Base64EnDeCode
{
    public class CustomLogger
    {
        private Serilog.Core.Logger _logger;
        public CustomLogger()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log.txt")
                .CreateLogger();
        }

        public void Log(string message)
        {
            _logger.Information(message);
            Console.WriteLine(message);
        }
    }
}