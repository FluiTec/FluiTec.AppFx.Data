using System;
using System.Linq;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.TestLibrary
{
    /// <summary>   A database test.</summary>
    public abstract class DbTest
    {
        #region Constructors

        /// <summary>   Specialized default constructor for use only by derived class.</summary>
        protected DbTest()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            InitOptionsAndDataService();
        }

        #endregion

        #region Fields

        /// <summary>   Gets a value indicating whether the database is available.</summary>
        /// <value> True if the database is available, false if not.</value>
        protected bool IsDbAvailable => ServiceOptions != null;

        #endregion

        #region Properties

        /// <summary>   Gets or sets options for controlling the service.</summary>
        /// <value> Options that control the service.</value>
        protected IDapperServiceOptions ServiceOptions { get; set; }

        /// <summary>   Gets or sets the data service.</summary>
        /// <value> The data service.</value>
        protected ITestDataService DataService { get; set; }

        #endregion

        #region Methods

        /// <summary>   Initializes the options and data service.</summary>
        protected abstract void InitOptionsAndDataService();

        /// <summary>   Assert database available.</summary>
        protected void AssertDbAvailable()
        {
            Assert.IsTrue(IsDbAvailable, "DB NOT AVAILABLE!");
        }

        #endregion

        #region TestMethods

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
            var entity = uow.DummyRepository.Add(new DummyEntity {Name = "Test"});
            Assert.IsTrue(entity.Id > -1);
        }

        /// <summary>   (Unit Test Method) can create entity asynchronous.</summary>
        [TestMethod]
        public void CanCreateEntityAsync()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.DummyRepository.AddAsync(new DummyEntity {Name = "Test"}).Result;
            Assert.IsTrue(entity.Id > -1);
        }

        /// <summary>   (Unit Test Method) can create multiple.</summary>
        [TestMethod]
        public void CanCreateMultiple()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            uow.DummyRepository.AddRange(new[]
            {
                new DummyEntity {Name = "Test1"},
                new DummyEntity {Name = "Test2"}
            });

            var entities = uow.DummyRepository.GetAll();
            foreach (var entity in entities)
                Assert.IsTrue(entity.Id > -1);
        }

        /// <summary>   (Unit Test Method) can create multiple asynchronous.</summary>
        [TestMethod]
        public void CanCreateMultipleAsync()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            uow.DummyRepository.AddRangeAsync(new[]
            {
                new DummyEntity {Name = "Test1"},
                new DummyEntity {Name = "Test2"}
            }).Wait();

            var entities = uow.DummyRepository.GetAllAsync().Result;
            foreach (var entity in entities)
                Assert.IsTrue(entity.Id > -1);
        }

        /// <summary>   (Unit Test Method) can read entity.</summary>
        [TestMethod]
        public void CanReadEntity()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.DummyRepository.Add(new DummyEntity {Name = "Test"});
            var dbEntity = uow.DummyRepository.Get(entity.Id);

            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can read entity asynchronous.</summary>
        [TestMethod]
        public void CanReadEntityAsync()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.DummyRepository.Add(new DummyEntity {Name = "Test"});
            var dbEntity = uow.DummyRepository.GetAsync(entity.Id).Result;

            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can update entity.</summary>
        [TestMethod]
        public void CanUpdateEntity()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.DummyRepository.Add(new DummyEntity {Name = "Test"});
            entity.Name = "Test2";

            uow.DummyRepository.Update(entity);

            var dbEntity = uow.DummyRepository.Get(entity.Id);
            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can throw update exception.</summary>
        [TestMethod]
        [ExpectedException(typeof(UpdateException))]
        public void CanThrowUpdateException()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            uow.DummyRepository.Update(new DummyEntity {Id = 100, Name = "Test"});
        }

        /// <summary>   (Unit Test Method) can throw update exception asynchronous.</summary>
        [TestMethod]
        public void CanThrowUpdateExceptionAsync()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            try
            {
                var unused = uow.DummyRepository.UpdateAsync(new DummyEntity {Id = 100, Name = "Test"}).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(typeof(UpdateException), e.InnerExceptions.Single().GetType());
            }
        }

        /// <summary>   (Unit Test Method) can update entity asynchronous.</summary>
        [TestMethod]
        public void CanUpdateEntityAsync()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.DummyRepository.AddAsync(new DummyEntity {Name = "Test"}).Result;
            entity.Name = "Test2";

            uow.DummyRepository.UpdateAsync(entity).Wait();

            var dbEntity = uow.DummyRepository.GetAsync(entity.Id).Result;
            Assert.AreEqual(entity.Name, dbEntity.Name);
        }

        /// <summary>   (Unit Test Method) can delete entity.</summary>
        [TestMethod]
        public void CanDeleteEntity()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.DummyRepository.Add(new DummyEntity {Name = "Test"});
            entity.Name = "Test2";

            Assert.IsTrue(uow.DummyRepository.Delete(entity));

            var dbEntity = uow.DummyRepository.Get(entity.Id);

            Assert.IsNull(dbEntity);
        }

        /// <summary>   (Unit Test Method) can delete entity asynchronous.</summary>
        [TestMethod]
        public void CanDeleteEntityAsync()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.DummyRepository.AddAsync(new DummyEntity {Name = "Test"}).Result;
            entity.Name = "Test2";

            Assert.IsTrue(uow.DummyRepository.DeleteAsync(entity).Result);

            var dbEntity = uow.DummyRepository.GetAsync(entity.Id).Result;

            Assert.IsNull(dbEntity);
        }

        /// <summary>   (Unit Test Method) can delete entity by identifier.</summary>
        [TestMethod]
        public void CanDeleteEntityById()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.DummyRepository.Add(new DummyEntity {Name = "Test"});
            entity.Name = "Test2";

            Assert.IsTrue(uow.DummyRepository.Delete(entity.Id));

            var dbEntity = uow.DummyRepository.Get(entity.Id);

            Assert.IsNull(dbEntity);
        }

        /// <summary>   (Unit Test Method) can delete entity by identifier asynchronous.</summary>
        [TestMethod]
        public void CanDeleteEntityByIdAsync()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity = uow.DummyRepository.AddAsync(new DummyEntity {Name = "Test"}).Result;
            entity.Name = "Test2";

            Assert.IsTrue(uow.DummyRepository.DeleteAsync(entity.Id).Result);

            var dbEntity = uow.DummyRepository.GetAsync(entity.Id).Result;

            Assert.IsNull(dbEntity);
        }

        /// <summary>   (Unit Test Method) can get all.</summary>
        [TestMethod]
        public void CanGetAll()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity1 = uow.DummyRepository.Add(new DummyEntity {Name = "Test1"});
            var entity2 = uow.DummyRepository.Add(new DummyEntity {Name = "Test2"});

            var dbCount = uow.DummyRepository.GetAll().Count();

            Assert.AreEqual(2, dbCount);
        }

        /// <summary>   (Unit Test Method) can get all asynchronous.</summary>
        [TestMethod]
        public void CanGetAllAsync()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity1 = uow.DummyRepository.Add(new DummyEntity {Name = "Test1"});
            var entity2 = uow.DummyRepository.Add(new DummyEntity {Name = "Test2"});

            var dbCount = uow.DummyRepository.GetAllAsync().Result.Count();

            Assert.AreEqual(2, dbCount);
        }

        /// <summary>   (Unit Test Method) can count.</summary>
        [TestMethod]
        public void CanCount()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity1 = uow.DummyRepository.Add(new DummyEntity {Name = "Test1"});
            var entity2 = uow.DummyRepository.Add(new DummyEntity {Name = "Test2"});

            var dbCount = uow.DummyRepository.Count();

            Assert.AreEqual(2, dbCount);
        }

        /// <summary>   (Unit Test Method) can count asynchronous.</summary>
        [TestMethod]
        public void CanCountAsync()
        {
            AssertDbAvailable();

            using var uow = DataService.BeginUnitOfWork();
            var entity1 = uow.DummyRepository.Add(new DummyEntity {Name = "Test1"});
            var entity2 = uow.DummyRepository.Add(new DummyEntity {Name = "Test2"});

            var dbCount = uow.DummyRepository.CountAsync().Result;

            Assert.AreEqual(2, dbCount);
        }

        #endregion
    }
}