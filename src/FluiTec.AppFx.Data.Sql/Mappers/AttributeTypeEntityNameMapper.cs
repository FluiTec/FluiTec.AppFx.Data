using System;
using System.Linq;
using System.Reflection;
using FluiTec.AppFx.Data.EntityNameServices;

namespace FluiTec.AppFx.Data.Sql.Mappers
{
    /// <summary>	An attribute type entity name mapper. </summary>
    public class AttributeTypeEntityNameMapper : TypeEntityNameMapper
    {
        /// <summary>	Gets a name. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	The name. </returns>
        public override string GetName(Type type)
        {
            if (SqlCache.EntityNameCache.TryGetValue(type.TypeHandle, out var name))
                return name;

            var entityName = type
                .GetTypeInfo()
                .GetCustomAttributes(typeof(EntityNameAttribute))
                .SingleOrDefault() is EntityNameAttribute attribute
                ? attribute.Name
                : base.GetName(type);
            SqlCache.EntityNameCache.TryAdd(type.TypeHandle, entityName);
            return entityName;
        }
    }
}