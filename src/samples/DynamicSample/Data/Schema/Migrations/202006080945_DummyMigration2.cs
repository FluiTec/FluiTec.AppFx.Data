using FluentMigrator;
using FluiTec.AppFx.Data.Dapper.Migration;

namespace DynamicSample.Data.Schema.Migrations
{
    /// <summary>   A dummy2 migration. </summary>
    [DapperMigration(2020, 06, 08, 9, 45, "Achim Schnell")]
    public class DummyMigration2 : Migration
    {
        /// <summary>   Collect the UP migration expressions. </summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.DummyTable2)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("TimeStamp").AsDateTimeOffset().NotNullable();
        }

        /// <summary>   Collects the DOWN migration expressions. </summary>
        public override void Down()
        {
            IfDatabase("sqlserver", "postgres")
                .Delete
                .Table(SchemaGlobals.DummyTable)
                .InSchema(SchemaGlobals.Schema);
        }
    }
}