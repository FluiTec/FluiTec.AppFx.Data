﻿using System;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.TestLibrary.Configuration;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.TestLibrary.DataServiceProviders
{
    /// <summary>
    ///     A mysql data service provider.
    /// </summary>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public abstract class MysqlDataServiceProvider<TDataService, TUnitOfWork>
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
        protected override string VariableName => "MYSQL_DATABASE";

        /// <summary>
        ///     Configure options.
        /// </summary>
        /// <returns>
        ///     The IDapperServiceOptions.
        /// </returns>
        protected override IDapperServiceOptions ConfigureOptions()
        {
            if (!EnvironmentConfigured) return ConfigurationManager.ExtractSettings<MysqlDapperServiceOptions>();

            var db = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
            var pw = Environment.GetEnvironmentVariable("MYSQL_ROOT_PASSWORD");

            return new MysqlDapperServiceOptions
            {
                ConnectionString = $"Server=mysql;Database={db};Uid=root;Pwd={pw}"
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
            return ConfigurationManager.ExtractSettings<MysqlAdminOption>();
        }
    }
}