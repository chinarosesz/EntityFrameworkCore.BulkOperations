using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace EntityFrameworkCore.BulkOperations.Tests
{
    internal class Helper
    { 
        public static ILogger RedirectLoggerToConsole()
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole((ConsoleLoggerOptions options) =>
                {
                    options.TimestampFormat = "yyyy-MM-ddTHH:mm:ss: ";
                    options.Format = ConsoleLoggerFormat.Systemd;
                });
            });

            ILogger logger = loggerFactory.CreateLogger(string.Empty);

            return logger;
        }
    }
}
