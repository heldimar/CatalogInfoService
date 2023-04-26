using CatalogInfoModelsLibrary.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Models
{
    public class YandexB2CGoodsItem : INumberedGoods
    {
        public int GoodsID { get; set; }
    }
}
