using FluiTec.AppFx.Data.Dapper.Extensions;
using FluiTec.AppFx.Data.Migration;

namespace FluiTec.AppFx.Data.TestLibrary.Schema.Migrations
{
    /// <summary>
    /// A date time dummy migration.
    /// </summary>
    [ExtendedMigration(2021, 10, 08, 07, 46, "Achim Schnell")]
    public class DateTimeDummyMigration : FluentMigrator.Migration
    {
        /// <summary>
        /// Collect the UP migration expressions.
        /// </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.DateTimeDummyTable, true)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("ChangeDate").AsDateTimeOffset().NotNullable();

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.DateTimeDummyTable, false)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("ChangeDate").AsDateTime().NotNullable();
        }

        /// <summary>
        /// Collects the DOWN migration expressions.
        /// </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.DateTimeDummyTable, true);

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.DateTimeDummyTable, false);
        }
    }
}