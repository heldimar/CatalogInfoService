using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Models.SqlFiltration
{
    /// <summary>
    /// Базовый класс многострочного (несколько выражений) фильтра для запроса SQL
    /// </summary>
    public abstract class FilterMultyRow : IFilterUnit
    {
        protected FilterMultyRow() { }

        protected FilterMultyRow(IEnumerable<IFilterUnit> filters)
        {
            Filters = (List<IFilterUnit>)filters;
        }

        /// <summary>
        /// Выражения в фильтре
        /// </summary>
        protected List<IFilterUnit> Filters { get; set; } = new List<IFilterUnit>();

        public int CommandsCount => Filters.Sum(x => x.CommandsCount);

        protected string rootOperation;

        public void AddFilter(IFilterUnit unit)
        {
            Filters.Add(unit);
        }

        public string GetFilter()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("(");
            for (int i = 0; i < Filters.Count; i++)
            {
                sb.AppendLine(Filters[i].GetFilter());
                if (i < Filters.Count - 1)
                {
                    sb.AppendLine($" {rootOperation} ");
                }
            }
            sb.AppendLine(")");
            return sb.ToString();
        }
    }
}
