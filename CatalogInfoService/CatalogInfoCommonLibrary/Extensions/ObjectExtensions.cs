namespace CatalogInfoCommonLibrary.Extensions
{
   /// <summary>
   /// Расширения object
   /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Преобразует объект в строку, адаптированную для записи в БД (обрамлённую одинарными кавычками)
        /// </summary>
        /// <param name="source">Преобразуемый объект</param>
        public static string ToDbString(this object source)
        {
            return $"'{source}'";
        }

        /// <summary>
        /// Преобразует объект в строку, если объёкт <see langword="null"/> -- вернёт строку "NULL"
        /// </summary>
        /// <param name="source">Преобразуемый объект</param>
        public static string ToStringN(this object source)
        {
            if(source == null)
            {
                return "NULL";
            }
            else
            {
                return source.ToString() ?? "NULL";
            }
        }
    }
}
