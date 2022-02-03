using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.TestLibrary.DataServiceProviders
{
    /// <summary>
    ///     An environment configured dapper data service provider.
    /// </summary>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public abstract class
        EnvironmentConfiguredDapperDataServiceProvider<TDataService, TUnitOfWork> : DapperDataServiceProvider<
            TDataService, TUnitOfWork>
        where TDataService : IDataService<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        /// <summary>
        ///     Gets the name of the variable.
        /// </summary>
        /// <value>
        ///     The name of the variable.
        /// </value>
        protected abstract string VariableName { get; }

        /// <summary>
        ///     Gets a value indicating whether the environment configured.
        /// </summary>
        /// <value>
        ///     True if environment configured, false if not.
        /// </value>
        public bool EnvironmentConfigured =>
            !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(VariableName));
    }
}