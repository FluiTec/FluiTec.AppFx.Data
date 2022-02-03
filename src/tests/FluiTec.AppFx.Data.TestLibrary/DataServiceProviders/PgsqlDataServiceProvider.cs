using System;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.TestLibrary.Configuration;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.TestLibrary.DataServiceProviders
{
    /// <summary>
    ///     A pgsql data service provider.
    /// </summary>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public abstract class PgsqlDataServiceProvider<TDataService, TUnitOfWork>
        : EnvironmentConfiguredDapperDataServiceProvider<TDataService, TUnitOfWork>
        where TDataService : IDataService<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        /// <summary>
        ///     Gets the name of the variable.
        /// </summary>
        /// <value>
        ///     The name of the variable.
        /// </value>
        protected override string VariableName => "POSTGRES_DB";

        /// <summary>
        ///     Configure options.
        /// </summary>
        /// <returns>
        ///     The IDapperServiceOptions.
        /// </returns>
        protected override IDapperServiceOptions ConfigureOptions()
        {
            if (!EnvironmentConfigured)
                return ConfigurationManager.ExtractSettings<PgsqlDapperServiceOptions>();

            var db = Environment.GetEnvironmentVariable("POSTGRES_DB");
            var usr = Environment.GetEnvironmentVariable("POSTGRES_USER");

            return new PgsqlDapperServiceOptions
            {
                ConnectionString = $"User ID={usr};Host=postgres;Database={db};Pooling=true;"
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
            return ConfigurationManager.ExtractSettings<PgsqlAdminOption>();
        }
    }
}