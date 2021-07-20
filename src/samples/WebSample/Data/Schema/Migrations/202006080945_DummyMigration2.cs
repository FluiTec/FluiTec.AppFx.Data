using FluentMigrator;
using FluiTec.AppFx.Data.Dapper.Extensions;
using FluiTec.AppFx.Data.Dapper.Migration;

namespace WebSample.Data.Schema.Migrations
{
    /// <summary>   A dummy2 migration. </summary>
    [DapperMigration(2020, 06, 08, 9, 45, "Achim Schnell")]
    public class DummyMigration2 : Migration
    {
        /// <summary>   Collect the UP migration expressions. </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.DummyTable2, true)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("TimeStamp").AsDateTimeOffset().NotNullable();

            IfDatabase(MigrationDatabaseName.Mysql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.DummyTable2, true)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("TimeStamp").AsDateTime().NotNullable();
        }

        /// <summary>   Collects the DOWN migration expressions. </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.DummyTable2, true);

            IfDatabase(MigrationDatabaseName.Mysql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.DummyTable2, true);
        }
    }
}