using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Models
{
    /// <summary>
    /// Путь к файлу в файловом хранилище
    /// </summary>
    public class PicPathInfo
    {
        /// <summary>
        /// Имя бакета
        /// </summary>
        public string BucketName { get; set; } = string.Empty;

        /// <summary>
        /// Путь к файлу в бакете
        /// </summary>
        public string FileName { get; set; } = string.Empty;
    }
}
