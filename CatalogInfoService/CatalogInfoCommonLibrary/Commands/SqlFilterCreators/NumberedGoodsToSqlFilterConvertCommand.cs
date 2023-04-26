using CatalogInfoCommonLibrary.Commands.Database;
using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Models;
using CatalogInfoCommonLibrary.Models.SqlFiltration;
using CatalogInfoCommonLibrary.Providers;
using CatalogInfoModelsLibrary.Models.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Commands.SqlFilterCreators
{
    public class NumberedGoodsToSqlFilterConvertCommand : ICommand
    {
        public NumberedGoodsToSqlFilterConvertCommand(object[] args)
        {
            items = (IEnumerable<INumberedGoods>)args[0];
            filterContainer = (IFilterContainer)args[1];
        }

        public bool CanUndo => false;

        readonly IFilterContainer filterContainer;

        readonly IEnumerable<INumberedGoods> items;

        public async Task Execute()
        {
            await Task.Run(() =>
            {
                var filter = new AdditiveLogicFilter();
                foreach(var item in items)
                {
                    filter.AddFilter(new FilterRow("GoodsID", "=", $"{item.GoodsID}"));
                }
                filterContainer.SetFilter(filter);
            });
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(NumberedGoodsToSqlFilterConvertCommand));
        }
    }
}
