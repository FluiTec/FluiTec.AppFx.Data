using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.TestLibrary.DataServiceProviders
{
    /// <summary>
    ///     A data service provider.
    /// </summary>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public abstract class DataServiceProvider<TDataService, TUnitOfWork>
        where TDataService : IDataService<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        /// <summary>
        ///     Gets a value indicating whether the database is available.
        /// </summary>
        /// <value>
        ///     True if the database is available, false if not.
        /// </value>
        public abstract bool IsDbAvailable { get; }

        /// <summary>
        ///     Provide data service.
        /// </summary>
        /// <returns>
        ///     A TDataService.
        /// </returns>
        public abstract TDataService ProvideDataService();
    }
}