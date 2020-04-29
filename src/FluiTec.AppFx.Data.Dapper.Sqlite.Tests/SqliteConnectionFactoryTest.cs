using FluiTec.AppFx.Data.Dapper.SqLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Sqlite.Tests
{
    /// <summary>   (Unit Test Class) a sqlite connection factory test. </summary>
    [TestClass]
    public class SqliteConnectionFactoryTest
    {
        /// <summary>   (Unit Test Method) can construct. </summary>
        [TestMethod]
        public void CanConstruct()
        {
            var unused = new SqliteConnectionFactory();
        }

        /// <summary>   (Unit Test Method) can create connection. </summary>
        [TestMethod]
        public void CanCreateConnection()
        {
            const string sampleConnectionString = "Data Source=:memory:;";
            var factory = new SqliteConnectionFactory();
            factory.CreateConnection(sampleConnectionString);
        }
    }
}