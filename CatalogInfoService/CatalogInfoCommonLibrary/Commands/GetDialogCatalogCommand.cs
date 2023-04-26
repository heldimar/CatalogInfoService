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
    public class GetDialogCatalogCommand : ICommand
    {
        public GetDialogCatalogCommand(ICommonCommandFactory factory, object[] args)
        {
            this.Factory = factory;
            OutSteream = (MemoryStream)args[0];
            Filter = (IFilterUnit)args[1];
        }

        public bool CanUndo => false;

        readonly ICommonCommandFactory Factory;

        /// <summary>
        /// Поток, в который будет записан файл каталога
        /// </summary>
        readonly MemoryStream OutSteream;

        /// <summary>
        /// Фильтр, по которому будут формироваться данные каталога
        /// </summary>
        readonly IFilterUnit Filter;

        public async Task Execute()
        {
            var data = new List<DialogCatalogueData>();

            var command = new MacroCommand(new List<ICommand>()
            {
                Factory.Resolve<ICommand>("Commands.CatalogDataDownloaders.GetFilteredDialogCatalogDataCommand", new object[] { data, Filter}),
                Factory.Resolve<ICommand>("Commands.CatalogCreators.CreateDialogCatalogCommand", new object[] { data, OutSteream})
            });

            await command.Execute();
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(GetDialogCatalogCommand));
        }
    }
}
