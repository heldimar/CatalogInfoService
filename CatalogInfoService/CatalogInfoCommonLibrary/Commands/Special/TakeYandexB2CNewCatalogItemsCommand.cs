using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoModelsLibrary.Models;
using CatalogInfoModelsLibrary.Models.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Commands.Special
{
    public class TakeYandexB2CNewCatalogItemsCommand : ICommand
    {
        public TakeYandexB2CNewCatalogItemsCommand(object[] args)
        {
            source = (List<YandexCatalogData>)args[0];
            newItems = (List<YandexCatalogData>)args[1];
            difference = (List<int>)args[2];
        }

        public bool CanUndo => false;

        readonly List<YandexCatalogData> source;

        readonly List<YandexCatalogData> newItems;

        readonly List<int> difference;

        public async Task Execute()
        {
            if (difference.Count == 0)
            {
                throw new NewCatalogItemsNotExistsException();
            }
            await Task.Run(() =>
            {
                newItems.Clear();
                var difList = from s in source
                              where difference.Contains(s.GoodsID)
                              select s;
                newItems.AddRange(difList);
            });
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(TakeYandexB2CNewCatalogItemsCommand));
        }
    }
}
