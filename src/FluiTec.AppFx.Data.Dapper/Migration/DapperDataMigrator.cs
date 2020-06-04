using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Exceptions;
using FluentMigrator.Runner.VersionTableInfo;
using FluiTec.AppFx.Data.Migration;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Data.Dapper.Migration
{
    /// <summary>   A dapper data migrator. </summary>
    public class DapperDataMigrator : IDataMigrator
    {
        /// <summary>   The runner. </summary>
        private readonly IMigrationRunner _runner;

        /// <summary>   Constructor. </summary>
        /// <param name="connectionString">     The connection string. </param>
        /// <param name="scanAssemblies">       The scan assemblies. </param>
        /// <param name="metaData">             Information describing the meta. </param>
        /// <param name="configureSqlProvider"> The configure SQL provider. </param>
        public DapperDataMigrator(string connectionString, IEnumerable<Assembly> scanAssemblies, IVersionTableMetaData metaData, Action<IMigrationRunnerBuilder> configureSqlProvider)
        {
            if (scanAssemblies == null)
                throw new ArgumentNullException();

            var sp = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Set the connection string
                    .WithGlobalConnectionString(connectionString)
                    // Set version meta-data
                    .WithVersionTable(metaData)
                    // Define the assembly containing the migrations
                    .ScanIn(scanAssemblies.ToArray()).For.Migrations())
                .ConfigureRunner(configureSqlProvider)
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);

            _runner = sp.GetRequiredService<IMigrationRunner>();
            
            try
            {
                var migrations = _runner.MigrationLoader
                    .LoadMigrations()
                    .OrderByDescending(m => m.Value.Version);
           
                MaximumVersion = migrations.First().Value.Version;

                var loader = sp.GetRequiredService<IVersionLoader>();
                CurrentVersion = loader.VersionInfo.Latest();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (MissingMigrationsException)
            {
                MaximumVersion = 0;
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        /// <summary>   Gets the current version. </summary>
        /// <value> The current version. </value>
        public long CurrentVersion { get; }

        /// <summary>   Gets the maximum version. </summary>
        /// <value> The maximum version. </value>
        public long MaximumVersion { get; }

        /// <summary>   Migrates this. </summary>
        /// <remarks>
        /// Migrates the database to the latest version.
        /// </remarks>
        public void Migrate()
        {
            if (CurrentVersion < MaximumVersion)
                _runner.MigrateUp();
        }

        /// <summary>   Migrates this. </summary>
        /// <exception cref="InvalidOperationException">    Thrown when the requested operation is
        ///                                                 invalid. </exception>
        /// <param name="version">  The version. </param>
        public void Migrate(long version)
        {
            if (version > MaximumVersion)
                throw new InvalidOperationException($"Invalid version for migration. Maximum version found: {MaximumVersion}, Current version: {CurrentVersion}");

            if (version > CurrentVersion)
            {
                _runner.MigrateUp(version);
            }
            else if (version < CurrentVersion)
            {
                _runner.MigrateDown(version);
            }
        }
    }
}