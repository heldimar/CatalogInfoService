using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogInfoCommonLibrary.Exceptions
{
    /// <summary>
    /// Исключение, выбрасываемое в случае, если происходит попытка отменить неотменяемую команду
    /// </summary>
    public class ImpossibleUndoException : Exception
    {
        public ImpossibleUndoException(string commandType) : base($"Impossible undo command \"{commandType}\"")
        { }
    }
}
