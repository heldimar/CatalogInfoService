using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Models.SqlFiltration
{
    /// <summary>
    /// Многострочный (несколько выражений) фильтр для запроса SQL с аддитивной логикой
    /// </summary>
    public class AdditiveLogicFilter : FilterMultyRow
    {
        public AdditiveLogicFilter()
        {
            rootOperation = "OR";
        }

        public AdditiveLogicFilter(IEnumerable<IFilterUnit> filters) : base(filters)
        {
            rootOperation = "OR";
        }

    }
}
