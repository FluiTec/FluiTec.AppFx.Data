using System.Data;
using FluiTec.AppFx.Data.EntityNameServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;

namespace FluiTec.AppFx.Data.Sql.Tests
{
    [TestClass]
    public class PgsqlBuilderTest
    {
        /// <summary>	The connection. </summary>
        private readonly IDbConnection _connection;

        /// <summary>	Default constructor. </summary>
        public PgsqlBuilderTest()
        {
            _connection = new NpgsqlConnection();
        }

        [TestMethod]
        public void TestGetBuilder()
        {
            // just test this doesnt throw
            _connection.GetBuilder();
        }

        [TestMethod]
        public void TestRenderTableName()
        {
            var tableName = _connection.GetBuilder().Adapter.RenderTableName(typeof(Dummy));
            var nameService = new AttributeEntityNameService();
            Assert.AreEqual($"\"public\".\"{nameService.Name(typeof(Dummy))}\"", tableName);
        }

        [TestMethod]
        public void TestRenderPropertyName()
        {
            var type = typeof(Dummy);
            var propertyInfo = type.GetProperty(nameof(Dummy.Id));
            var propertyName = $"\"{nameof(Dummy.Id)}\"";
            var renderedPropertyName = _connection.GetBuilder().Adapter.RenderPropertyName(propertyInfo);
            Assert.AreEqual(propertyName, renderedPropertyName);
        }

        [TestMethod]
        [DataRow("Test")]
        public void TestRenderPropertyNameByString(string name)
        {
            var propertyName = $"\"{name}\"";
            var renderedPropertyName = _connection.GetBuilder().Adapter.RenderPropertyName(name);
            Assert.AreEqual(propertyName, renderedPropertyName);
        }

        [TestMethod]
        public void TestRenderPropertyList()
        {
            var properties = typeof(Dummy).GetProperties();
            var renderedList = _connection.GetBuilder().Adapter.RenderPropertyList(properties);
            Assert.AreEqual("\"Id\", \"Name\"", renderedList.ToString());
        }

        [TestMethod]
        public void TestRenderPropertyListWithTable()
        {
            var properties = typeof(Dummy).GetProperties();
            var renderedList = _connection.GetBuilder().Adapter.RenderPropertyList(typeof(Dummy), properties);
            Assert.AreEqual("\"public\".\"Dummy\".\"Id\", \"public\".\"Dummy\".\"Name\"", renderedList.ToString());
        }

        [TestMethod]
        public void SelectByFilterTest()
        {
            var sql = _connection.GetBuilder().SelectByFilter(typeof(Dummy), nameof(Dummy.Name));
            Assert.AreEqual("SELECT \"Id\", \"Name\" FROM \"public\".\"Dummy\" WHERE \"Name\" = @Name", sql);
        }

        [TestMethod]
        public void SelectByInFilterTest()
        {
            var sql = _connection.GetBuilder().SelectByInFilter(typeof(Dummy), nameof(Dummy.Id), "Ids");
            Assert.AreEqual("SELECT \"Id\", \"Name\" FROM \"public\".\"Dummy\" WHERE \"Id\" = ANY(@Ids)", sql);
        }

        [TestMethod]
        public void SelectByFilterMultiTest()
        {
            var sql1 = _connection.GetBuilder().SelectByFilter(typeof(Dummy), new[] {nameof(Dummy.Name)});
            Assert.AreEqual("SELECT \"Id\", \"Name\" FROM \"public\".\"Dummy\" WHERE \"Name\" = @Name", sql1);

            var sql2 = _connection.GetBuilder()
                .SelectByFilter(typeof(Dummy), new[] {nameof(Dummy.Id), nameof(Dummy.Name)});
            Assert.AreEqual("SELECT \"Id\", \"Name\" FROM \"public\".\"Dummy\" WHERE \"Id\" = @Id AND \"Name\" = @Name",
                sql2);
        }

        [TestMethod]
        public void TestSelectAll()
        {
            var sql = _connection.GetBuilder().SelectAll(typeof(Dummy));
            Assert.AreEqual($"SELECT \"Id\", \"Name\" FROM \"public\".\"{nameof(Dummy)}\"", sql);
        }

        [TestMethod]
        public void TestSelectByKey()
        {
            var sql = _connection.GetBuilder().SelectByKey(typeof(Dummy));
            Assert.AreEqual($"SELECT \"Id\", \"Name\" FROM \"public\".\"{nameof(Dummy)}\" WHERE \"Id\" = @Id", sql);
        }

        [TestMethod]
        public void TestSelectCustom()
        {
            var sql = _connection.GetBuilder().SelectByFilter(typeof(Dummy), nameof(Dummy.Id));
            Assert.AreEqual($"SELECT \"Id\", \"Name\" FROM \"public\".\"{nameof(Dummy)}\" WHERE \"Id\" = @Id", sql);
        }

        [TestMethod]
        public void TestInsertAutoKey()
        {
            var sql = _connection.GetBuilder().InsertAutoKey(typeof(Dummy));
            Assert.AreEqual(
                $"INSERT INTO \"public\".\"{nameof(Dummy)}\" (\"Name\") VALUES (@Name) RETURNING \"{nameof(Dummy.Id)}\"",
                sql);
        }

        [TestMethod]
        public void TestInsertMultiple()
        {
            var sql = _connection.GetBuilder().InsertAutoMultiple(typeof(Dummy));
            Assert.AreEqual($"INSERT INTO \"public\".\"{nameof(Dummy)}\" (\"Name\") VALUES (@Name)", sql);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var sql = _connection.GetBuilder().Update(typeof(Dummy));
            Assert.AreEqual($"UPDATE \"public\".\"{nameof(Dummy)}\" SET \"Name\" = @Name WHERE \"Id\" = @Id", sql);
        }

        [TestMethod]
        public void TestDelete()
        {
            var sql = _connection.GetBuilder().Delete(typeof(Dummy));
            Assert.AreEqual($"DELETE FROM \"public\".\"{nameof(Dummy)}\" WHERE \"Id\" = @Id", sql);
        }

        [TestMethod]
        public void TestDeleteBy()
        {
            var sql = _connection.GetBuilder().DeleteBy(typeof(Dummy), nameof(Dummy.Name));
            Assert.AreEqual($"DELETE FROM \"public\".\"{nameof(Dummy)}\" WHERE \"Name\" = @Name", sql);
        }
    }
}