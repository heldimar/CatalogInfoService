using CatalogInfoCommonLibrary.Commands.Database;
using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Models;
using CatalogInfoCommonLibrary.Providers;
using CatalogInfoModelsLibrary.Models.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Commands.Special
{
    public class CalcNumberedGoodsDifferenceCommand : ICommand
    {
        public CalcNumberedGoodsDifferenceCommand(object[] args)
        {
            minuend = (IEnumerable<INumberedGoods>)args[0];
            subtrahend = (IEnumerable<INumberedGoods>)args[1];
            difference = (List<int>)args[2];
        }

        public bool CanUndo => false;

        readonly IEnumerable<INumberedGoods> minuend;

        readonly IEnumerable<INumberedGoods> subtrahend;

        readonly List<int> difference;

        public async Task Execute()
        {
            await Task.Run(() =>
            {
                difference.AddRange(minuend.Except(subtrahend).Select(x => x.GoodsID));
            });
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(CalcNumberedGoodsDifferenceCommand));
        }
    }
}
