using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Models;
using CatalogInfoCommonLibrary.Models.SqlFiltration;
using CatalogInfoCommonLibrary.Providers;
using CatalogInfoModelsLibrary.Models;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Commands.Pictures
{
    /// <summary>
    /// Команда получения пути к файлу картинки товара
    /// </summary>
    public class GetGoodsMainPicturePathCommand : ICommand
    {
        public GetGoodsMainPicturePathCommand(IDbProvider dbProvider, object[] args)
        {
            this.dbProvider = dbProvider;
            GoodsID = (int)args[0];
            pathInfo = (PicPathInfo)args[1];
        }

        public bool CanUndo => false;

        readonly IDbProvider dbProvider;

        /// <summary>
        /// GoodsID товара, для которого запрошена картинка
        /// </summary>
        readonly int GoodsID;

        /// <summary>
        /// Путь к файлу картинки (будет записан в этот объект)
        /// </summary>
        readonly PicPathInfo pathInfo;

        public async Task Execute()
        {
            using (var db = dbProvider.NewConnection)
            {
                var sqlQuery = @$"
								ЗАПРОС";
                pathInfo.BucketName = await db.QuerySingleAsync<string>(sqlQuery);

                sqlQuery = @$"
							ЗАПРОС";
                pathInfo.FileName = await db.QuerySingleAsync<string>(sqlQuery, new { GoodsID })
                    ?? throw new FilePathNotExistsException($"GetGoodsMainPicturePathCommand - GoodsID={GoodsID}");
            }
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(GetGoodsMainPicturePathCommand));
        }
    }
}
