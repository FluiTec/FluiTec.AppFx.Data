using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.TestLibrary.DataServiceProviders
{
    /// <summary>
    /// A lite database data service provider.
    /// </summary>
    ///
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public abstract class LiteDbDataServiceProvider<TDataService, TUnitOfWork>
        : DataServiceProvider<TDataService, TUnitOfWork>
        where TDataService : IDataService<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Configure options.
        /// </summary>
        ///
        /// <returns>
        /// The LiteDbServiceOptions.
        /// </returns>
        public LiteDbServiceOptions ConfigureOptions()
        {
            return new LiteDbServiceOptions {DbFileName = "mydb.ldb", UseSingletonConnection = true};
        }
    }
}