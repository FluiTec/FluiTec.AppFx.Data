using FluiTec.AppFx.Data.Dapper.Migration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Tests.Migration
{
    [TestClass]
    public class VersionTableTests
    {
        [TestMethod]
        public void TestSavesSchema()
        {
            const string schema = "test";
            var table = new VersionTable(schema);
            Assert.AreEqual(schema, table.SchemaName);
        }
    }
}