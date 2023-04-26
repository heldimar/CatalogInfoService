namespace CatalogInfoService.Services
{
    /// <summary>
    /// Сервис каталога
    /// </summary>
    public interface ICatalogService
    {
        /// <summary>
        /// Получение главной картинки товара
        /// </summary>
        /// <param name="GoodsID">GoodsID запрашиваемого товара</param>
        Task<MemoryStream> GetGoodsMainPictureAsync(int goodsID);

        /// <summary>
        /// Получение картинки товара
        /// </summary>
        /// <param name="GoodsID">GoodsID запрашиваемого товара</param>
        /// <param name="imgNum">Номер изображения</param>
        Task<MemoryStream> GetGoodsPictureAsync(int goodsID, int imgNum);


        Task CreateDialogCatalogAsync();

        Task<MemoryStream> GetYandexB2BAllCatalog(string token);

        Task<MemoryStream> GetYandexB2BPartialCatalog(string token);
    }
}
