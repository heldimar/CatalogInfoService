using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Exceptions
{
    /// <summary>
    /// Исключение, выбрасываемое в случае, если путь к файлу в файловом хранилище отсутствует
    /// </summary>
    public class FilePathNotExistsException : Exception
    {
        public FilePathNotExistsException(string description)
            : base($"File path doesn't not exists: {description}") { }
    }
}
