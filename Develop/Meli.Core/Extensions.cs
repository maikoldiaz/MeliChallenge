using System.Data;
using System.Globalization;

namespace Meli.Core;
public static class Extensions
{
    /// <summary>
        /// Converts the IEnumerable of type object to data table.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="columnName">The columnName.</param>
        /// <param name="tableName">The tableName.</param>
        /// <returns>The data table.</returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> source, string columnName, string tableName)
            where T : struct
        {
            var dataTable = new DataTable(tableName) { Locale = CultureInfo.InvariantCulture };
            dataTable.Columns.Add(columnName, typeof(T));

            if (source == null)
            {
                return dataTable;
            }

            foreach (var item in source)
            {
                var dr = dataTable.NewRow();
                dr[columnName] = item;

                dataTable.Rows.Add(dr);
            }

            return dataTable;
        }
}
