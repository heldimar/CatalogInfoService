using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Models.SqlFiltration;
using CatalogInfoCommonLibrary.Providers;
using CatalogInfoModelsLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace CatalogInfoCommonLibrary.Commands.CatalogDataDownloaders
{
    /// <summary>
    /// Команда получения данных каталога для Диалог
    /// </summary>
    public class GetFilteredDialogCatalogDataCommand : ICommand
    {
        public GetFilteredDialogCatalogDataCommand(IDbProvider dbProvider, object[] args)
        {
            this.dbProvider = dbProvider;
            CatalogData = (List<DialogCatalogueData>)args[0];
            Filter = (IFilterUnit)args[1];
        }

        public bool CanUndo => false;

        readonly IDbProvider dbProvider;

        /// <summary>
        /// Коллекция, в которую будут записаны данные каталога
        /// </summary>
        readonly List<DialogCatalogueData> CatalogData;

        /// <summary>
        /// Фильтр, по которому будут формироваться данные каталога
        /// </summary>
        readonly IFilterUnit Filter;

        public async Task Execute()
        {
            CatalogData.Clear();

            using (var db = dbProvider.NewConnection)
            {
                var sqlQuery = @$"
								ТЕЛО ЗАПРОСА" + dbProvider.CombineFilter(Filter);
                var q = await db.QueryAsync<DialogCatalogueData>(sqlQuery);
                CatalogData.AddRange(q);
            }
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(GetFilteredDialogCatalogDataCommand));
        }
    }
}
