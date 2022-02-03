using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.TestLibrary.DataTests
{
    /// <summary>
    ///     A dependency entity data test.
    /// </summary>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    /// <typeparam name="TEntity">      Type of the entity. </typeparam>
    /// <typeparam name="TKey">         Type of the key. </typeparam>
    // ReSharper disable once UnusedMember.Global
    public abstract class
        DependencyEntityDataTest<TDataService, TUnitOfWork, TEntity, TKey> : EntityDataTest<TDataService, TUnitOfWork,
            TEntity, TKey>
        where TDataService : IDataService<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
        where TEntity : class, IKeyEntity<TKey>, new()
    {
        /// <summary>
        ///     Specialized constructor for use only by derived class.
        /// </summary>
        /// <param name="dataServiceProvider">  The data service provider. </param>
        protected DependencyEntityDataTest(DataServiceProvider<TDataService, TUnitOfWork> dataServiceProvider) : base(
            dataServiceProvider)
        {
        }

        /// <summary>
        ///     Begins unit of work.
        /// </summary>
        /// <returns>
        ///     A TUnitOfWork.
        /// </returns>
        protected override TUnitOfWork BeginUnitOfWork()
        {
            var uow = base.BeginUnitOfWork();
            CreateDependencies(uow);
            return uow;
        }

        /// <summary>
        ///     Creates the dependencies.
        /// </summary>
        /// <param name="uow">  The uow. </param>
        protected abstract void CreateDependencies(TUnitOfWork uow);
    }
}