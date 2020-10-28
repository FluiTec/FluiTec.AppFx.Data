using System;
using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mssql.IntegrationTests
{
    /// <summary>   (Unit Test Class) a pgsql tests.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlTests
    {
        private readonly PgsqlDapperServiceOptions _options;

        private bool IsDbAvailable => _options != null;

        /// <summary>   Default constructor.</summary>
        public PgsqlTests()
        {
            var db = Environment.GetEnvironmentVariable("POSTGRES_DB");
            var usr = Environment.GetEnvironmentVariable("POSTGRES_USER");
            if (!string.IsNullOrWhiteSpace(db) && !string.IsNullOrWhiteSpace(usr))
            {
                _options = new PgsqlDapperServiceOptions
                    {ConnectionString = $"User ID={usr};Host=postgres;Database={db};Pooling=true;"};
            }
        }

        /// <summary>   Assert database available.</summary>
        private void AssertDbAvailable()
        {
            Assert.IsTrue(IsDbAvailable, "POSTGRES-DB NOT AVAILABLE!");
        }

        /// <summary>   (Unit Test Method) can check apply migrations.</summary>
        [TestInitialize]
        public void CanCheckApplyMigrations()
        {
            AssertDbAvailable();

            var service = new PgsqlTestDataService(_options, null);
            var migrator = new DapperDataMigrator(_options.ConnectionString, new [] {typeof(PgsqlTestDataService).Assembly}, service.MetaData,
                builder => builder.AddPostgres());
            migrator.Migrate();
        }

        /// <summary>   (Unit Test Method) can create unit of work.</summary>
        [TestMethod]
        public void CanCreateUnitOfWork()
        {
            AssertDbAvailable();

            var service = new PgsqlTestDataService(_options, null);
            using (var uow = service.BeginUnitOfWork())
            {
            }
        }

        /// <summary>   (Unit Test Method) can create entity.</summary>
        [TestMethod]
        public void CanCreateEntity()
        {
            AssertDbAvailable();

            var service = new PgsqlTestDataService(_options, null);
            using (var uow = service.BeginUnitOfWork())
            {
                var entity = uow.DummyRepository.Add(new DummyEntity {Name = "Test"});
                Assert.IsTrue(entity.Id > -1);
            }
        }
    }
}
