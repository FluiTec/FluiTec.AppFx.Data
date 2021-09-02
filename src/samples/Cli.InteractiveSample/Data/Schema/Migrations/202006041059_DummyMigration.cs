using Cli.InteractiveSample.Data.Schema;
using FluentMigrator;
using FluiTec.AppFx.Data.Dapper.Extensions;
using FluiTec.AppFx.Data.Dapper.Migration;

namespace DynamicSample.Data.Schema.Migrations
{
    /// <summary>   A dummy migration. </summary>
    [DapperMigration(2020, 06, 04, 10, 59, "Achim Schnell")]
    public class DummyMigration : Migration
    {
        /// <summary>   Collect the UP migration expressions. </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.DummyTable, true)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().Nullable();

            IfDatabase(MigrationDatabaseName.Mysql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.DummyTable, true)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().Nullable();
        }

        /// <summary>   Collects the DOWN migration expressions. </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.DummyTable, true);

            IfDatabase(MigrationDatabaseName.Mysql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.DummyTable, true);
        }
    }
}