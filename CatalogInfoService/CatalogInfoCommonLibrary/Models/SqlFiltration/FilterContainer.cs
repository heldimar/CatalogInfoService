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
    public class FilterContainer : IFilterUnit, IFilterContainer
    {
        protected IFilterUnit? filter;

        public int CommandsCount => filter?.CommandsCount ?? 0;

        public string GetFilter()
        {
            return filter?.GetFilter() ?? "";
        }

        public void SetFilter(IFilterUnit filter)
        {
            this.filter = filter;
        }
    }
}
