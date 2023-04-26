using CatalogInfoCommonLibrary.Commands;
using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Factories;
using CatalogInfoCommonLibrary.Models.SqlFiltration;
using Minio.DataModel;

namespace CatalogInfoService.Services
{
    public class CatalogService : ICatalogService
    {
        public CatalogService(ICommonCommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        private readonly ICommonCommandFactory commandFactory;

        public async Task<MemoryStream> GetGoodsMainPictureAsync(int goodsID)
        {
            var outStream = new MemoryStream();
            await commandFactory.Resolve<ICommand>("Commands.GetGoodsMainPictureCommand", new object[] { outStream, goodsID }).Execute();
            return outStream;
        }
        public async Task<MemoryStream> GetGoodsPictureAsync(int goodsID, int imgNum)
        {
            var outStream = new MemoryStream();
            await commandFactory.Resolve<ICommand>("Commands.GetGoodsPictureCommand", new object[] { outStream, goodsID, imgNum }).Execute();
            return outStream;
        }


        public async Task CreateDialogCatalogAsync()
        {
            var file = new MemoryStream();
            await commandFactory.Resolve<ICommand>("Commands.GetDialogCatalogCommand", new object[] { file, null }).Execute();
            if (File.Exists("Каталог Диалог.xlsx"))
            {
                File.Delete("Каталог Диалог.xlsx");
            }
            using (var fstream = new FileStream("Каталог Диалог.xlsx", FileMode.Create, FileAccess.Write))
            {
                file.Position = 0;
                file.CopyTo(fstream);
            }
            file.Close();
            file.Dispose();
        }

        public async Task<MemoryStream> GetYandexB2BAllCatalog(string token)
        {
            CheckInternalToken(token);
            var outStream = new MemoryStream();
            await commandFactory.Resolve<ICommand>("Commands.GetYandexB2BAllCatalog", new object[] { outStream, true }).Execute();
            return outStream;
        }

        public async Task<MemoryStream> GetYandexB2BPartialCatalog(string token)
        {
            CheckInternalToken(token);
            var outStream = new MemoryStream();
            await commandFactory.Resolve<ICommand>("Commands.GetYandexB2BPartialCatalog", new object[] { outStream, true }).Execute();
            return outStream;
        }

        private void CheckInternalToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
