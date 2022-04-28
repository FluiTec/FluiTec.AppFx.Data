using System;
using System.Collections.Generic;
using System.Linq;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.TestLibrary.DataTests
{
    /// <summary>
    ///     An entity data test.
    /// </summary>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    /// <typeparam name="TEntity">      Type of the entity. </typeparam>
    /// <typeparam name="TKey">         Type of the key. </typeparam>
    public abstract class EntityDataTest<TDataService, TUnitOfWork, TEntity, TKey> : DataTest<TDataService, TUnitOfWork>
        where TDataService : IDataService<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
        where TEntity : class, IKeyEntity<TKey>, new()
    {
        #region Constructors

        /// <summary>
        ///     Specialized constructor for use only by derived class.
        /// </summary>
        /// <param name="dataServiceProvider">  The data service provider. </param>
        protected EntityDataTest(DataServiceProvider<TDataService, TUnitOfWork> dataServiceProvider) : base(
            dataServiceProvider)
        {
        }

        #endregion

        #region Abstract

        /// <summary>
        ///     Creates an entity.
        /// </summary>
        /// <returns>
        ///     The new entity.
        /// </returns>
        protected abstract TEntity CreateEntity();

        /// <summary>
        ///     Creates non updateable entity.
        /// </summary>
        /// <returns>
        ///     The new non updateable entity.
        /// </returns>
        protected abstract TEntity CreateNonUpdateableEntity();

        /// <summary>
        ///     Enumerates create entities in this collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process create entities in this collection.
        /// </returns>
        protected abstract IEnumerable<TEntity> CreateEntities();

        /// <summary>
        ///     Query if 'entity' has valid key.
        /// </summary>
        /// <param name="entity">   The entity. </param>
        /// <returns>
        ///     True if valid key, false if not.
        /// </returns>
        protected abstract bool HasValidKey(TEntity entity);

        /// <summary>
        ///     Entity equals.
        /// </summary>
        /// <param name="code"> The code. </param>
        /// <param name="db">   The database. </param>
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
        protected abstract bool EntityEquals(TEntity code, TEntity db);

        /// <summary>
        ///     Change entity.
        /// </summary>
        /// <param name="entity">   The entity. </param>
        /// <returns>
        ///     A TEntity.
        /// </returns>
        protected abstract TEntity ChangeEntity(TEntity entity);

        #endregion

        #region Tests

        #region Create

        /// <summary>   (Unit Test Method) can create entity.</summary>
        [TestMethod]
        public virtual void CanCreateEntity()
        {
            using var uow = BeginUnitOfWork();

            var entity = uow.GetWritableRepository<TEntity>().Add(CreateEntity());
            Assert.IsTrue(HasValidKey(entity));
        }

        /// <summary>
        ///     (Unit Test Method) can create entity asynchronous.
        /// </summary>
        [TestMethod]
        public virtual void CanCreateEntityAsync()
        {
            using var uow = BeginUnitOfWork();

            var entity = uow.GetWritableRepository<TEntity>().AddAsync(CreateEntity()).Result;
            Assert.IsTrue(HasValidKey(entity));
        }

        /// <summary>
        ///     (Unit Test Method) can create multiple.
        /// </summary>
        [TestMethod]
        public virtual void CanCreateMultiple()
        {
            using var uow = BeginUnitOfWork();

            uow.GetWritableRepository<TEntity>().AddRange(CreateEntities());

            var entities = uow.GetDataRepository<TEntity>().GetAll();
            foreach (var entity in entities)
                Assert.IsTrue(HasValidKey(entity));
        }

        /// <summary>
        ///     (Unit Test Method) can create multiple asynchronous.
        /// </summary>
        [TestMethod]
        public virtual void CanCreateMultipleAsync()
        {
            using var uow = BeginUnitOfWork();

            uow.GetWritableRepository<TEntity>().AddRangeAsync(CreateEntities()).Wait();

            var entities = uow.GetDataRepository<TEntity>().GetAllAsync().Result;
            foreach (var entity in entities)
                Assert.IsTrue(HasValidKey(entity));
        }

        #endregion

        #region Read

        /// <summary>
        ///     (Unit Test Method) can read entity.
        /// </summary>
        [TestMethod]
        public virtual void CanReadEntity()
        {
            using var uow = BeginUnitOfWork();

            var entity = uow.GetWritableRepository<TEntity>().Add(CreateEntity());
            var dbEntity = uow.GetKeyWritableRepository<TEntity, TKey>().Get(entity.Id);

            Assert.IsTrue(EntityEquals(entity, dbEntity));
        }

        /// <summary>
        /// (Unit Test Method) can read entity by keys.
        /// </summary>
        [TestMethod]
        public virtual void CanReadEntityByKeys()
        {
            using var uow = BeginUnitOfWork();

            var entity = uow.GetWritableRepository<TEntity>().Add(CreateEntity());
            var dbEntity = uow.GetKeyWritableRepository<TEntity, TKey>().Get(new object[] {entity.Id});

            Assert.IsTrue(EntityEquals(entity, dbEntity));
        }

        /// <summary>
        ///     (Unit Test Method) can read entity asynchronous.
        /// </summary>
        [TestMethod]
        public virtual void CanReadEntityAsync()
        {
            using var uow = BeginUnitOfWork();

            var entity = uow.GetWritableRepository<TEntity>().Add(CreateEntity());
            var dbEntity = uow.GetKeyWritableRepository<TEntity, TKey>().GetAsync(entity.Id).Result;

            Assert.IsTrue(EntityEquals(entity, dbEntity));
        }

        /// <summary>
        /// (Unit Test Method) can read entity by keys asynchronous.
        /// </summary>
        [TestMethod]
        public virtual void CanReadEntityByKeysAsync()
        {
            using var uow = BeginUnitOfWork();

            var entity = uow.GetWritableRepository<TEntity>().Add(CreateEntity());
            var dbEntity = uow.GetKeyWritableRepository<TEntity, TKey>().GetAsync(new object[] {entity.Id}).Result;

            Assert.IsTrue(EntityEquals(entity, dbEntity));
        }

        /// <summary>
        ///     (Unit Test Method) can get all.
        /// </summary>
        [TestMethod]
        public virtual void CanGetAll()
        {
            using var uow = BeginUnitOfWork();

            var entities = CreateEntities().ToList();
            uow.GetWritableRepository<TEntity>().AddRange(entities);

            var dbCount = uow.GetDataRepository<TEntity>().GetAll().Count();

            Assert.AreEqual(entities.Count, dbCount);
        }

        /// <summary>
        ///     (Unit Test Method) can get all asynchronous.
        /// </summary>
        [TestMethod]
        public virtual void CanGetAllAsync()
        {
            using var uow = BeginUnitOfWork();

            var entities = CreateEntities().ToList();
            uow.GetWritableRepository<TEntity>().AddRange(entities);

            var dbCount = uow.GetDataRepository<TEntity>().GetAllAsync().Result.Count();

            Assert.AreEqual(entities.Count, dbCount);
        }

        /// <summary>
        ///     (Unit Test Method) can count.
        /// </summary>
        [TestMethod]
        public virtual void CanCount()
        {
            using var uow = BeginUnitOfWork();

            var entities = CreateEntities().ToList();
            uow.GetKeyWritableRepository<TEntity, TKey>().AddRange(entities);

            var dbCount = uow.GetDataRepository<TEntity>().Count();

            Assert.AreEqual(entities.Count, dbCount);
        }

        /// <summary>
        ///     (Unit Test Method) can count asynchronous.
        /// </summary>
        [TestMethod]
        public virtual void CanCountAsync()
        {
            using var uow = BeginUnitOfWork();

            var entities = CreateEntities().ToList();
            uow.GetWritableRepository<TEntity>().AddRange(entities);

            var dbCount = uow.GetDataRepository<TEntity>().CountAsync().Result;

            Assert.AreEqual(entities.Count, dbCount);
        }

        #endregion

        #region Update

        /// <summary>
        ///     (Unit Test Method) can update entity.
        /// </summary>
        [TestMethod]
        public virtual void CanUpdateEntity()
        {
            using var uow = BeginUnitOfWork();

            var entity = uow.GetWritableRepository<TEntity>().Add(CreateEntity());
            ChangeEntity(entity);

            uow.GetWritableRepository<TEntity>().Update(entity);

            var dbEntity = uow.GetKeyWritableRepository<TEntity, TKey>().Get(entity.Id);
            Assert.IsTrue(EntityEquals(entity, dbEntity));
        }

        /// <summary>
        ///     (Unit Test Method) can update entity asynchronous.
        /// </summary>
        [TestMethod]
        public virtual void CanUpdateEntityAsync()
        {
            using var uow = BeginUnitOfWork();

            var entity = uow.GetWritableRepository<TEntity>().AddAsync(CreateEntity()).Result;
            ChangeEntity(entity);

            uow.GetWritableRepository<TEntity>().UpdateAsync(entity).Wait();

            var dbEntity = uow.GetKeyWritableRepository<TEntity, TKey>().GetAsync(entity.Id).Result;
            Assert.IsTrue(EntityEquals(entity, dbEntity));
        }

        /// <summary>
        ///     (Unit Test Method) can throw update exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(UpdateException))]
        public virtual void CanThrowUpdateException()
        {
            using var uow = BeginUnitOfWork();

            uow.GetWritableRepository<TEntity>().Update(CreateNonUpdateableEntity());
        }

        /// <summary>
        ///     (Unit Test Method) can throw update exception asynchronous.
        /// </summary>
        [TestMethod]
        public virtual void CanThrowUpdateExceptionAsync()
        {
            using var uow = BeginUnitOfWork();

            try
            {
                var unused = uow.GetWritableRepository<TEntity>().UpdateAsync(CreateNonUpdateableEntity()).Result;
            }
            catch (UpdateException e)
            {
                Assert.IsTrue(e.GetType() == typeof(UpdateException));
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(typeof(UpdateException), e.InnerExceptions.Single().GetType());
            }
        }

        #endregion

        #region Delete

        /// <summary>
        ///     (Unit Test Method) can delete entity.
        /// </summary>
        [TestMethod]
        public virtual void CanDeleteEntity()
        {
            using var uow = BeginUnitOfWork();

            var entity = uow.GetWritableRepository<TEntity>().Add(CreateEntity());

            Assert.IsTrue(uow.GetWritableRepository<TEntity>().Delete(entity));

            var dbEntity = uow.GetKeyWritableRepository<TEntity, TKey>().Get(entity.Id);

            Assert.IsNull(dbEntity);
        }

        /// <summary>
        ///     (Unit Test Method) can delete entity asynchronous.
        /// </summary>
        [TestMethod]
        public virtual void CanDeleteEntityAsync()
        {
            using var uow = BeginUnitOfWork();

            var entity = uow.GetWritableRepository<TEntity>().AddAsync(CreateEntity()).Result;
            ChangeEntity(entity);

            Assert.IsTrue(uow.GetWritableRepository<TEntity>().DeleteAsync(entity).Result);

            var dbEntity = uow.GetKeyWritableRepository<TEntity, TKey>().GetAsync(entity.Id).Result;

            Assert.IsNull(dbEntity);
        }

        /// <summary>
        ///     (Unit Test Method) can delete entity by identifier.
        /// </summary>
        [TestMethod]
        public virtual void CanDeleteEntityById()
        {
            using var uow = BeginUnitOfWork();

            var entity = uow.GetWritableRepository<TEntity>().Add(CreateEntity());
            ChangeEntity(entity);

            Assert.IsTrue(uow.GetKeyWritableRepository<TEntity, TKey>().Delete(entity.Id));

            var dbEntity = uow.GetKeyWritableRepository<TEntity, TKey>().Get(entity.Id);

            Assert.IsNull(dbEntity);
        }

        /// <summary>
        ///     (Unit Test Method) can delete entity by identifier asynchronous.
        /// </summary>
        [TestMethod]
        public virtual void CanDeleteEntityByIdAsync()
        {
            using var uow = BeginUnitOfWork();

            var entity = uow.GetWritableRepository<TEntity>().AddAsync(CreateEntity()).Result;
            ChangeEntity(entity);

            Assert.IsTrue(uow.GetKeyWritableRepository<TEntity, TKey>().DeleteAsync(entity.Id).Result);

            var dbEntity = uow.GetKeyWritableRepository<TEntity, TKey>().GetAsync(entity.Id).Result;

            Assert.IsNull(dbEntity);
        }

        #endregion

        #endregion
    }
}