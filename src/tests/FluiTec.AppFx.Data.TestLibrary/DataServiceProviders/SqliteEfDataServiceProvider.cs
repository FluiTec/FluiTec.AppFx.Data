using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Ef;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.TestLibrary.Configuration;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.TestLibrary.DataServiceProviders
{
    /// <summary>
    ///     A sqlite ef data service provider.
    /// </summary>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public abstract class SqliteEfDataServiceProvider<TDataService, TUnitOfWork>
        : EfDataServiceProvider<TDataService, TUnitOfWork>
        where TDataService : IDataService<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        /// <summary>
        ///     Configure options.
        /// </summary>
        /// <returns>
        ///     The IDapperServiceOptions.
        /// </returns>
        protected override ISqlServiceOptions ConfigureOptions()
        {
            return new EfSqlServiceOptions
            {
                SqlType = SqlType.Sqlite,
                ConnectionString = "Data Source=mydb.db;"
            };
        }

        /// <summary>
        ///     Configure admin options.
        /// </summary>
        /// <returns>
        ///     The DbAdminOptions.
        /// </returns>
        protected override DbAdminOptions ConfigureAdminOptions()
        {
            return null;
        }
    }
}