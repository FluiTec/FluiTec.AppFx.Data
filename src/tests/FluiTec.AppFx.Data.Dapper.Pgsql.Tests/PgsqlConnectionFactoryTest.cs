using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Pgsql.Tests
{
    /// <summary>   (Unit Test Class) a pgsql connection factory test. </summary>
    [TestClass]
    public class PgsqlConnectionFactoryTest
    {
        /// <summary>   (Unit Test Method) can construct. </summary>
        [TestMethod]
        public void CanConstruct()
        {
            var unused = new PgsqlConnectionFactory();
        }

        /// <summary>   (Unit Test Method) can create connection. </summary>
        [TestMethod]
        public void CanCreateConnection()
        {
            const string sampleConnectionString = "Server=127.0.0.1;Port=5432;Database=myDataBase;User Id=myUsername;Password=myPassword;";
            var factory = new PgsqlConnectionFactory();
            factory.CreateConnection(sampleConnectionString);
        }
    }
}