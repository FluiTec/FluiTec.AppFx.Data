using System;

namespace FluiTec.AppFx.Data.Sql.Mappers
{
    /// <summary>	A type entity name mapper. </summary>
    public class TypeEntityNameMapper : IEntityNameMapper
    {
        /// <summary>	Gets a name. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	The name. </returns>
        public virtual string GetName(Type type)
        {
            if (SqlCache.EntityNameCache.TryGetValue(type.TypeHandle, out var name))
                return name;

            var entityName = type.Name;
            SqlCache.EntityNameCache.TryAdd(type.TypeHandle, entityName);
            return entityName;
        }
    }
}