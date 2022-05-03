using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.Sql.Tests.Entities;
using ImmediateReflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Sql.Tests
{
    [TestClass]
    public class MssqlBuilderTest : SqlBuilderTest
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        ///
        /// <returns>
        /// The connection.
        /// </returns>
        protected override IDbConnection GetConnection() => new SqlConnection();

        [TestMethod]
        public void TestGetBuilder()
        {
            Assert.IsNotNull(Builder);
        }

        [TestMethod]
        public void TestRenderTableName()
        {
            var tableName = Builder.Adapter.RenderTableName(typeof(Dummy));
            Assert.AreEqual($"[dbo].[{new AttributeEntityNameService().Name(typeof(Dummy))}]", tableName);
        }


        [TestMethod]
        public void TestRenderPropertyName()
        {
            var type = typeof(Dummy);
            var propertyInfo = type.GetImmediateProperty(nameof(Dummy.Id));
            var propertyName = $"[{nameof(Dummy.Id)}]";
            var renderedPropertyName = Builder.Adapter.RenderPropertyName(propertyInfo);
            Assert.AreEqual(propertyName, renderedPropertyName);
        }

        [TestMethod]
        [DataRow("Test")]
        public void TestRenderPropertyNameByString(string name)
        {
            var propertyName = $"[{name}]";
            var renderedPropertyName = Builder.Adapter.RenderPropertyName(name);
            Assert.AreEqual(propertyName, renderedPropertyName);
        }

        [TestMethod]
        public void TestRenderPropertyList()
        {
            var properties = typeof(Dummy).GetImmediateProperties();
            var renderedList = Builder.Adapter.RenderPropertyList(properties.ToArray());
            Assert.AreEqual("[Id], [Name]", renderedList.ToString());
        }

        [TestMethod]
        public void TestRenderPropertyListWithTable()
        {
            var properties = typeof(Dummy).GetImmediateProperties();
            var renderedList = Builder.Adapter.RenderPropertyList(typeof(Dummy), properties.ToArray());
            Assert.AreEqual("[dbo].[Dummy].[Id], [dbo].[Dummy].[Name]", renderedList.ToString());
        }

        [TestMethod]
        public void SelectByFilterTest()
        {
            var sql = Builder.SelectByFilter(typeof(Dummy), nameof(Dummy.Name));
            Assert.AreEqual("SELECT [Id], [Name] FROM [dbo].[Dummy] WHERE [Name] = @Name", sql);
        }

        [TestMethod]
        public void SelectByInFilterTest()
        {
            var sql = Builder.SelectByInFilter(typeof(Dummy), nameof(Dummy.Id), "Ids");
            Assert.AreEqual("SELECT [Id], [Name] FROM [dbo].[Dummy] WHERE [Id] IN @Ids", sql);
        }

        [TestMethod]
        public void SelectByFilterMultiTest()
        {
            var sql1 = Builder.SelectByFilter(typeof(Dummy), new[] {nameof(Dummy.Name)});
            Assert.AreEqual("SELECT [Id], [Name] FROM [dbo].[Dummy] WHERE [Name] = @Name", sql1);

            var sql2 = Builder
                .SelectByFilter(typeof(Dummy), new[] {nameof(Dummy.Id), nameof(Dummy.Name)});
            Assert.AreEqual("SELECT [Id], [Name] FROM [dbo].[Dummy] WHERE [Id] = @Id AND [Name] = @Name", sql2);
        }

        [TestMethod]
        public void TestSelectAll()
        {
            var sql = Builder.SelectAll(typeof(Dummy));
            Assert.AreEqual($"SELECT [Id], [Name] FROM [dbo].[{nameof(Dummy)}]", sql);
        }

        [TestMethod]
        public void TestSelectByKey()
        {
            var sql = Builder.SelectByKey(typeof(Dummy));
            Assert.AreEqual($"SELECT [Id], [Name] FROM [dbo].[{nameof(Dummy)}] WHERE [Id] = @Id", sql);
        }

        [TestMethod]
        public void TestSelectCustom()
        {
            var sql = Builder.SelectByFilter(typeof(Dummy), nameof(Dummy.Id));
            Assert.AreEqual($"SELECT [Id], [Name] FROM [dbo].[{nameof(Dummy)}] WHERE [Id] = @Id", sql);
        }

        [TestMethod]
        public void TestInsertAutoKey()
        {
            var sql = Builder.InsertAutoKey(typeof(Dummy));
            Assert.AreEqual(
                $"INSERT INTO [dbo].[{nameof(Dummy)}] ([Name]) VALUES (@Name);SELECT SCOPE_IDENTITY() [{nameof(Dummy.Id)}]",
                sql);
        }

        [TestMethod]
        public void TestInsertMultiple()
        {
            var sql = Builder.InsertAutoMultiple(typeof(Dummy));
            Assert.AreEqual($"INSERT INTO [dbo].[{nameof(Dummy)}] ([Name]) VALUES (@Name)", sql);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var sql = Builder.Update(typeof(Dummy));
            Assert.AreEqual($"UPDATE [dbo].[{nameof(Dummy)}] SET [Name] = @Name WHERE [Id] = @Id", sql);
        }

        [TestMethod]
        public void TestDelete()
        {
            var sql = Builder.Delete(typeof(Dummy));
            Assert.AreEqual($"DELETE FROM [dbo].[{nameof(Dummy)}] WHERE [Id] = @Id", sql);
        }

        [TestMethod]
        public void TestDeleteBy()
        {
            var sql = Builder.DeleteBy(typeof(Dummy), nameof(Dummy.Name));
            Assert.AreEqual($"DELETE FROM [dbo].[{nameof(Dummy)}] WHERE [Name] = @Name", sql);
        }
    }
}