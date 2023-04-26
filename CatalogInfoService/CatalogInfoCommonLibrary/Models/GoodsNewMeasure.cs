using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Models
{
    public class GoodsNewMeasure
    {
        /// <summary>
        /// GoodsID
        /// </summary>
        public string GoodsID { get; set; }

        /// <summary>
        /// Длина
        /// </summary>
        public double GLength { get; set; }

        /// <summary>
        /// Ширина
        /// </summary>
        public double GWidth { get; set; }

        /// <summary>
        /// Высота
        /// </summary>
        public double GHeight { get; set; }
    }
}
