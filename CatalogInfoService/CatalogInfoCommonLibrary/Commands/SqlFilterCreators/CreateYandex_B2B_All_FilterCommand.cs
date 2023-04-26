using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Models.SqlFiltration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Commands.SqlFilterCreators
{
    /// <summary>
    /// Команда создания фильтра для схемы B2B со всеми заполненными полями
    /// </summary>
    public class CreateYandex_B2B_All_FilterCommand : ICommand
    {
        public CreateYandex_B2B_All_FilterCommand(object[] args)
        {
            FilterContainer = (IFilterContainer)args[0];
        }

        public bool CanUndo => false;

        /// <summary>
        /// Фильтр, по которому будут формироваться данные каталога
        /// </summary>
        readonly IFilterContainer FilterContainer;

        public async Task Execute()
        {
            await Task.Run(() =>
            {
                var Filter = new MultiplicativeLogicFilter(new List<IFilterUnit>()
                {
                    new FilterRow("A", "is not", "null"),
                    new FilterRow("B", "is not", "null"),
                    new FilterRow("C", "!=", "'-'"),
                    new FilterRow("D", "is not", "null"),
                    new FilterRow("E", "is", "null"),
                    new FilterRow("F", "!=", "x")
                });

                FilterContainer.SetFilter(Filter);
            });
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(CreateYandex_B2B_All_FilterCommand));
        }
    }
}
