using System.Reflection;
using FluiTec.AppFx.Data.Sql.Mappers;

namespace FluiTec.AppFx.Data.Sql.Adapters
{
    public class SqLiteAdapter : SqlAdapter
    {
        /// <summary>	Default constructor. </summary>
        public SqLiteAdapter()
        {
        }

        /// <summary>	Constructor. </summary>
        /// <param name="entityNameMapper">	The entity name mapper. </param>
        public SqLiteAdapter(IEntityNameMapper entityNameMapper) : base(entityNameMapper)
        {
        }

        /// <summary>	Renders the table name described by tableName. </summary>
        /// <param name="tableName">	Name of the table. </param>
        /// <returns>	A string. </returns>
        public override string RenderTableName(string tableName)
        {
            return string.IsNullOrWhiteSpace(tableName)
                ? base.RenderTableName(tableName)
                : tableName.Replace('.', '_');
        }

        /// <summary>	Gets automatic key statement. </summary>
        /// <param name="propertyInfo">	Information describing the property. </param>
        /// <returns>	The automatic key statement. </returns>
        public override string GetAutoKeyStatement(PropertyInfo propertyInfo)
        {
            return $";SELECT last_insert_rowid() {RenderPropertyName(propertyInfo)}";
        }
    }
}