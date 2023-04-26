using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Models.SqlFiltration
{
    /// <summary>
    /// Многострочный (несколько выражений) фильтр для запроса SQL с мультипликативной логикой
    /// </summary>
    public class MultiplicativeLogicFilter : FilterMultyRow
    {
        public MultiplicativeLogicFilter()
        {
            rootOperation = "AND";
        }
        public MultiplicativeLogicFilter(IEnumerable<IFilterUnit> filters) : base(filters)
        {
            rootOperation = "AND";
        }
    }
}
