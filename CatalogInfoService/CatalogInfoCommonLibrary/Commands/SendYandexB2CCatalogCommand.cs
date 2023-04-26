using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Factories;
using CatalogInfoCommonLibrary.Models;
using CatalogInfoCommonLibrary.Models.SqlFiltration;
using CatalogInfoModelsLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CatalogInfoCommonLibrary.Commands
{
    public class SendYandexB2CCatalogCommand : ICommand
    {
        public SendYandexB2CCatalogCommand(ICommonCommandFactory factory, object[] args)
        {
            this.Factory = factory;
        }

        public bool CanUndo => false;

        readonly ICommonCommandFactory Factory;

        public async Task Execute()
        {
            var oldCatalogGoods = new List<YandexB2CGoodsItem>();
            var catalogFileSteream = new MemoryStream();
            var catalogFullData = new List<YandexCatalogData>();
            var filterContainer = new FilterContainer();
            var difference = new List<int>();
            var catalogNewData = new List<YandexCatalogData>();

            var command = new MacroCommand(new List<ICommand>()
                {
                    Factory.Resolve<ICommand>("Commands.Database.GetYandexB2CGoodsItemsCommand", new object[] {oldCatalogGoods}),
                    Factory.Resolve<ICommand>("Commands.SqlFilterCreators.CreateYandex_B2C_FilterCommand", new object[] {filterContainer}),
                    Factory.Resolve<ICommand>("Commands.CatalogDataDownloaders.GetFilteredYandexCatalogDataCommand", new object[] { catalogFullData, filterContainer}),
                    Factory.Resolve<ICommand>("Commands.Special.CalcNumberedGoodsDifferenceCommand", new object[] {catalogFullData, oldCatalogGoods, difference}),
                    Factory.Resolve<ICommand>("Commands.Special.TakeYandexB2CNewCatalogItemsCommand", new object[] {catalogFullData, catalogNewData, difference}),
                    Factory.Resolve<ICommand>("Commands.CatalogCreators.CreateYandexCatalogCommand", new object[] {catalogNewData, catalogFileSteream}),
                    Factory.Resolve<ICommand>("Commands.Database.AddYandexB2CGoodsItemsCommand", new object[] {catalogNewData}),
                    Factory.Resolve<ICommand>("Commands.MailSending.SendMailFromStreamCommand"
                            , new object[] {catalogFileSteream, "CatalogueNewInfo.xlsx"
                            , new List<MailAddress>(){ new MailAddress("heldimar.h.o@gmail.com") }, "Новые позиции каталога Витта Компани"}),
                });

            await command.Execute();

            catalogFileSteream.Close();
            catalogFileSteream.Dispose();
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(SendYandexB2CCatalogCommand));
        }
    }
}
