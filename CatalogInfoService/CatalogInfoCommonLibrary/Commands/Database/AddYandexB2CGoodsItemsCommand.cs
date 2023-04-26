using CatalogInfoCommonLibrary.Commands.Pictures;
using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Extensions;
using CatalogInfoCommonLibrary.Factories;
using CatalogInfoCommonLibrary.Models;
using CatalogInfoCommonLibrary.Models.SqlFiltration;
using CatalogInfoCommonLibrary.Providers;
using CatalogInfoModelsLibrary.Models.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CatalogInfoCommonLibrary.Commands.Database
{
    /// <summary>
    /// Команда добавления новых записей в таблицу списка SKU, отправленных Яндексу в каталоге
    /// </summary>
    public class AddYandexB2CGoodsItemsCommand : ICommand
    {
        public AddYandexB2CGoodsItemsCommand(IDbProvider dbProvider, ICommonCommandFactory factory, object[] args)
        {
            this.dbProvider = dbProvider;
            this.factory = factory;
            items = (IEnumerable<INumberedGoods>)args[0];
        }

        public bool CanUndo => true;

        readonly IDbProvider dbProvider;

        ICommonCommandFactory factory;

        readonly IEnumerable<INumberedGoods> items;

        public async Task Execute()
        {
            using (var db = dbProvider.NewConnection)
            {
                using (var copy = new SqlBulkCopy(db))
                {
                    copy.DestinationTableName = "ТАБЛИЦА";
                    copy.ColumnMappings.Add(nameof(INumberedGoods.GoodsID), "GoodsID");

                    await copy.WriteToServerAsync(items.AsDataTable());
                }
            }
        }

        public async Task Undo()
        {
            var filterContainer = new FilterContainer();

            var command = new MacroCommand(new List<ICommand>()
                {
                    factory.Resolve<ICommand>("Commands.SqlFilterCreators.NumberedGoodsToSqlFilterConvertCommand", new object[] {items, filterContainer}),
                    factory.Resolve<ICommand>("Commands.Database.DeleteYandexB2CGoodsItemsCommand", new object[] {filterContainer})
                });

            await command.Execute();
        }
    }
}
