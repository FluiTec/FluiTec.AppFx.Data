using System.Reflection;
using System.Text;
using FluiTec.AppFx.Data.Sql.Mappers;

namespace FluiTec.AppFx.Data.Sql.Adapters
{
    /// <summary>	A postgre SQL adapter. </summary>
    public class PostgreSqlAdapter : SqlAdapter
    {
        /// <summary>	Default constructor. </summary>
        public PostgreSqlAdapter()
        {
        }

        /// <summary>	Constructor. </summary>
        /// <param name="entityNameMapper">	The entity name mapper. </param>
        public PostgreSqlAdapter(IEntityNameMapper entityNameMapper) : base(entityNameMapper)
        {
        }

        /// <summary>	Gets automatic key statement. </summary>
        /// <param name="propertyInfo">	Information describing the property. </param>
        /// <returns>	The automatic key statement. </returns>
        public override string GetAutoKeyStatement(PropertyInfo propertyInfo)
        {
            return $" RETURNING {RenderPropertyName(propertyInfo)}";
        }

        /// <summary>	Renders the table name described by tableName. </summary>
        /// <param name="tableName">	Name of the table. </param>
        /// <returns>	A string. </returns>
        public override string RenderTableName(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                return base.RenderTableName(tableName);
            if (!tableName.Contains("."))
                return $"\"public\".\"{tableName}\"";
            var sb = new StringBuilder();
            var split = tableName.Split('.');
            for (var i = 0; i < split.Length; i++)
            {
                if (i != 0)
                    sb.Append('.');
                sb.Append($"\"{split[i]}\"");
            }

            return sb.ToString();
        }

        /// <summary>	Renders the property name described by propertyInfo. </summary>
        /// <param name="propertyInfo">	Information describing the property. </param>
        /// <returns>	A string. </returns>
        public override string RenderPropertyName(PropertyInfo propertyInfo)
        {
            return RenderPropertyName(propertyInfo.Name);
        }

        /// <summary>Renders the property name described by propertyName.</summary>
        /// <param name="propertyName"> Name of the property. </param>
        /// <returns>A string.</returns>
        public override string RenderPropertyName(string propertyName)
        {
            return $"\"{propertyName}\"";
        }
    }
}