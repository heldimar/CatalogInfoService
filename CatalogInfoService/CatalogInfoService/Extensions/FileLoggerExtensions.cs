using CatalogInfoService.Providers;

namespace CatalogInfoService.Extensions
{
    public static class FileLoggerExtensions
    {
        public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder)
        {
            builder.AddProvider(new FileLoggerProvider());
            return builder;
        }
    }
}
