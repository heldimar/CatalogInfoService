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
    /// Команда формирования файла каталога для Диалог
    /// </summary>
    public class CreateDialogCatalogCommand : ICommand
    {
        public CreateDialogCatalogCommand(object[] args)
        {
            PriceData = (List<DialogCatalogueData>)args[0];
            OutSteream = (MemoryStream)args[1];
        }

        public bool CanUndo => false;

        /// <summary>
        /// Коллекция данных каталога
        /// </summary>
        readonly List<DialogCatalogueData> PriceData;

        /// <summary>
        /// Поток, в который будет записан файл каталога
        /// </summary>
        readonly MemoryStream OutSteream;

        public async Task Execute()
        {
            await Task.Run(() =>
            {
                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    IWorkbook workbook = excelEngine.Excel.Workbooks.Create();
                    workbook.Version = ExcelVersion.Excel2013;
                    var worksheet = workbook.Worksheets[0];

                    worksheet[1, 1].Value = "Код товара";
                    worksheet[1, 2].Value = "Наименование";
                    worksheet[1, 3].Value = "Форма выпуска";
                    worksheet[1, 4].Value = "Производитель";
                    worksheet[1, 5].Value = "Ссылка на изображение";
                    worksheet[1, 6].Value = "Основной вид (да/нет)";

                    int row = 2;
                    foreach (DialogCatalogueData item in PriceData)
                    {
                        worksheet[row, 1].Value = item.GoodsID.ToString();
                        worksheet[row, 2].Value = item.DrugTitle;
                        worksheet[row, 3].Value = item.OutFormTitle;
                        worksheet[row, 4].Value = item.MakerTitle;
                        worksheet[row, 5].Value = item.PicUrl;
                        worksheet[row, 6].Value = item.IsMainPic;

                        row++;
                    }

                    workbook.SaveAs(OutSteream);
                    OutSteream.Position = 0;
                    workbook.Close();
                }
            });
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(CreateDialogCatalogCommand));
        }
    }
}
