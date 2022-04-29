using System;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Ef;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.TestLibrary.Configuration;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.TestLibrary.DataServiceProviders
{
    /// <summary>
    ///     A mssql ef data service provider.
    /// </summary>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public abstract class MssqlEfDataServiceProvider<TDataService, TUnitOfWork>
        : EnvironmentConfiguredEfDataServiceProvider<TDataService, TUnitOfWork>
        where TDataService : IDataService<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        /// <summary>
        ///     Gets the name of the variable.
        /// </summary>
        /// <value>
        ///     The name of the variable.
        /// </value>
        protected override string VariableName => "SA_PASSWORD";

        /// <summary>
        ///     Configure options.
        /// </summary>
        /// <returns>
        ///     The IDapperServiceOptions.
        /// </returns>
        protected override ISqlServiceOptions ConfigureOptions()
        {
            if (!EnvironmentConfigured) return ConfigurationManager.ExtractSettings<MssqlDapperServiceOptions>();
            return new EfSqlServiceOptions
            {
                SqlType = SqlType.Mssql,
                ConnectionString =
                    $"Data Source=mssql2;Initial Catalog=master;Integrated Security=False;User ID=sa;Password={Environment.GetEnvironmentVariable(VariableName)};Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
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
            return ConfigurationManager.ExtractSettings<MssqlAdminOption>("Ef.Mssql");
        }
    }
}