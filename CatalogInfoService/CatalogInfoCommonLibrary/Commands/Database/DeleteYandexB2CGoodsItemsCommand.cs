using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Models;
using CatalogInfoCommonLibrary.Models.SqlFiltration;
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
    /// Команда удаления записей из таблицы списка SKU, отправленных Яндексу в каталоге
    /// </summary>
    public class DeleteYandexB2CGoodsItemsCommand : ICommand
    {
        public DeleteYandexB2CGoodsItemsCommand(IDbProvider dbProvider, object[] args)
        {
            this.dbProvider = dbProvider;
            filter = (IFilterUnit)args[0];
        }

        public bool CanUndo => false;

        readonly IDbProvider dbProvider;

        readonly IFilterUnit filter;

        public async Task Execute()
        {
            string filterString = "";
            if (filter != null && filter.CommandsCount > 0)
            {
                filterString = " WHERE " + filter.GetFilter();
            }

            using (var db = dbProvider.NewConnection)
            {
                var sqlQuery = @$"
								ТАБЛИЦА" + dbProvider.CombineFilter(filter);
                var q = await db.ExecuteAsync(sqlQuery);
            }
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(DeleteYandexB2CGoodsItemsCommand));
        }
    }
}
