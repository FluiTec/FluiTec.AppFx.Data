using FluentMigrator.Runner.VersionTableInfo;

namespace FluiTec.AppFx.Data.Dapper.Migration
{
    /// <summary>   A version table. </summary>
    [VersionTableMetaData]
    public class VersionTable : IVersionTableMetaData
    {
        /// <summary>   Default constructor. </summary>
        public VersionTable(string schema)
        {
            SchemaName = schema;
        }

        /// <summary>	Name of the column. </summary>
        public string ColumnName => "Version";

        /// <summary>	True to owns schema. </summary>
        public bool OwnsSchema => true;

        /// <summary>	Name of the schema. </summary>
        public string SchemaName {get; }

        /// <summary>	Name of the table. </summary>
        public string TableName => "Migrations";

        /// <summary>	Name of the unique index. </summary>
        public string UniqueIndexName => "UC_Version";

        /// <summary>	Name of the applied on column. </summary>
        public virtual string AppliedOnColumnName => "AppliedOn";

        /// <summary>	Name of the description column. </summary>
        public virtual string DescriptionColumnName => "Description";

        /// <summary>	Gets or sets a context for the application. </summary>
        /// <value>	The application context. </value>
        public object ApplicationContext { get; set; }
    }
}
