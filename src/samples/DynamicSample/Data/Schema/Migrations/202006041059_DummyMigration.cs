using FluentMigrator;
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
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.DummyTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().Nullable();
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
