using CatalogInfoCommonLibrary.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Factories
{
    /// <summary>
    /// Фабрика общих для решения команд
    /// </summary>
    public interface ICommonCommandFactory
    {
        /// <summary>
        /// Получение конкретной команды
        /// </summary>
        /// <param name="commandTitle">Название команды</param>
        /// <param name="args">Список параметров, передаваемых в команду</param>
        public T Resolve<T>(string commandTitle, object[]? args);
    }
}
