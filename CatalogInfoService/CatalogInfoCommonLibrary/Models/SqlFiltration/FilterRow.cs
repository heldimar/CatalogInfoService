using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Models.SqlFiltration
{
    /// <summary>
    /// Однострочный (одно выражение) фильтр для запроса SQL
    /// </summary>
    public class FilterRow : IFilterUnit
    {
        public FilterRow(string fieldName, string operation, string value)
        {
            this.fieldName = fieldName;
            this.operation = operation;
            this.value = value;
        }

        /// <summary>
        /// Поле сравнения (левая часть)
        /// </summary>
        private string fieldName;

        /// <summary>
        /// Условие сравнения (средняя часть)
        /// </summary>
        private string operation;

        /// <summary>
        /// Значение для сравнения (правая часть)
        /// </summary>
        private string value;

        public int CommandsCount => 1;

        public string GetFilter()
        {
            return $"{fieldName} {operation} {value}";
        }
    }
}
