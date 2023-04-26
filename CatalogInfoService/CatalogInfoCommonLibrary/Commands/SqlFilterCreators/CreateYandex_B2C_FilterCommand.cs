using CatalogInfoCommonLibrary.Exceptions;
using CatalogInfoCommonLibrary.Factories;
using CatalogInfoCommonLibrary.Models.SqlFiltration;
using CatalogInfoModelsLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Commands.SqlFilterCreators
{
    /// <summary>
    /// Команда создания фильтра для схемы B2C
    /// </summary>
    public class CreateYandex_B2C_FilterCommand : ICommand
    {
        public CreateYandex_B2C_FilterCommand(object[] args)
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
                    new FilterRow("C", "is", "null"),
                    new AdditiveLogicFilter(new List<IFilterUnit>()
                    {
                        new FilterRow("D", "is", "null"),
                        new FilterRow("E", "<=", "25")
                    }),
                    new AdditiveLogicFilter(new List<IFilterUnit>()
                    {
                        new FilterRow("F", "!=", "x"),
                        new MultiplicativeLogicFilter(new List<IFilterUnit>()
                        {
                            new FilterRow("F", "=", "x"),
                            new FilterRow("G", "=", "'?'")
                        })
                    })
                });

                FilterContainer.SetFilter(Filter);
            });
        }

        public Task Undo()
        {
            throw new ImpossibleUndoException(nameof(CreateYandex_B2C_FilterCommand));
        }
    }
}
