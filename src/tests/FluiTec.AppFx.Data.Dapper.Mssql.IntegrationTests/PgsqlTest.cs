using System;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mssql.IntegrationTests
{
    /// <summary>   (Unit Test Class) a pgsql tests.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlTests
    {
        private readonly bool _isDbAvailable;

        private readonly PgsqlDapperServiceOptions _options;

        /// <summary>   Default constructor.</summary>
        public PgsqlTests()
        {
            var db = Environment.GetEnvironmentVariable("POSTGRES_DB");
            var usr = Environment.GetEnvironmentVariable("POSTGRES_USER");
            if (!string.IsNullOrWhiteSpace(db) && !string.IsNullOrWhiteSpace(usr))
            {
                _isDbAvailable = true;
                _options = new PgsqlDapperServiceOptions {ConnectionString = $"User ID={usr};Host=postgres;Database={db};Pooling=true;" };
            }
        }

        /// <summary>   (Unit Test Method) can create unit of work.</summary>
        [TestMethod]
        public void CanCreateUnitOfWork()
        {
            if (_isDbAvailable && _options != null)
            {
                var service = new PgsqlTestDataService(_options, null);
                using (var uow = service.BeginUnitOfWork())
                {
                    uow.DummyRepository.GetAll();
                }

                Console.WriteLine("AVAILABLE");
            }
            else
            {
                Console.WriteLine("NOT AVAILABLE");
            }
        }
    }
}
