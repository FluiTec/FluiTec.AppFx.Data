using System.Collections.Generic;
using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Data.TestLibrary.DataTests;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using FluiTec.AppFx.Data.TestLibrary.UnitsOfWork;

namespace FluiTec.AppFx.Data.TestLibrary
{
    /// <summary>
    /// A test data test.
    /// </summary>
    public abstract class TestDataTest : EntityDataTest<ITestDataService, ITestUnitOfWork, DummyEntity, int>
    {
        /// <summary>
        /// Specialized constructor for use only by derived class.
        /// </summary>
        ///
        /// <param name="dataServiceProvider">  The data service provider. </param>
        protected TestDataTest(DataServiceProvider<ITestDataService, ITestUnitOfWork> dataServiceProvider) : base(dataServiceProvider)
        {

        }

        /// <summary>
        /// Creates an entity.
        /// </summary>
        ///
        /// <returns>
        /// The new entity.
        /// </returns>
        protected override DummyEntity CreateEntity()
        {
            return new DummyEntity {Name = "Dummy"};
        }

        /// <summary>
        /// Creates non updateable entity.
        /// </summary>
        ///
        /// <returns>
        /// The new non updateable entity.
        /// </returns>
        protected override DummyEntity CreateNonUpdateableEntity()
        {
            return new DummyEntity { Id = 100, Name = "Dummy"};
        }

        /// <summary>
        /// Enumerates create entities in this collection.
        /// </summary>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process create entities in this collection.
        /// </returns>
        protected override IEnumerable<DummyEntity> CreateEntities()
        {
            return new[]
            {
                new DummyEntity {Name = "Dummy 1"},
                new DummyEntity {Name = "Dummy 2"}
            };
        }

        /// <summary>
        /// Query if 'entity' has valid key.
        /// </summary>
        ///
        /// <param name="entity">   The entity. </param>
        ///
        /// <returns>
        /// True if valid key, false if not.
        /// </returns>
        protected override bool HasValidKey(DummyEntity entity)
        {
            return entity.Id > 0;
        }

        /// <summary>
        /// Entity equals.
        /// </summary>
        ///
        /// <param name="code"> The code. </param>
        /// <param name="db">   The database. </param>
        ///
        /// <returns>
        /// True if it succeeds, false if it fails.
        /// </returns>
        protected override bool EntityEquals(DummyEntity code, DummyEntity db)
        {
            return code.Equals(db);
        }

        /// <summary>
        /// Change entity.
        /// </summary>
        ///
        /// <param name="entity">   The entity. </param>
        ///
        /// <returns>
        /// A TEntity.
        /// </returns>
        protected override DummyEntity ChangeEntity(DummyEntity entity)
        {
            entity.Name = "Changed";
            return entity;
        }
    }
}