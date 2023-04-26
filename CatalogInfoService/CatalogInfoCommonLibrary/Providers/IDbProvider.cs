using CatalogInfoCommonLibrary.Extensions;
using CatalogInfoCommonLibrary.Models.SqlFiltration;
using CatalogInfoCommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogInfoModelsLibrary.Models.Interfaces;
using Microsoft.Data.SqlClient;

namespace CatalogInfoCommonLibrary.Providers
{
    public interface IDbProvider
    {
        SqlConnection NewConnection { get; }

        string CombineFilter(IFilterUnit filter);
    }
}
