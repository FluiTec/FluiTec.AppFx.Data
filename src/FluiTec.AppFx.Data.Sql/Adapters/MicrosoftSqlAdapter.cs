using System.Reflection;
using System.Text;
using FluiTec.AppFx.Data.Sql.Mappers;

namespace FluiTec.AppFx.Data.Sql.Adapters
{
    /// <summary>	A microsoft SQL adapter. </summary>
    public class MicrosoftSqlAdapter : SqlAdapter
    {
        /// <summary>	Default constructor. </summary>
        public MicrosoftSqlAdapter()
        {
        }

        /// <summary>	Constructor. </summary>
        /// <param name="entityNameMapper">	The entity name mapper. </param>
        public MicrosoftSqlAdapter(IEntityNameMapper entityNameMapper) : base(entityNameMapper)
        {
        }

        /// <summary>	Renders the table name described by tableName. </summary>
        /// <param name="tableName">	Name of the table. </param>
        /// <returns>	A string. </returns>
        public override string RenderTableName(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                return base.RenderTableName(tableName);
            if (!tableName.Contains("."))
                return $"[dbo].[{tableName}]";
            var sb = new StringBuilder();
            var split = tableName.Split('.');
            for (var i = 0; i < split.Length; i++)
            {
                if (i != 0)
                    sb.Append('.');
                sb.Append($"[{split[i]}]");
            }

            return sb.ToString();
        }

        /// <summary>	Gets automatic key statement. </summary>
        /// <param name="propertyInfo">	Information describing the property. </param>
        /// <returns>	The automatic key statement. </returns>
        public override string GetAutoKeyStatement(PropertyInfo propertyInfo)
        {
            return $";SELECT SCOPE_IDENTITY() {RenderPropertyName(propertyInfo)}";
        }

        public override string RenderPropertyName(PropertyInfo propertyInfo)
        {
            return $"[{propertyInfo.Name}]";
        }
    }
}