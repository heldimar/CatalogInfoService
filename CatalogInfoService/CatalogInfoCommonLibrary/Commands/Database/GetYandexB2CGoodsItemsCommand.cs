using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Models;
using CatalogInfoCommonLibrary.Providers;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Commands.Database
{
    /// <summary>
    /// Команда получения записей из таблицы списка SKU, отправленных Яндексу в каталоге
    /// </summary>
    public class GetYandexB2CGoodsItemsCommand : ICommand
    {
        public GetYandexB2CGoodsItemsCommand(IDbProvider dbProvider, object[] args)
        {
            this.dbProvider = dbProvider;
            items = (List<YandexB2CGoodsItem>)args[0];
        }

        public bool CanUndo => false;

        readonly IDbProvider dbProvider;

        readonly List<YandexB2CGoodsItem> items;

        public async Task Execute()
        {
            items.Clear();

            using (var db = dbProvider.NewConnection)
            {
                var sqlQuery = @$"
								ЗАПРОС";
                var q = await db.QueryAsync<YandexB2CGoodsItem>(sqlQuery);
                items.AddRange(q);
            }
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(GetYandexB2CGoodsItemsCommand));
        }
    }
}
