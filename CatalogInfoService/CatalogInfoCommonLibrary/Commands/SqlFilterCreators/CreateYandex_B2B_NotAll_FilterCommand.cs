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
    /// Команда создания фильтра для схемы B2B, где заполнены не все поля
    /// </summary>
    public class CreateYandex_B2B_NotAll_FilterCommand : ICommand
    {
        public CreateYandex_B2B_NotAll_FilterCommand(object[] args)
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
                    new FilterRow("A", "!=", "x"),
                    new FilterRow("B", "is", "null"),
                    new AdditiveLogicFilter(new List<IFilterUnit>()
                    {
                        new FilterRow("C", "is", "null"),
                        new FilterRow("D", "is", "null"),
                        new FilterRow("E", "=", "'-'"),
                        new FilterRow("F", "is", "null")
                    })
                });

                FilterContainer.SetFilter(Filter);
            });
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(CreateYandex_B2B_NotAll_FilterCommand));
        }
    }
}
