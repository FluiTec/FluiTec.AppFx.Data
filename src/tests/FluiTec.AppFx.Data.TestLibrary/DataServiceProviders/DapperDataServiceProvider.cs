using System.IO;
using FluiTec.AppFx.Console.Helpers;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.TestLibrary.Configuration;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Configuration;
using ConfigurationManager = FluiTec.AppFx.Options.Managers.ConfigurationManager;

namespace FluiTec.AppFx.Data.TestLibrary.DataServiceProviders
{
    /// <summary>
    ///     A dapper data service provider.
    /// </summary>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public abstract class
        DapperDataServiceProvider<TDataService, TUnitOfWork> : DataServiceProvider<TDataService, TUnitOfWork>
        where TDataService : IDataService<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        #region Constructors

        /// <summary>
        ///     Specialized default constructor for use only by derived class.
        /// </summary>
        protected DapperDataServiceProvider()
        {
            var path = DirectoryHelper.GetApplicationRoot();
            var parent = Directory.GetParent(path)?.Parent?.Parent?.Parent?.FullName;
            var config = new ConfigurationBuilder()
                .SetBasePath(parent)
                .AddJsonFile("appsettings.integration.json", false, false)
                .AddJsonFile("appsettings.integration.secret.json", true, false)
                .Build();
            ConfigurationManager = new ConfigurationManager(config);

            // ReSharper disable VirtualMemberCallInConstructor
            ServiceOptions = ConfigureOptions();
            AdminOptions = ConfigureAdminOptions();
            // ReSharper enable VirtualMemberCallInConstructor
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets options for controlling the service.
        /// </summary>
        /// <value>
        ///     Options that control the service.
        /// </value>
        public IDapperServiceOptions ServiceOptions { get; protected set; }

        /// <summary>
        ///     Gets or sets options for controlling the admin.
        /// </summary>
        /// <value>
        ///     Options that control the admin.
        /// </value>
        public DbAdminOptions AdminOptions { get; protected set; }

        /// <summary>
        ///     Gets the manager for configuration.
        /// </summary>
        /// <value>
        ///     The configuration manager.
        /// </value>
        protected ConfigurationManager ConfigurationManager { get; }

        /// <summary>
        ///     Gets a value indicating whether the database is available.
        /// </summary>
        /// <value>
        ///     True if the database is available, false if not.
        /// </value>
        public override bool IsDbAvailable => ServiceOptions != null;

        #endregion

        #region Methods

        /// <summary>
        ///     Configure options.
        /// </summary>
        /// <returns>
        ///     The IDapperServiceOptions.
        /// </returns>
        protected abstract IDapperServiceOptions ConfigureOptions();

        /// <summary>
        ///     Configure admin options.
        /// </summary>
        /// <returns>
        ///     The DbAdminOptions.
        /// </returns>
        protected abstract DbAdminOptions ConfigureAdminOptions();

        #endregion
    }
}