using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluiTec.AppFx.Data.Sql.Attributes;

namespace FluiTec.AppFx.Data.Sql
{
    /// <summary>	A SQL entity cache. </summary>
    public static class SqlCache
    {
        /// <summary>	The type properties. </summary>
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IList<PropertyInfo>> TypeProperties =
            new();

        /// <summary>	The type key properties. </summary>
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IList<PropertyInfo>> TypeKeyProperties =
            new();

        /// <summary>	The entity name cache. </summary>
        public static ConcurrentDictionary<RuntimeTypeHandle, string> EntityNameCache =
            new();

        /// <summary>	Type properties chache. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	A list of. </returns>
        public static IList<PropertyInfo> TypePropertiesChache(Type type)
        {
            if (TypeProperties.TryGetValue(type.TypeHandle, out var propertyInfos))
                return propertyInfos;

            var allProperties = type.GetProperties();

            var properties = new List<PropertyInfo>();
            foreach (var property in allProperties)
                if (!property.GetCustomAttributes(typeof(SqlIgnoreAttribute)).Any())
                    properties.Add(property);

            TypeProperties[type.TypeHandle] = properties.ToList();
            return properties;
        }

        /// <summary>	Type key properties cache. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	A list of. </returns>
        public static IList<PropertyInfo> TypeKeyPropertiesCache(Type type)
        {
            if (TypeKeyProperties.TryGetValue(type.TypeHandle, out var propertyInfos))
                return propertyInfos;

            var allProperties = TypePropertiesChache(type);
            var keyProperties = allProperties.Where(p => p.Name.ToLower() == "id").ToList();
            TypeKeyProperties[type.TypeHandle] = keyProperties;
            return keyProperties;
        }
    }
}