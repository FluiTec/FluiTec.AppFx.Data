using System.Collections.Generic;
using System.Linq;

namespace FluiTec.AppFx.Data.Dynamic.Extensions
{
    /// <summary>
    ///     A dictionary extensions.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     A Dictionary&lt;TKey,TValue&gt; extension method that searches for the first value.
        /// </summary>
        /// <typeparam name="TKey">     Type of the key. </typeparam>
        /// <typeparam name="TValue">   Type of the value. </typeparam>
        /// <param name="dictionary">   The dictionary to act on. </param>
        /// <param name="value">        The value. </param>
        /// <returns>
        ///     The zero-based index of the found value, or -1 if no match was found.
        /// </returns>
        // ReSharper disable once UnusedMember.Global
        public static int IndexOfValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TValue value)
        {
            for (var i = 0; i < dictionary.Count; ++i)
                if (dictionary.ElementAt(i).Value.Equals(value))
                    return i;
            return -1;
        }

        /// <summary>
        ///     A Dictionary&lt;TKey,TValue&gt; extension method that searches for the first key.
        /// </summary>
        /// <typeparam name="TKey">     Type of the key. </typeparam>
        /// <typeparam name="TValue">   Type of the value. </typeparam>
        /// <param name="dictionary">   The dictionary to act on. </param>
        /// <param name="key">          The key. </param>
        /// <returns>
        ///     The zero-based index of the found key, or -1 if no match was found.
        /// </returns>
        public static int IndexOfKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            for (var i = 0; i < dictionary.Count; ++i)
                if (dictionary.ElementAt(i).Key.Equals(key))
                    return i;
            return -1;
        }
    }
}