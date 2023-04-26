using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Factories;
using CatalogInfoCommonLibrary.Models.SqlFiltration;
using CatalogInfoModelsLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Commands
{
    internal class GetYandexB2BPartialCatalog : ICommand
    {
        public GetYandexB2BPartialCatalog(ICommonCommandFactory factory, object[] args)
        {
            this.Factory = factory;
            OutSteream = (MemoryStream)args[0];
            needPhotoCol = args.Length > 1 && (bool)args[1];
            needAlcoCol = args.Length > 2 && (bool)args[2];
            needSig = args.Length > 3 && (bool)args[3];
        }

        public bool CanUndo => false;

        readonly ICommonCommandFactory Factory;

        /// <summary>
        /// Поток, в который будет записан файл каталога
        /// </summary>
        readonly MemoryStream OutSteream;

        readonly bool needPhotoCol;
        readonly bool needAlcoCol;
        readonly bool needSig;

        public async Task Execute()
        {
            var data = new List<YandexCatalogData>();
            var filterContainer = new FilterContainer();

            var command = new MacroCommand(new List<ICommand>()
            {
                Factory.Resolve<ICommand>("Commands.SqlFilterCreators.CreateYandex_B2B_NotAll_FilterCommand", new object[] {filterContainer}),
                Factory.Resolve<ICommand>("Commands.CatalogDataDownloaders.GetFilteredYandexCatalogDataCommand", new object[] { data, filterContainer}),
                Factory.Resolve<ICommand>("Commands.CatalogCreators.CreateYandexCatalogCommand", new object[] { data, OutSteream, needPhotoCol, needAlcoCol, needSig})
            });

            await command.Execute();
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(GetYandexB2BPartialCatalog));
        }
    }
}
