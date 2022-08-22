using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

    /// <summary>
    /// Converts the IEnumerable of type string to data table.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="columnName">The columnName.</param>
    /// <param name="tableName">The tableName.</param>
    /// <returns>The data table.</returns>
    public static DataTable ToDataTable(this IEnumerable<string> source, string columnName, string tableName)
    {
        var dataTable = new DataTable(tableName) { Locale = CultureInfo.InvariantCulture };
        dataTable.Columns.Add(columnName, typeof(string));

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

    /// <summary>
    /// Converts the IEnumerable of type object to data table.
    /// </summary>
    /// <typeparam name="T">The type of collection.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="tableName">Name of the table.</param>
    /// <returns>
    /// The data table.
    /// </returns>
    public static DataTable ToDataTable<T>(this IEnumerable<T> source, string tableName)
       where T : class
    {
        var isDisplayNameAvailable = typeof(T).GetCustomAttributes().Any(a => string.Equals(a.GetType().Name, nameof(DisplayNameAttribute), StringComparison.Ordinal));
        string dataTableName = string.IsNullOrWhiteSpace(tableName) ? typeof(T).Name : tableName;
        dataTableName = isDisplayNameAvailable ? GetTableDisplayName<T>() : dataTableName;

        var dataTable = new DataTable(dataTableName) { Locale = CultureInfo.InvariantCulture };
        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var propsinfo = props.Where(p => !p.GetCustomAttributes().Any(a => string.Equals(a.GetType().Name, "ColumnIgnoreAttribute", StringComparison.Ordinal)));

        GenerateDataColumns(isDisplayNameAvailable, dataTable, propsinfo);

        if (source != null)
        {
            GenerateDataRows(source, isDisplayNameAvailable, dataTable, propsinfo);
        }

        return dataTable;
    }

    private static void GenerateDataRows<T>(IEnumerable<T> source, bool isDisplayNameAvailable, DataTable dataTable, IEnumerable<PropertyInfo> propsinfo)
    where T : class
    {
        foreach (var item in source)
        {
            var dr = dataTable.NewRow();
            foreach (DataColumn dataTableColumn in dataTable.Columns)
            {
                PropertyInfo propertyInfo = isDisplayNameAvailable
                    ? propsinfo.Single(p => p.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>()
                    .FirstOrDefault()!.DisplayName.Equals(dataTableColumn.ColumnName, StringComparison.OrdinalIgnoreCase))
                    : propsinfo.Single(p => p.Name.Equals(dataTableColumn.ColumnName, StringComparison.OrdinalIgnoreCase));
                var value = Convert.ToBoolean(Nullable.GetUnderlyingType(propertyInfo.PropertyType)?.IsEnum, CultureInfo.InvariantCulture)
                        ? (int)propertyInfo.GetValue(item)!
                        : propertyInfo.GetValue(item) ?? DBNull.Value;
                dr[dataTableColumn] = value;                        
            }

            dataTable.Rows.Add(dr);
        }
    }

    private static void GenerateDataColumns(bool isDisplayNameAvailable, DataTable dataTable, IEnumerable<PropertyInfo> propsinfo)
    {
        foreach (var prop in propsinfo)
        {
            string propName = string.Empty;
            propName = isDisplayNameAvailable ? GetPropertyDisplayName(prop) : prop.Name;
            dataTable.Columns.Add(propName, GetType(prop));
        }
    }

    private static string GetTableDisplayName<T>()
           where T : class
    {
        return typeof(T).GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().FirstOrDefault()!.DisplayName;
    }

    private static string GetPropertyDisplayName(PropertyInfo propertyInfo)
    {
        return propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().FirstOrDefault()!.DisplayName;
    }

    /// <summary>
    /// Gets the prop type.
    /// </summary>
    /// <param name="prop">The prop.</param>
    /// <returns>The type.</returns>
    private static Type GetType(PropertyInfo prop)
    {
        return Convert.ToBoolean(Nullable.GetUnderlyingType(prop.PropertyType)?.IsEnum, CultureInfo.InvariantCulture)
            ? typeof(int)
            : Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
    }
}
