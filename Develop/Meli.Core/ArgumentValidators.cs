namespace Meli.Core;
using Meli.Core.Attributes;
public static class ArgumentValidators
{
    /// <summary>
        /// Throw argument null exception if value is null.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <param name="parameterName">The parameter.</param>
        public static void ThrowIfNull([ValidatedNotNull] object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throw argument null exception if value is null or empty.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <param name="parameterName">The parameter.</param>
        public static void ThrowIfNullOrEmpty([ValidatedNotNull] string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throw argument null exception if value is null or empty.
        /// </summary>
        /// <param name="enumerable">The value to be validated.</param>
        /// <param name="parameterName">The parameter.</param>
        /// <typeparam name="T">The generic type of enumerable.</typeparam>
        /// <returns>The enumerated list.</returns>
        public static IList<T> ThrowIfNullOrEmpty<T>([ValidatedNotNull] this IEnumerable<T> enumerable, string parameterName)
        {
            var list = enumerable?.ToList();
            if (list == null || !list.Any())
            {
                throw new ArgumentNullException(parameterName);
            }

            return list;
        }
}