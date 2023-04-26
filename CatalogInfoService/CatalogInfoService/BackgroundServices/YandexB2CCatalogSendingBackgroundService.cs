using CatalogInfoCommonLibrary.Commands;
using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Factories;
using VittaSchedulerLib;

namespace CatalogInfoService.BackgroundServices
{
    public class YandexB2CCatalogSendingBackgroundService : BackgroundService
    {

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public YandexB2CCatalogSendingBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            this._serviceScopeFactory = serviceScopeFactory;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var func = async () =>
            {

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var logger = scope.ServiceProvider.GetService<ILogger<YandexB2CCatalogSendingBackgroundService>>();
                    var f = scope.ServiceProvider.GetService<ICommonCommandFactory>();

                    logger.LogInformation("YandexB2CCatalogSendingBackgroundService Running at:{date}", DateTimeOffset.Now);
                    try
                    {
                        await f.Resolve<ICommand>("Commands.SendYandexB2CCatalogCommand", null).Execute();
                    }
                    catch (NewCatalogItemsNotExistsException)
                    {
                        logger.LogInformation("YandexB2CCatalogSendingBackgroundService: No new data");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError("YandexB2CCatalogSendingBackgroundService: {ex}", ex.Message);
                    }
                }
            };

            await Scheduler.Daily(func, 8, 30, cancellationToken).Run();
        }
    }
}
