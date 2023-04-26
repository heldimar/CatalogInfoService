using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Models.SqlFiltration
{
    /// <summary>
    /// Контейнер фильтра для запроса SQL
    /// </summary>
    public interface IFilterContainer
    {
        /// <summary>
        /// Устанавливает фильтр в контейнер
        /// </summary>
        /// <param name="filter">Устанавливаемый фильтр</param>
        void SetFilter(IFilterUnit filter);
    }
}
