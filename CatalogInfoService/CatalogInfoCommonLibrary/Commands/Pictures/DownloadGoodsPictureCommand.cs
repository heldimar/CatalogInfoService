using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Models;
using CatalogInfoCommonLibrary.Providers;
using CatalogInfoModelsLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Commands.Pictures
{
    /// <summary>
    /// Команда загрузки картинки
    /// </summary>
    public class DownloadGoodsPictureCommand : ICommand
    {
        public DownloadGoodsPictureCommand(IMinioDataProvider minioDataProvider, object[] args)
        {
            this.minioDataProvider = minioDataProvider;
            pathInfo = (PicPathInfo)args[0];
            OutSteream = (MemoryStream)args[1];
        }

        public bool CanUndo => false;

        readonly IMinioDataProvider minioDataProvider;

        /// <summary>
        /// Путь к файлу картинки 
        /// </summary>
        readonly PicPathInfo pathInfo;

        /// <summary>
        /// Поток, в который будет записана картинка
        /// </summary>
        readonly MemoryStream OutSteream;

        public async Task Execute()
        {
            using (var minio = minioDataProvider.NewMinioClient)
            {
                var statObjectArgs = new StatObjectArgs()
                .WithBucket(pathInfo.BucketName)
                .WithObject(pathInfo.FileName);
                await minio.StatObjectAsync(statObjectArgs);
                //Loading
                var getObjectArgs = new GetObjectArgs()
                                .WithBucket(pathInfo.BucketName)
                                .WithObject(pathInfo.FileName)
                                .WithCallbackStream(stream =>
                                {
                                    stream.CopyTo(OutSteream);
                                    stream.Dispose();
                                });
                await minio.GetObjectAsync(getObjectArgs);
                OutSteream.Position = 0;

            }
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(DownloadGoodsPictureCommand));
        }
    }
}
