using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentMigrator.Infrastructure;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Exceptions;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.VersionTableInfo;
using FluiTec.AppFx.Data.Migration;
using Microsoft.Extensions.DependencyInjection;
using MigrationInfo = FluiTec.AppFx.Data.Migration.MigrationInfo;

namespace FluiTec.AppFx.Data.Dapper.Migration
{
    /// <summary>   A dapper data migrator. </summary>
    public class DapperDataMigrator : IDataMigrator
    {
        /// <summary>   The runner. </summary>
        private readonly IMigrationRunner _runner;

        IVersionLoader loader;

        /// <summary>
        /// The migrations.
        /// </summary>
        private IOrderedEnumerable<KeyValuePair<long, IMigrationInfo>> _migrations;

        /// <summary>   Constructor. </summary>
        /// <param name="connectionString">     The connection string. </param>
        /// <param name="scanAssemblies">       The scan assemblies. </param>
        /// <param name="metaData">             Information describing the meta. </param>
        /// <param name="configureSqlProvider"> The configure SQL provider. </param>
        public DapperDataMigrator(string connectionString, IEnumerable<Assembly> scanAssemblies,
            IVersionTableMetaData metaData, Action<IMigrationRunnerBuilder> configureSqlProvider)
        {
            if (scanAssemblies == null)
                throw new ArgumentNullException(nameof(scanAssemblies));

            var services = new ServiceCollection()
                .AddFluentMigratorCore()
                .Configure<RunnerOptions>(opt => opt.Profile = "Development")
                .ConfigureRunner(rb => rb
                    // Set the connection string
                    .WithGlobalConnectionString(connectionString)
                    // Set version meta-data
                    .WithVersionTable(metaData)
                    // Define the assembly containing the migrations
                    .ScanIn(scanAssemblies.ToArray()).For.Migrations())
                .ConfigureRunner(configureSqlProvider)
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            var sp = services.BuildServiceProvider(false);

            _runner = sp.GetRequiredService<IMigrationRunner>();
            
            try
            {
                _migrations = _runner.MigrationLoader
                    .LoadMigrations()
                    .OrderByDescending(m => m.Value.Version);
                

                MaximumVersion = _migrations.First().Value.Version;

                loader = sp.GetRequiredService<IVersionLoader>();
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
        public long CurrentVersion => loader.VersionInfo.Latest();

        /// <summary>   Gets the maximum version. </summary>
        /// <value> The maximum version. </value>
        public long MaximumVersion { get; }

        /// <summary>
        /// Gets the migrations in this collection.
        /// </summary>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the migrations in this collection.
        /// </returns>
        public IEnumerable<MigrationInfo> GetMigrations()
        {
            return _migrations?.Select((pair, i) => new MigrationInfo(pair.Key, pair.Value.GetName().Substring(pair.Value.GetName().IndexOf(':')+2)));
        }

        /// <summary>   Migrates this. </summary>
        /// <remarks>
        ///     Migrates the database to the latest version.
        /// </remarks>
        public void Migrate()
        {
            if (CurrentVersion < MaximumVersion)
                _runner.MigrateUp();
        }

        /// <summary>   Migrates this. </summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the requested operation is
        ///     invalid.
        /// </exception>
        /// <param name="version">  The version. </param>
        public void Migrate(long version)
        {
            if (version > MaximumVersion)
                throw new InvalidOperationException(
                    $"Invalid version for migration. Maximum version found: {MaximumVersion}, Current version: {CurrentVersion}");

            if (version > CurrentVersion)
                _runner.MigrateUp(version);
            else if (version < CurrentVersion) _runner.MigrateDown(version);
        }
    }
}