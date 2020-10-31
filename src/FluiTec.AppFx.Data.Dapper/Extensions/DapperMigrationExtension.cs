using System;
using FluentMigrator.Builders.Create;
using FluentMigrator.Builders.Create.Table;
using FluentMigrator.Builders.Delete;

namespace FluiTec.AppFx.Data.Dapper.Extensions
{
    /// <summary>   A dapper migration extension.</summary>
    public static class DapperMigrationExtension
    {
        /// <summary>   An ICreateExpressionRoot extension method that tables.</summary>
        /// <exception cref="ArgumentOutOfRangeException">  Thrown when one or more arguments are outside
        ///                                                 the required range. </exception>
        /// <param name="expressionRoot">   The expressionRoot to act on. </param>
        /// <param name="tableName">        Name of the table. </param>
        /// <param name="schemaName">       Name of the schema. </param>
        /// <param name="supportsSchema">   True to supports schema. </param>
        /// <returns>   An ICreateTableWithColumnSyntax.</returns>
        public static ICreateTableWithColumnSyntax Table(this ICreateExpressionRoot expressionRoot, string schemaName, string tableName, bool supportsSchema)
        {
            return supportsSchema ? expressionRoot.Table(tableName).InSchema(schemaName) : expressionRoot.Table($"{schemaName}_{tableName}");
        }

        /// <summary>   An ICreateExpressionRoot extension method that tables.</summary>
        /// <param name="expressionRoot">   The expressionRoot to act on. </param>
        /// <param name="tableName">        Name of the table. </param>
        /// <param name="schemaName">       Name of the schema. </param>
        /// <param name="supportsSchema">   True to supports schema. </param>
        public static void Table(this IDeleteExpressionRoot expressionRoot,
            string schemaName, string tableName, bool supportsSchema)
        {
            if (supportsSchema)
                expressionRoot
                    .Table(tableName)
                    .InSchema(schemaName);
            else
                expressionRoot.Table($"{schemaName}_{tableName}");
        }
    }
}