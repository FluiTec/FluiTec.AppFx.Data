using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.TestLibrary
{
    /// <summary>   A database test.</summary>
    public abstract class DbTest
    {
        /// <summary>   Gets a value indicating whether the database is available.</summary>
        /// <value> True if the database is available, false if not.</value>
        protected bool IsDbAvailable => ServiceOptions != null;

        /// <summary>   Gets options for controlling the service.</summary>
        /// <value> Options that control the service.</value>
        protected abstract IDapperServiceOptions ServiceOptions { get; }

        /// <summary>   Gets the data service.</summary>
        /// <value> The data service.</value>
        protected abstract ITestDataService DataService { get; }

        /// <summary>   Assert database available.</summary>
        protected void AssertDbAvailable()
        {
            Assert.IsTrue(IsDbAvailable, "DB NOT AVAILABLE!");
        }

        /// <summary>   (Unit Test Method) can create unit of work.</summary>
        [TestMethod]
        public void CanCreateUnitOfWork()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
        }

        /// <summary>   (Unit Test Method) can create entity.</summary>
        [TestMethod]
        public void CanCreateEntity()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.DummyRepository.Add(new DummyEntity { Name = "Test" });
            Assert.IsTrue(entity.Id > -1);
        }

        /// <summary>   (Unit Test Method) can read entity.</summary>
        [TestMethod]
        public void CanReadEntity()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.DummyRepository.Add(new DummyEntity { Name = "Test" });
            var dbEntity = uow.DummyRepository.Get(entity.Id);

            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can update entity.</summary>
        [TestMethod]
        public void CanUpdateEntity()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.DummyRepository.Add(new DummyEntity { Name = "Test" });
            entity.Name = "Test2";

            uow.DummyRepository.Update(entity);

            var dbEntity = uow.DummyRepository.Get(entity.Id);
            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can delete entity.</summary>
        [TestMethod]
        public void CanDeleteEntity()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.DummyRepository.Add(new DummyEntity { Name = "Test" });
            entity.Name = "Test2";

            uow.DummyRepository.Delete(entity);

            var dbEntity = uow.DummyRepository.Get(entity.Id);

            Assert.IsNull(dbEntity);
        }
    }
}