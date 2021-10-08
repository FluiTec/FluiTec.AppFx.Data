using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Data.TestLibrary.DataTests;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using FluiTec.AppFx.Data.TestLibrary.UnitsOfWork;

namespace FluiTec.AppFx.Data.TestLibrary
{
    /// <summary>
    /// A date time dummy data test.
    /// </summary>
    public abstract class DateTimeDummyDataTest : EntityDataTest<ITestDataService, ITestUnitOfWork, DateTimeDummyEntity, int>
    {
        /// <summary>
        /// Specialized constructor for use only by derived class.
        /// </summary>
        ///
        /// <param name="dataServiceProvider">  The data service provider. </param>
        protected DateTimeDummyDataTest(DataServiceProvider<ITestDataService, ITestUnitOfWork> dataServiceProvider) : base(dataServiceProvider)
        {
        }

        /// <summary>
        /// Creates an entity.
        /// </summary>
        ///
        /// <returns>
        /// The new entity.
        /// </returns>
        protected override DateTimeDummyEntity CreateEntity()
        {
            return new DateTimeDummyEntity {Name = "Dummy", ChangeDate = DateTimeOffset.Now};
        }

        /// <summary>
        /// Creates non updateable entity.
        /// </summary>
        ///
        /// <returns>
        /// The new non updateable entity.
        /// </returns>
        protected override DateTimeDummyEntity CreateNonUpdateableEntity()
        {
            return new DateTimeDummyEntity {Id = 100, Name = "Dummy", ChangeDate = DateTimeOffset.Now};
        }

        /// <summary>
        /// Enumerates create entities in this collection.
        /// </summary>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process create entities in this collection.
        /// </returns>
        protected override IEnumerable<DateTimeDummyEntity> CreateEntities()
        {
            return new[]
            {
                new DateTimeDummyEntity {Name = "Dummy 1", ChangeDate = DateTimeOffset.Now},
                new DateTimeDummyEntity {Name = "Dummy 2", ChangeDate = DateTimeOffset.Now}
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
        protected override bool HasValidKey(DateTimeDummyEntity entity)
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
        protected override bool EntityEquals(DateTimeDummyEntity code, DateTimeDummyEntity db)
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
        protected override DateTimeDummyEntity ChangeEntity(DateTimeDummyEntity entity)
        {
            entity.Name = "Changed";
            return entity;
        }
    }
}