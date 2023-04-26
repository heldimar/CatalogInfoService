using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Providers;
using CatalogInfoModelsLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CatalogInfoCommonLibrary.Commands.CatalogCreators
{
    /// <summary>
    /// Команда формирования файла каталога для ЯМ
    /// </summary>
    public class CreateYandexCatalogCommand : ICommand
    {
        public CreateYandexCatalogCommand(object[] args)
        {
            PriceData = (List<YandexCatalogData>)args[0];
            OutSteream = (MemoryStream)args[1];
            needPhotoCol = args.Length > 2 && (bool)args[2];
            needAlcoCol = args.Length > 3 && (bool)args[3];
            needSig = args.Length > 4 && (bool)args[4];
        }

        public bool CanUndo => false;

        /// <summary>
        /// Коллекция данных каталога
        /// </summary>
        readonly List<YandexCatalogData> PriceData;

        /// <summary>
        /// Поток, в который будет записан файл каталога
        /// </summary>
        readonly MemoryStream OutSteream;

        bool needPhotoCol;
        bool needAlcoCol;
        bool needSig;

        public async Task Execute()
        {
            await Task.Run(() =>
            {
                var xlsx = Path.Combine("XLSX", "yandex-b2b.xlsx");

                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    using (FileStream inputStream = new FileStream(xlsx, FileMode.Open, FileAccess.ReadWrite))
                    {
                        IWorkbook workbook = excelEngine.Excel.Workbooks.Open(inputStream);
                        workbook.Version = ExcelVersion.Excel2013;
                        var worksheet = workbook.Worksheets[0];

                        int row = 5;
                        foreach (YandexCatalogData item in PriceData)
                        {
                            worksheet[row, 3].Value = item.GoodsID.ToString();
                            worksheet[row, 4].Value = item.GoodsTitle;
                            if (needPhotoCol) worksheet[row, 5].Value = item.PicUrl;
                            worksheet[row, 7].Value = item.Category;
                            worksheet[row, 8].Value = item.Trademark;
                            worksheet[row, 9].Value = item.Barcodes;
                            worksheet[row, 11].Value = item.Country;
                            worksheet[row, 13].Value = item.WeightKG;
                            worksheet[row, 14].Value = item.Dimensions;
                            worksheet[row, 16].Value = item.ExpirationFormatted;
                            worksheet[row, 23].Value = item.CodeTNVED;
                            worksheet[row, 27].Value = item.CostFormatted;
                            if (needAlcoCol) worksheet[row, 34].Value = item.Alco.ToString();
                            if (needSig) worksheet[row, 35].Value = item.SigText;

                            row++;
                        }

                        workbook.SaveAs(OutSteream);
                        OutSteream.Position = 0;
                        workbook.Close();
                    }
                }
            });
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(CreateYandexCatalogCommand));
        }
    }
}
