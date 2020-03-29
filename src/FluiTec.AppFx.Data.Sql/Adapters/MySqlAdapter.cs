﻿using System.Reflection;
using FluiTec.AppFx.Data.EntityNameServices;

namespace FluiTec.AppFx.Data.Sql.Adapters
{
    /// <summary>	a MySql adapter. </summary>
    public class MySqlAdapter : SqlAdapter
    {
        /// <summary>	Constructor. </summary>
        /// <param name="entityNameService">	The entity name service. </param>
        public MySqlAdapter(IEntityNameService entityNameService) : base(entityNameService)
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
            return $";SELECT LAST_INSERT_ID() {RenderPropertyName(propertyInfo)}";
        }
    }
}