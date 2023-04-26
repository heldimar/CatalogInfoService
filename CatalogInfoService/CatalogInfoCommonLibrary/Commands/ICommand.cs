using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Commands
{
    /// <summary>
    /// Интерфейс команды
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Выполнение команды
        /// </summary>
        Task Execute();

        /// <summary>
        /// Отмена команды
        /// </summary>
        Task Undo();

        /// <summary>
        /// Возможность отмены
        /// </summary>
        bool CanUndo { get; }
    }
}
