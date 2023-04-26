using CatalogInfoModelsLibrary.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CatalogInfoModelsLibrary.Models
{
    /// <summary>
    /// Класс для каталога яндекса
    /// </summary>
    public class YandexCatalogData : INumberedGoods
    {
        /// <summary>
        /// SKU
        /// </summary>
        public int GoodsID { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string GoodsTitle { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Торговая марка
        /// </summary>
        public string Trademark { get; set; }

        /// <summary>
        /// Изготовитель
        /// </summary>
        public string Maker { get; set; }

        /// <summary>
        /// Страна производства
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Штрихкод
        /// </summary>
        public string Barcodes { get; set; }

        /// <summary>
        /// Код ТН ВЭД
        /// </summary>
        public string CodeTNVED { get; set; }

        /// <summary>
        /// Длина, мм
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Ширина, мм
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Высота, мм
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Вес, г
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Срок годности
        /// </summary>
        public byte Expiration { get; set; }

        /// <summary>
        /// Наличие изображения
        /// </summary>
        public bool HasImg { get; set; }

        /// <summary>
        /// Текущая цена
        /// </summary>
        public double Cost { get; set;}

        /// <summary>
        /// Крепкость алкоголя в процентах
        /// </summary>
        public decimal Alco { get; set; }

        /// <summary>
        /// Признак рецептуры
        /// </summary>
        public string SigText { get; set; }

        /// <summary>
        /// Ссылка на товар (фото)
        /// </summary>
        public string PicUrl => HasImg ? $"//////////////////////////////" : "";

        /// <summary>
        /// Габариты, см.
        /// </summary>
        public string Dimensions => Length == 0 ? "" :
            $"{(Length / .10).ToString("F2", CultureInfo.CreateSpecificCulture("en-GB"))}/{(Width / .10).ToString("F2", CultureInfo.CreateSpecificCulture("en-GB"))}/{(Height / .10).ToString("F2", CultureInfo.CreateSpecificCulture("en-GB"))}";

        /// <summary>
        /// Вес, кг
        /// </summary>
        public string WeightKG => Weight == 0 ? "" :
            (Weight/1000.0).ToString("F3", CultureInfo.CreateSpecificCulture("en-GB"));

        /// <summary>
        /// Форматированная стоимость
        /// </summary>
        public string CostFormatted => Cost.ToString("F2", CultureInfo.CreateSpecificCulture("de-DE"));

        public string ExpirationFormatted => $"{(Expiration < 120 ? Expiration : 120)} месяцев";
    }
}
