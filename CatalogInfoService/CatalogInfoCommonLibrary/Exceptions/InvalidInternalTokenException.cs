using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Exceptions
{
    /// <summary>
    /// Исключение, выбрасываемое при вызове метода, требующего токен, с невалидным токеном
    /// </summary>
    public class InvalidInternalTokenException : Exception
    {
        public InvalidInternalTokenException() 
            : base("Invalid internal access token!") { }
    }
}
