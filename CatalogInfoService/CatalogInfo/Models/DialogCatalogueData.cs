using CatalogInfoModelsLibrary.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogInfoModelsLibrary.Models
{
    /// <summary>
    /// Класс для каталога Диалога
    /// </summary>
    public class DialogCatalogueData : INumberedGoods
    {
        /// <summary>
        /// SKU
        /// </summary>
        public int GoodsID { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string DrugTitle { get; set; }

        /// <summary>
        /// Форма выпуска
        /// </summary>
        public string OutFormTitle { get; set; }

        /// <summary>
        /// Производитель
        /// </summary>
        public string MakerTitle { get; set; }

        /// <summary>
        /// Номер изображения
        /// </summary>
        public byte ImgNumber { get; set; }

        /// <summary>
        /// Номер главного изображения
        /// </summary>
        public byte MainImgNumber { get; set; }

        /// <summary>
        /// Ссылка на товар (фото)
        /// </summary>
        public string PicUrl => $"///////////";

        public string IsMainPic => MainImgNumber == ImgNumber ? "Да" : "Нет";
    }
}
