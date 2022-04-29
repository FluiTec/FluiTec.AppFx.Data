using System;
using System.Text;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.Sql.Adapters;
using FluiTec.AppFx.Data.Sql.Tests.Entities;
using ImmediateReflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Sql.Tests.Adapters
{
    [TestClass]
    public class PostgreSqlAdapterTest : SqlAdapterTest
    {
        /// <summary>
        ///     Default constructor.
        /// </summary>
        public PostgreSqlAdapterTest() : base(new PostgreSqlAdapter(new AttributeEntityNameService()))
        {
        }

        /// <summary>
        ///     Gets expected key parameters.
        /// </summary>
        /// <param name="entityType">   Type of the entity. </param>
        /// <returns>
        ///     The expected key parameters.
        /// </returns>
        protected override string GetExpectedKeyParameters(Type entityType)
        {
            if (entityType == typeof(Dummy)) return "@Id";
            if (entityType == typeof(RenamedDummy)) return "@UId";
            if (entityType == typeof(MultiKeyDummy)) return "@Id, @MyKey";
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets expected select all statement.
        /// </summary>
        /// <param name="entityType">   Type of the entity. </param>
        /// <returns>
        ///     The expected select all statement.
        /// </returns>
        protected override string GetExpectedSelectAllStatement(Type entityType)
        {
            if (entityType == typeof(Dummy)) return "SELECT \"Id\", \"Name\" FROM \"public\".\"Dummy\"";
            if (entityType == typeof(RenamedDummy)) return "SELECT \"UId\", \"Name\" FROM \"public\".\"RenamedDummy\"";
            if (entityType == typeof(MultiKeyDummy))
                return "SELECT \"Id\", \"MyKey\" FROM \"public\".\"MultiKeyDummy\"";
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets expected by key statement.
        /// </summary>
        /// <param name="entityType">   Type of the entity. </param>
        /// <returns>
        ///     The expected by key statement.
        /// </returns>
        protected override string GetExpectedByKeyStatement(Type entityType)
        {
            if (entityType == typeof(Dummy))
                return "SELECT \"Id\", \"Name\" FROM \"public\".\"Dummy\" WHERE \"Id\" = @Id";
            if (entityType == typeof(RenamedDummy))
                return "SELECT \"UId\", \"Name\" FROM \"public\".\"RenamedDummy\" WHERE \"UId\" = @UId";
            if (entityType == typeof(MultiKeyDummy))
                return
                    "SELECT \"Id\", \"MyKey\" FROM \"public\".\"MultiKeyDummy\" WHERE \"Id\" = @Id AND \"MyKey\" = @MyKey";
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets expected by filter statement.
        /// </summary>
        /// <param name="entityType">       Type of the entity. </param>
        /// <param name="filterProperty">   The filter property. </param>
        /// <param name="selectProperties"> The select properties. </param>
        /// <returns>
        ///     The expected by filter statement.
        /// </returns>
        protected override string GetExpectedByFilterStatement(Type entityType, string filterProperty,
            string[] selectProperties)
        {
            if (entityType == typeof(Dummy))
                return "SELECT \"Id\", \"Name\" FROM \"public\".\"Dummy\" WHERE \"Name\" = @Name";
            if (entityType == typeof(RenamedDummy))
                return "SELECT \"UId\", \"Name\" FROM \"public\".\"RenamedDummy\" WHERE \"Name\" = @Name";
            if (entityType == typeof(MultiKeyDummy))
                return "SELECT \"Id\", \"MyKey\" FROM \"public\".\"MultiKeyDummy\" WHERE \"MyKey\" = @MyKey";
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets expected by filter in statement.
        /// </summary>
        /// <param name="entityType">       Type of the entity. </param>
        /// <param name="filterProperty">   The filter property. </param>
        /// <param name="colName">          Name of the col. </param>
        /// <param name="selectFields">     The select fields. </param>
        /// <returns>
        ///     The expected by filter in statement.
        /// </returns>
        protected override string GetExpectedByFilterInStatement(Type entityType, string filterProperty, string colName,
            string[] selectFields)
        {
            if (entityType == typeof(Dummy))
                return "SELECT \"Id\", \"Name\" FROM \"public\".\"Dummy\" WHERE \"Name\" = ANY(@Names)";
            if (entityType == typeof(RenamedDummy))
                return "SELECT \"UId\", \"Name\" FROM \"public\".\"RenamedDummy\" WHERE \"Name\" = ANY(@Names)";
            if (entityType == typeof(MultiKeyDummy))
                return "SELECT \"Id\", \"MyKey\" FROM \"public\".\"MultiKeyDummy\" WHERE \"MyKey\" = ANY(@Keys)";
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets expected by filter statement.
        /// </summary>
        /// <param name="entityType">   Type of the entity. </param>
        /// <param name="properties">   The properties. </param>
        /// <returns>
        ///     The expected by filter statement.
        /// </returns>
        protected override string GetExpectedByFilterStatement(Type entityType, string[] properties)
        {
            if (entityType == typeof(Dummy))
                return "SELECT \"Id\", \"Name\" FROM \"public\".\"Dummy\" WHERE \"Id\" = @Id AND \"Name\" = @Name";
            if (entityType == typeof(RenamedDummy))
                return
                    "SELECT \"UId\", \"Name\" FROM \"public\".\"RenamedDummy\" WHERE \"UId\" = @UId AND \"Name\" = @Name";
            if (entityType == typeof(MultiKeyDummy))
                return
                    "SELECT \"Id\", \"MyKey\" FROM \"public\".\"MultiKeyDummy\" WHERE \"Id\" = @Id AND \"MyKey\" = @MyKey";
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets expected insert statement.
        /// </summary>
        /// <param name="entityType">   Type of the entity. </param>
        /// <returns>
        ///     The expected insert statement.
        /// </returns>
        protected override string GetExpectedInsertStatement(Type entityType)
        {
            if (entityType == typeof(Dummy))
                return "INSERT INTO \"public\".\"Dummy\" (\"Id\", \"Name\") VALUES (@Id, @Name)";
            if (entityType == typeof(RenamedDummy))
                return "INSERT INTO \"public\".\"RenamedDummy\" (\"UId\", \"Name\") VALUES (@UId, @Name)";
            if (entityType == typeof(MultiKeyDummy))
                return "INSERT INTO \"public\".\"MultiKeyDummy\" (\"Id\", \"MyKey\") VALUES (@Id, @MyKey)";
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets expected insert automatic key statement.
        /// </summary>
        /// <param name="entityType">   Type of the entity. </param>
        /// <returns>
        ///     The expected insert automatic key statement.
        /// </returns>
        protected override string GetExpectedInsertAutoKeyStatement(Type entityType)
        {
            if (entityType == typeof(Dummy))
                return "INSERT INTO \"public\".\"Dummy\" (\"Name\") VALUES (@Name) RETURNING \"Id\"";
            if (entityType == typeof(RenamedDummy))
                return "INSERT INTO \"public\".\"RenamedDummy\" (\"Name\") VALUES (@Name) RETURNING \"UId\"";
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets expected insert multiple statement.
        /// </summary>
        /// <param name="entityType">   Type of the entity. </param>
        /// <returns>
        ///     The expected insert multiple statement.
        /// </returns>
        protected override string GetExpectedInsertMultipleStatement(Type entityType)
        {
            if (entityType == typeof(Dummy)) return "INSERT INTO \"public\".\"Dummy\" (\"Name\") VALUES (@Name)";
            if (entityType == typeof(RenamedDummy))
                return "INSERT INTO \"public\".\"RenamedDummy\" (\"Name\") VALUES (@Name)";
            if (entityType == typeof(MultiKeyDummy))
                return "INSERT INTO \"public\".\"MultiKeyDummy\" (\"Id\", \"MyKey\") VALUES (@Id, @MyKey)";
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets expected insert automatic key multiple statement.
        /// </summary>
        /// <param name="entityType">   Type of the entity. </param>
        /// <returns>
        ///     The expected insert automatic key multiple statement.
        /// </returns>
        protected override string GetExpectedInsertAutoKeyMultipleStatement(Type entityType)
        {
            if (entityType == typeof(Dummy)) return "INSERT INTO \"public\".\"Dummy\" (\"Name\") VALUES (@Name)";
            if (entityType == typeof(RenamedDummy))
                return "INSERT INTO \"public\".\"RenamedDummy\" (\"Name\") VALUES (@Name)";
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets expected update statement.
        /// </summary>
        /// <param name="entityType">   Type of the entity. </param>
        /// <returns>
        ///     The expected update statement.
        /// </returns>
        protected override string GetExpectedUpdateStatement(Type entityType)
        {
            if (entityType == typeof(Dummy))
                return "UPDATE \"public\".\"Dummy\" SET \"Name\" = @Name WHERE \"Id\" = @Id";
            if (entityType == typeof(RenamedDummy))
                return "UPDATE \"public\".\"RenamedDummy\" SET \"Name\" = @Name WHERE \"UId\" = @UId";
            if (entityType == typeof(MultiKeyDummy))
                return
                    "UPDATE \"public\".\"MultiKeyDummy\" SET \"Id\" = @Id, \"MyKey\" = @MyKey WHERE \"Id\" = @Id AND \"MyKey\" = @MyKey";
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets expected delete statement.
        /// </summary>
        /// <param name="entityType">   Type of the entity. </param>
        /// <returns>
        ///     The expected delete statement.
        /// </returns>
        protected override string GetExpectedDeleteStatement(Type entityType)
        {
            if (entityType == typeof(Dummy)) return "DELETE FROM \"public\".\"Dummy\" WHERE \"Id\" = @Id";
            if (entityType == typeof(RenamedDummy))
                return "DELETE FROM \"public\".\"RenamedDummy\" WHERE \"UId\" = @UId";
            if (entityType == typeof(MultiKeyDummy))
                return "DELETE FROM \"public\".\"MultiKeyDummy\" WHERE \"Id\" = @Id AND \"MyKey\" = @MyKey";
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets expected delete by statement.
        /// </summary>
        /// <param name="entityType">       Type of the entity. </param>
        /// <param name="filterProperty">   The filter property. </param>
        /// <returns>
        ///     The expected delete by statement.
        /// </returns>
        protected override string GetExpectedDeleteByStatement(Type entityType, string filterProperty)
        {
            if (entityType == typeof(Dummy)) return "DELETE FROM \"public\".\"Dummy\" WHERE \"Name\" = @Name";
            if (entityType == typeof(RenamedDummy))
                return "DELETE FROM \"public\".\"RenamedDummy\" WHERE \"Name\" = @Name";
            if (entityType == typeof(MultiKeyDummy))
                return "DELETE FROM \"public\".\"MultiKeyDummy\" WHERE \"MyKey\" = @MyKey";
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Renders the expected property list described by props.
        /// </summary>
        /// <param name="props">        The properties. </param>
        /// <param name="entityType">   Type of the entity. </param>
        /// <returns>
        ///     A string.
        /// </returns>
        protected override string RenderExpectedPropertyList(ImmediateProperty[] props, Type entityType = null)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < props.Length; i++)
            {
                if (i > 0)
                    sb.Append(", ");
                if (entityType != null)
                    sb.Append($"\"public\".\"{new AttributeEntityNameService().Name(entityType)}\".");
                sb.Append($"\"{props[i].Name}\"");
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Gets expected render table name.
        /// </summary>
        /// <param name="entityType">   Type of the entity. </param>
        /// <returns>
        ///     The expected render table name.
        /// </returns>
        protected override string GetExpectedRenderTableName(Type entityType)
        {
            if (entityType == typeof(Dummy)) return "\"public\".\"Dummy\"";
            if (entityType == typeof(RenamedDummy)) return "\"public\".\"RenamedDummy\"";
            if (entityType == typeof(MultiKeyDummy)) return "\"public\".\"MultiKeyDummy\"";
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets expected render property name.
        /// </summary>
        /// <param name="propName"> Name of the property. </param>
        /// <returns>
        ///     The expected render property name.
        /// </returns>
        protected override string GetExpectedRenderPropertyName(string propName)
        {
            return $"\"{propName}\"";
        }

        /// <summary>
        ///     Gets expected render parameter property name.
        /// </summary>
        /// <param name="pName">    The name. </param>
        /// <returns>
        ///     The expected render parameter property name.
        /// </returns>
        protected override string GetExpectedRenderParameterPropertyName(string pName)
        {
            return $"@{pName}";
        }
    }
}