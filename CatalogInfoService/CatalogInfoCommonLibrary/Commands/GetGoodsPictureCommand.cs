using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Factories;
using CatalogInfoCommonLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Commands
{
    public class GetGoodsPictureCommand : ICommand
    {
        public GetGoodsPictureCommand(ICommonCommandFactory factory, object[] args)
        {
            this.Factory = factory;
            OutSteream = (MemoryStream)args[0];
            GoodsID = (int)args[1];
            ImgNumber = args.Length > 2 ? (int)args[2] : 0;

        }

        public bool CanUndo => false;

        readonly ICommonCommandFactory Factory;

        /// <summary>
        /// Поток, в который будет записана картинка
        /// </summary>
        readonly MemoryStream OutSteream;

        /// <summary>
        /// GoodsID товара, для которого запрошена картинка
        /// </summary>
        readonly int GoodsID;

        /// <summary>
        /// Номер изображения
        /// </summary>
        readonly int ImgNumber;

        public async Task Execute()
        {
            var pathInfo = new PicPathInfo();

            var command = new MacroCommand(new List<ICommand>()
            {
                Factory.Resolve<ICommand>("Commands.Pictures.GetGoodsPicturePathCommand", new object[] { GoodsID, ImgNumber, pathInfo }),
                Factory.Resolve<ICommand>("Commands.Pictures.DownloadGoodsPictureCommand", new object[] { pathInfo, OutSteream})
            });

            await command.Execute();
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(GetGoodsMainPictureCommand));
        }
    }
}
