﻿using FluiTec.AppFx.Data.Dapper.Migration;

namespace FluiTec.AppFx.Data.TestLibrary.Schema.Migrations
{
    /// <summary>   A dummy migration. </summary>
    [DapperMigration(2020, 10, 28, 15, 12, "Achim Schnell")]
    public class DummyMigration : FluentMigrator.Migration
    {
        /// <summary>   Collect the UP migration expressions. </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.DummyTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().Nullable();

            IfDatabase(MigrationDatabaseName.Mysql)
                .Create
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.DummyTable}")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().Nullable();
        }

        /// <summary>   Collects the DOWN migration expressions. </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.DummyTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase(MigrationDatabaseName.Mysql)
                .Delete
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.DummyTable}");
        }
    }
}