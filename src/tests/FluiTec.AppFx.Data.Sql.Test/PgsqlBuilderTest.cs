using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;

namespace FluiTec.AppFx.Data.Sql.Test
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
    }
}