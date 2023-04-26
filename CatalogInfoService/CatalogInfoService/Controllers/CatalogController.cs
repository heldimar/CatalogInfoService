using CatalogInfoCommonLibrary.Commands;
using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoModelsLibrary.Models;
using CatalogInfoService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogInfoService.Controllers
{
    /// <summary>
    /// Контроллер каталога
    /// </summary>
    [Route("api/ExtSales/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService service;

        protected readonly ILogger<CatalogController> logger;

        public CatalogController(ICatalogService service, ILogger<CatalogController> logger)
        {
            this.service = service;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Получение картинки товара
        /// </summary>
        /// <param name="id">GoodsID запрашиваемого товара</param>
        [HttpGet("pic/{id}")]
        public async Task<ActionResult> GetGoodsMainPicture(int id)
        {
            logger.LogInformation("GetGoodsMainPicture requested  at {date}. GoodsID = {id}", DateTimeOffset.Now, id);
            try
            {
                var file = await service.GetGoodsMainPictureAsync(id);
                file.Position = 0;
                byte[] buffer = new byte[file.Length];
                await file.ReadAsync(buffer, 0, (int)file.Length);
                file.Close();
                file.Dispose();
                return new FileContentResult(buffer, new MediaTypeHeaderValue("image/jpeg"));
            }
            catch (FilePathNotExistsException fpneEx)
            {
                logger.LogError("GetGoodsMainPicture error at:{date}: {message}", DateTimeOffset.Now, fpneEx.Message);
                return BadRequest("Изображения запрашиваемого товара не найдены в системе!");
            }
            catch (Exception ex)
            {
                logger.LogError("GetGoodsMainPicture error at:{date}: {message}", DateTimeOffset.Now, ex.Message);
                return BadRequest("Непредвиденная ошибка сервиса каталога! Обратитесь к программистам.");
            }
        }

        /// <summary>
        /// Получение картинки товара
        /// </summary>
        /// <param name="id">GoodsID запрашиваемого товара</param>
        [HttpGet("pic/{id}/{num}")]
        public async Task<ActionResult> GetGoodsPicture(int id, int num)
        {
            logger.LogInformation("GetGoodsPicture requested  at {date}. GoodsID = {id}, ImgNumber = {num}", DateTimeOffset.Now, id, num);
            try
            {
                var file = await service.GetGoodsPictureAsync(id, num);
                file.Position = 0;
                byte[] buffer = new byte[file.Length];
                await file.ReadAsync(buffer, 0, (int)file.Length);
                file.Close();
                file.Dispose();
                return new FileContentResult(buffer, new MediaTypeHeaderValue("image/jpeg"));
            }
            catch (FilePathNotExistsException fpneEx)
            {
                logger.LogError("GetGoodsPicture error at:{date}: {message}", DateTimeOffset.Now, fpneEx.Message);
                return BadRequest("Запрашиваемое изображение не найдено в системе!!");
            }
            catch (Exception ex)
            {
                logger.LogError("GetGoodsPicture error at:{date}: {message}", DateTimeOffset.Now, ex.Message);
                return BadRequest("Непредвиденная ошибка сервиса каталога! Обратитесь к программистам.");
            }
        }

        
        [HttpPost("yandex/b2b/all")]
        public async Task<ActionResult> GetYandexB2BAllCatalog(YandexB2bCatalogRequest request)
        {
            try
            {
                var file = await service.GetYandexB2BAllCatalog(request.Token);
                file.Position = 0;
                return File(file, "application/vnd.ms-excel", "Есть все (B2B).xlsx");
            }
            catch (InvalidInternalTokenException iitEx)
            {
                logger.LogError("GetYandexB2BPartialCatalog error at:{date}: {message}", DateTimeOffset.Now, iitEx.Message);
                return BadRequest("Невалидный внутренний токен доступа сервиса каталога! Обратитесь к программистам.");
            }
            catch (Exception ex)
            {
                logger.LogError("GetYandexB2BPartialCatalog error at:{date}: {message}", DateTimeOffset.Now, ex.Message);
                return BadRequest("Непредвиденная ошибка сервиса каталога! Обратитесь к программистам.");
            }
        }

        [HttpPost("yandex/b2b/partial")]
        public async Task<ActionResult> GetYandexB2BPartialCatalog(YandexB2bCatalogRequest request)
        {
            try
            {
                var file = await service.GetYandexB2BPartialCatalog(request.Token);
                file.Position = 0;
                return File(file, "application/vnd.ms-excel", "Чего-то нет (B2B).xlsx");
            }
            catch (InvalidInternalTokenException iitEx)
            {
                logger.LogError("GetYandexB2BPartialCatalog error at:{date}: {message}", DateTimeOffset.Now, iitEx.Message);
                return BadRequest("Невалидный внутренний токен доступа сервиса каталога! Обратитесь к программистам.");
            }
            catch (Exception ex)
            {
                logger.LogError("GetYandexB2BPartialCatalog error at:{date}: {message}", DateTimeOffset.Now, ex.Message);
                return BadRequest("Непредвиденная ошибка сервиса каталога! Обратитесь к программистам.");
            }
        }

       

        [HttpPost("dialog")]
        public async Task<ActionResult> CreateDialogCatalog()
        {
            try
            {
                await service.CreateDialogCatalogAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError("CreateDialogCatalog error at:{date}: {message}", DateTimeOffset.Now, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
