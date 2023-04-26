using CatalogInfoService.Services;
using Microsoft.Extensions.Logging;

namespace CatalogInfoService.Providers
{
    /// <summary>
    /// Провайдер логгирования
    /// </summary>
    public class FileLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// Получение логгера
        /// </summary>
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLoggerService();
        }

        public void Dispose() { }
    }
}
