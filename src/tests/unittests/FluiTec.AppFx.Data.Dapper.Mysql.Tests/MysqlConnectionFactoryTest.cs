using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mysql.Tests
{
    /// <summary>   (Unit Test Class) a mysql connection factory test. </summary>
    [TestClass]
    public class MysqlConnectionFactoryTest
    {
        /// <summary>   (Unit Test Method) can construct. </summary>
        [TestMethod]
        public void CanConstruct()
        {
            var unused = new MysqlConnectionFactory();
        }

        /// <summary>   (Unit Test Method) can create connection. </summary>
        [TestMethod]
        public void CanCreateConnection()
        {
            const string sampleConnectionString =
                "Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;";
            var factory = new MysqlConnectionFactory();
            factory.CreateConnection(sampleConnectionString);
        }
    }
}