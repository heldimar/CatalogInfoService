using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CatalogInfoService.Services
{
    /// <summary>
    /// Сервис записи логов в файл
    /// </summary>
    public class FileLoggerService : ILogger, IDisposable
    {
        int month = 0;

        string filePath = string.Empty;
        string FilePath 
        { 
            get
            {
                if(DateTime.Now.Month != month)
                {
                    month = DateTime.Now.Month;
                    var location = Assembly.GetExecutingAssembly().Location;
                    var path = Path.Combine(location.Substring(0, location.LastIndexOf('\\')), "log");

                    if (!Directory.Exists(path)) 
                    {
                        Directory.CreateDirectory(path);
                    }
                    filePath = Path.Combine(path, $"{DateTime.Now.ToString("MM.yyyy")}.log");
                }
                return filePath;
            }
        }

        static object _lock = new object();

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose() { }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel > LogLevel.Trace;
            //return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId,
                    TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            lock (_lock)
            {
                File.AppendAllText(FilePath, $"{DateTimeOffset.Now}: {formatter(state, exception)}{Environment.NewLine}");
            }
        }
    }
}
