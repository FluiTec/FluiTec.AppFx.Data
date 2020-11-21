using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace FluiTec.AppFx.Data.EntityNameServices
{
    /// <summary>
    ///     EntityNameService using <see cref="EntityNameAttribute" /> with a fallback to
    ///     <see cref="ClassEntityNameService" />.
    /// </summary>
    /// <seealso cref="FluiTec.AppFx.Data.EntityNameServices.ClassEntityNameService" />
    public class AttributeEntityNameService : ClassEntityNameService
    {
        /// <summary>	Gets or sets a list of names of the entities. </summary>
        /// <value>	A list of names of the entities. </value>
        private static readonly ConcurrentDictionary<Type, string> EntityNames =
            new ConcurrentDictionary<Type, string>();

        /// <summary>Names the specified type.</summary>
        /// <param name="type">The type.</param>
        /// <returns>A name for the entityType.</returns>
        /// <exception cref="System.ArgumentNullException">type</exception>
        public override string Name(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (EntityNames.ContainsKey(type)) return EntityNames[type];

            EntityNames.TryAdd(type,
                type.GetTypeInfo().GetCustomAttributes(typeof(EntityNameAttribute)).SingleOrDefault() is
                    EntityNameAttribute attribute
                    ? attribute.Name
                    : base.Name(type));

            return EntityNames[type];
        }
    }
}