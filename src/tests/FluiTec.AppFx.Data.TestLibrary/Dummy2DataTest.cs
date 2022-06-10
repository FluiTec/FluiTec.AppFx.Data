using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Data.TestLibrary.DataTests;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using FluiTec.AppFx.Data.TestLibrary.UnitsOfWork;

namespace FluiTec.AppFx.Data.TestLibrary
{
    /// <summary> A dummy 2 data test.</summary>
    public abstract class Dummy2DataTest : EntityDataTest<ITestDataService, ITestUnitOfWork, Dummy2Entity, Guid>
    {
        /// <summary> Specialized constructor for use only by derived class.</summary>
        ///
        /// <param name="dataServiceProvider"> The data service provider. </param>
        protected Dummy2DataTest(DataServiceProvider<ITestDataService, ITestUnitOfWork> dataServiceProvider) : base(dataServiceProvider)
        {
        }

        /// <summary>
        ///     Creates an entity.
        /// </summary>
        /// <returns>
        ///     The new entity.
        /// </returns>
        protected override Dummy2Entity CreateEntity()
        {
            return new Dummy2Entity { Name = "Dummy" };
        }

        /// <summary>
        ///     Creates non updateable entity.
        /// </summary>
        /// <returns>
        ///     The new non updateable entity.
        /// </returns>
        protected override Dummy2Entity CreateNonUpdateableEntity()
        {
            return new Dummy2Entity { Id = Guid.NewGuid(), Name = "Dummy" };
        }

        /// <summary>
        ///     Enumerates create entities in this collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process create entities in this collection.
        /// </returns>
        protected override IEnumerable<Dummy2Entity> CreateEntities()
        {
            return new[]
            {
                new Dummy2Entity {Name = "Dummy 1"},
                new Dummy2Entity {Name = "Dummy 2"}
            };
        }

        /// <summary>
        ///     Query if 'entity' has valid key.
        /// </summary>
        /// <param name="entity">   The entity. </param>
        /// <returns>
        ///     True if valid key, false if not.
        /// </returns>
        protected override bool HasValidKey(Dummy2Entity entity)
        {
            return !entity.Id.Equals(Guid.Empty);
        }

        /// <summary>
        ///     Entity equals.
        /// </summary>
        /// <param name="code"> The code. </param>
        /// <param name="db">   The database. </param>
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
        protected override bool EntityEquals(Dummy2Entity code, Dummy2Entity db)
        {
            return code.Equals(db);
        }

        /// <summary>
        ///     Change entity.
        /// </summary>
        /// <param name="entity">   The entity. </param>
        /// <returns>
        ///     A TEntity.
        /// </returns>
        protected override Dummy2Entity ChangeEntity(Dummy2Entity entity)
        {
            entity.Name = "Changed";
            return entity;
        }
    }
}