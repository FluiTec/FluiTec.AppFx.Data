using System.Data;
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