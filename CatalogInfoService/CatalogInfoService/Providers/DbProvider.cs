using CatalogInfoCommonLibrary.Models.SqlFiltration;
using CatalogInfoCommonLibrary.Providers;
using Microsoft.Data.SqlClient;
using Dapper;
using CatalogInfoCommonLibrary.Models;
using CatalogInfoModelsLibrary.Models;
using Minio.DataModel;
using CatalogInfoCommonLibrary.Extensions;
using CatalogInfoModelsLibrary.Models.Interfaces;

namespace CatalogInfoService.Providers
{
    public class DbProvider : IDbProvider
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DbProvider(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = _configuration?.GetSection("AppSettings:ConnectionString").Value ?? throw new ArgumentNullException(nameof(_configuration));
        }

        public SqlConnection NewConnection => new SqlConnection(_connectionString);

        public string CombineFilter(IFilterUnit filter)
        {
            if (filter != null && filter.CommandsCount > 0)
            {
                return " WHERE " + filter.GetFilter();
            }
            else
            {
                return "";
            }
        }
    }
}
