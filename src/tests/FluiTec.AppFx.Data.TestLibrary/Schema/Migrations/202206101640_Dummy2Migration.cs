using FluiTec.AppFx.Data.Dapper.Extensions;
using FluiTec.AppFx.Data.Migration;

namespace FluiTec.AppFx.Data.TestLibrary.Schema.Migrations
{
    /// <summary> A dummy 2 migration.</summary>
    [ExtendedMigration(2022,06,10,16,40, "Achim Schnell")]
    public class Dummy2Migration : FluentMigrator.Migration
    {
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.Dummy2Table, true)
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().Nullable();

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.Dummy2Table, false)
                .WithColumn("Id").AsCustom("CHAR(36)").NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().Nullable();
        }

        /// <summary> Collects the DOWN migration expressions.</summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.Dummy2Table, true);

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.Dummy2Table, false);
        }
    }
}