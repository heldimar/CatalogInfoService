using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Models.SqlFiltration
{
    /// <summary>
    /// Фильтр для запроса SQL (формирует строку в блоке WHERE)
    /// </summary>
    public interface IFilterUnit
    {
        /// <summary>
        /// Получение строки фильтра
        /// </summary>
        public string GetFilter();

        /// <summary>
        /// Кол-во строк (выражений) фильтра
        /// </summary>
        public int CommandsCount { get; }
    }
}
