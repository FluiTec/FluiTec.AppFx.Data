using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.TestLibrary.DataTests
{
    /// <summary>
    ///     A data test.
    /// </summary>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public abstract class DataTest<TDataService, TUnitOfWork>
        where TDataService : IDataService<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        #region Constructors

        /// <summary>
        ///     Specialized constructor for use only by derived class.
        /// </summary>
        /// <param name="dataServiceProvider">  The data service provider. </param>
        protected DataTest(DataServiceProvider<TDataService, TUnitOfWork> dataServiceProvider)
        {
            DataServiceProvider = dataServiceProvider;
            DataService = DataServiceProvider.ProvideDataService();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the data service provider.
        /// </summary>
        /// <value>
        ///     The data service provider.
        /// </value>
        public DataServiceProvider<TDataService, TUnitOfWork> DataServiceProvider { get; }

        /// <summary>
        ///     Gets the data service.
        /// </summary>
        /// <value>
        ///     The data service.
        /// </value>
        private TDataService DataService { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Begins unit of work.
        /// </summary>
        /// <returns>
        ///     A TUnitOfWork.
        /// </returns>
        protected virtual TUnitOfWork BeginUnitOfWork()
        {
            return DataService.BeginUnitOfWork();
        }

        /// <summary>
        ///     Assert database available.
        /// </summary>
        [TestInitialize]
        protected virtual void AssertDbAvailable()
        {
            Assert.IsTrue(DataServiceProvider.IsDbAvailable, "DB NOT AVAILABLE!");
        }

        /// <summary>   (Unit Test Method) can create unit of work.</summary>
        [TestMethod]
        public virtual void CanCreateUnitOfWork()
        {
            using var uow = DataService.BeginUnitOfWork();
        }

        #endregion
    }
}