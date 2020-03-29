using System;

namespace FluiTec.AppFx.Data.Sql.Mappers
{
    /// <summary>	Interface for entity name mapper. </summary>
    public interface IEntityNameMapper
    {
        /// <summary>	Gets a name. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	The name. </returns>
        string GetName(Type type);
    }
}