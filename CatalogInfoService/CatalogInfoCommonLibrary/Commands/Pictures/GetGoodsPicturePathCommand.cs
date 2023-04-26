using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Models;
using CatalogInfoCommonLibrary.Providers;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Commands
{
    public class GetGoodsPicturePathCommand : ICommand
    {
        public GetGoodsPicturePathCommand(IDbProvider dbProvider, object[] args)
        {
            this.dbProvider = dbProvider;
            GoodsID = (int)args[0];
            ImgNumber = (int)args[1];
            pathInfo = (PicPathInfo)args[2];
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

        /// <summary>
        /// Номер фотографии
        /// </summary>
        readonly int ImgNumber;

        public async Task Execute()
        {
            using (var db = dbProvider.NewConnection)
            {
                var sqlQuery = @$"
								ЗАПРОС";
                pathInfo.BucketName = await db.QuerySingleAsync<string>(sqlQuery);

                sqlQuery = @$"
							ЗАПРОС";
                pathInfo.FileName = await db.QuerySingleAsync<string>(sqlQuery, new { GoodsID, ImgNumber })
                        ?? throw new FilePathNotExistsException($"GetGoodsPicturePathCommand - GoodsID={GoodsID}");
            }
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(GetGoodsPicturePathCommand));
        }
    }
}
