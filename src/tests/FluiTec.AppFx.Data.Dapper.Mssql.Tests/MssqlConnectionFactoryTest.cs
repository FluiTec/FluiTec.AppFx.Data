using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mssql.Tests
{
    /// <summary>   (Unit Test Class) a mssql connection factory test. </summary>
    [TestClass]
    public class MssqlConnectionFactoryTest
    {
        /// <summary>   (Unit Test Method) can construct. </summary>
        [TestMethod]
        public void CanConstruct()
        {
            var unused = new MssqlConnectionFactory();
        }

        /// <summary>   (Unit Test Method) can create connection. </summary>
        [TestMethod]
        public void CanCreateConnection()
        {
            const string sampleConnectionString = "Server=myServerAddress; Database=myDataBase; Trusted_Connection=True;";
            var factory = new MssqlConnectionFactory();
            factory.CreateConnection(sampleConnectionString);
        }
    }
}