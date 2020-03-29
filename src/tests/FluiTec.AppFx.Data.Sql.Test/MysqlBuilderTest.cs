using System.Data;
using FluiTec.AppFx.Data.EntityNameServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;

namespace FluiTec.AppFx.Data.Sql.Test
{
    [TestClass]
    public class MysqlBuilderTest
    {
        /// <summary>	The connection. </summary>
        private readonly IDbConnection _connection;

        /// <summary>	Default constructor. </summary>
        public MysqlBuilderTest()
        {
            _connection = new MySqlConnection();
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
            Assert.AreEqual($"{nameService.Name(typeof(Dummy))}", tableName);
        }

        [TestMethod]
        public void TestRenderPropertyName()
        {
            var type = typeof(Dummy);
            var propertyInfo = type.GetProperty(nameof(Dummy.Id));
            var propertyName = $"{nameof(Dummy.Id)}";
            var renderedPropertyName = _connection.GetBuilder().Adapter.RenderPropertyName(propertyInfo);
            Assert.AreEqual(propertyName, renderedPropertyName);
        }

        [TestMethod]
        [DataRow("Test")]
        public void TestRenderPropertyNameByString(string name)
        {
            var propertyName = $"{name}";
            var renderedPropertyName = _connection.GetBuilder().Adapter.RenderPropertyName(name);
            Assert.AreEqual(propertyName, renderedPropertyName);
        }

        [TestMethod]
        public void TestRenderPropertyList()
        {
            var properties = typeof(Dummy).GetProperties();
            var renderedList = _connection.GetBuilder().Adapter.RenderPropertyList(properties);
            Assert.AreEqual("Id, Name", renderedList.ToString());
        }

        [TestMethod]
        public void SelectByFilterTest()
        {
            var sql = _connection.GetBuilder().SelectByFilter(typeof(Dummy), nameof(Dummy.Name));
            Assert.AreEqual("SELECT Id, Name FROM Dummy WHERE Name = @Name", sql);
        }

        [TestMethod]
        public void SelectByFilterMultiTest()
        {
            var sql1 = _connection.GetBuilder().SelectByFilter(typeof(Dummy), new[] { nameof(Dummy.Name) });
            Assert.AreEqual("SELECT Id, Name FROM Dummy WHERE Name = @Name", sql1);

            var sql2 = _connection.GetBuilder().SelectByFilter(typeof(Dummy), new[] { nameof(Dummy.Id), nameof(Dummy.Name) });
            Assert.AreEqual("SELECT Id, Name FROM Dummy WHERE Id = @Id AND Name = @Name", sql2);
        }

        [TestMethod]
        public void TestSelectAll()
        {
            var sql = _connection.GetBuilder().SelectAll(typeof(Dummy));
            Assert.AreEqual($"SELECT Id, Name FROM {nameof(Dummy)}", sql);
        }

        [TestMethod]
        public void TestSelectByKey()
        {
            var sql = _connection.GetBuilder().SelectByKey(typeof(Dummy));
            Assert.AreEqual($"SELECT Id, Name FROM {nameof(Dummy)} WHERE Id = @Id", sql);
        }

        [TestMethod]
        public void TestSelectCustom()
        {
            var sql = _connection.GetBuilder().SelectByFilter(typeof(Dummy), nameof(Dummy.Id));
            Assert.AreEqual($"SELECT Id, Name FROM {nameof(Dummy)} WHERE Id = @Id", sql);
        }

        [TestMethod]
        public void TestInsertAutoKey()
        {
            var sql = _connection.GetBuilder().InsertAutoKey(typeof(Dummy));
            Assert.AreEqual(
                $"INSERT INTO {nameof(Dummy)} (Name) VALUES (@Name);SELECT LAST_INSERT_ID() {nameof(Dummy.Id)}", sql);
        }

        [TestMethod]
        public void TestInsertMultiple()
        {
            var sql = _connection.GetBuilder().InsertAutoMultiple(typeof(Dummy));
            Assert.AreEqual($"INSERT INTO {nameof(Dummy)} (Name) VALUES (@Name)", sql);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var sql = _connection.GetBuilder().Update(typeof(Dummy));
            Assert.AreEqual($"UPDATE {nameof(Dummy)} SET Name = @Name WHERE Id = @Id", sql);
        }

        [TestMethod]
        public void TestDelete()
        {
            var sql = _connection.GetBuilder().Delete(typeof(Dummy));
            Assert.AreEqual($"DELETE FROM {nameof(Dummy)} WHERE Id = @Id", sql);
        }
    }
}