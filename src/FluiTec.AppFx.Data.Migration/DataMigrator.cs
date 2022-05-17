using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentMigrator.Infrastructure;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Exceptions;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Logging;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Migration;

/// <summary>   A dapper data migrator. </summary>
public class DataMigrator : IDataMigrator
{
    /// <summary>
    ///     (Immutable) the loader.
    /// </summary>
    private IVersionLoader _loader;

    /// <summary>
    ///     The migrations.
    /// </summary>
    private IOrderedEnumerable<KeyValuePair<long, IMigrationInfo>> _migrations;

    /// <summary>   The preview runner. </summary>
    private IMigrationRunner _previewRunner;

    /// <summary>   The runner. </summary>
    private IMigrationRunner _runner;

    /// <summary>
    ///     The stringWriter to use to preview migrations.
    /// </summary>
    private StringWriter _stringWriter;

    /// <summary>   Constructor. </summary>
    /// <param name="connectionString">     The connection string. </param>
    /// <param name="scanAssemblies">       The scan assemblies. </param>
    /// <param name="metaData">             Information describing the meta. </param>
    /// <param name="configureSqlProvider"> The configure SQL provider. </param>
    public DataMigrator(string connectionString, IEnumerable<Assembly> scanAssemblies,
        IVersionTableMetaData metaData, Action<IMigrationRunnerBuilder> configureSqlProvider)
    {
        if (scanAssemblies == null)
            throw new ArgumentNullException(nameof(scanAssemblies));

        var assemblies = scanAssemblies as Assembly[] ?? scanAssemblies.ToArray();
        InitializeRunner(connectionString, assemblies, metaData, configureSqlProvider);
        InitializePreviewRunner(connectionString, assemblies, metaData, configureSqlProvider);
    }

    /// <summary>   Gets the current version. </summary>
    /// <value> The current version. </value>
    public long CurrentVersion => _loader.VersionInfo.Latest();

    /// <summary>   Gets the maximum version. </summary>
    /// <value> The maximum version. </value>
    public long MaximumVersion { get; private set; }

    /// <summary>
    ///     Gets the migrations in this collection.
    /// </summary>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process the migrations in this collection.
    /// </returns>
    public IEnumerable<MigrationInfo> GetMigrations()
    {
        return _migrations?.Select((pair, _) =>
            new MigrationInfo(pair.Key, pair.Value.GetName().Substring(pair.Value.GetName().IndexOf(':') + 2)));
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

    /// <summary>
    ///     Gets the migration instructions in text-format.
    /// </summary>
    /// ///
    /// <returns>Instructions to be executed by applying the latest migration.</returns>
    public string GetMigrationInstructions()
    {
        if (CurrentVersion < MaximumVersion)
            _previewRunner.MigrateUp();

        var res = _stringWriter.GetStringBuilder().ToString();
        _stringWriter.GetStringBuilder().Clear();
        return res;
    }

    /// <summary>
    ///     Gets the migration instructions in text-format.
    /// </summary>
    /// <param name="version">  The version.</param>
    /// <returns>Instructions to be exectuted by applying this migration-version.</returns>
    public string GetMigrationInstructions(long version)
    {
        if (version > MaximumVersion)
            throw new InvalidOperationException(
                $"Invalid version for migration. Maximum version found: {MaximumVersion}, Current version: {CurrentVersion}");

        if (version > CurrentVersion)
            _previewRunner.MigrateUp(version);
        else if (version < CurrentVersion) _previewRunner.MigrateDown(version);

        var res = _stringWriter.GetStringBuilder().ToString();
        _stringWriter.GetStringBuilder().Clear();
        return res;
    }

    /// <summary>
    ///     Initializes the MigrationRunner to be used to apply migrations.
    /// </summary>
    /// <param name="connectionString">     The connectionstring.</param>
    /// <param name="scanAssemblies">       The scanAssemblies.</param>
    /// <param name="metaData">             The metaData.</param>
    /// <param name="configureSqlProvider"> The sqlProvider-configuration.</param>
    private void InitializeRunner(string connectionString, IEnumerable<Assembly> scanAssemblies,
        IVersionTableMetaData metaData, Action<IMigrationRunnerBuilder> configureSqlProvider)
    {
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

        IServiceProvider serviceProvider = services.BuildServiceProvider(false);

        _runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        try
        {
            _migrations = _runner.MigrationLoader
                .LoadMigrations()
                .OrderByDescending(m => m.Value.Version);
            MaximumVersion = _migrations.First().Value.Version;
            _loader = serviceProvider.GetRequiredService<IVersionLoader>();
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (MissingMigrationsException)
        {
            MaximumVersion = 0;
        }
#pragma warning restore CA1031 // Do not catch general exception types
    }

    /// <summary>
    ///     Initializes the MigrationRunner to be used to preview migrations.
    /// </summary>
    /// <param name="connectionString">     The connectionstring.</param>
    /// <param name="scanAssemblies">       The scanAssemblies.</param>
    /// <param name="metaData">             The metaData.</param>
    /// <param name="configureSqlProvider"> The sqlProvider-configuration.</param>
    private void InitializePreviewRunner(string connectionString, IEnumerable<Assembly> scanAssemblies,
        IVersionTableMetaData metaData, Action<IMigrationRunnerBuilder> configureSqlProvider)
    {
        _stringWriter = new StringWriter();
        var services = new ServiceCollection()
            .AddFluentMigratorCore()
            .Configure<RunnerOptions>(opt => opt.Profile = "Development")
            .ConfigureRunner(rb => rb
                // Set the connection string
                .WithGlobalConnectionString(connectionString)
                // Set version meta-data
                .WithVersionTable(metaData)
                // disable execution of operations
                .ConfigureGlobalProcessorOptions(options => options.PreviewOnly = true)
                // Define the assembly containing the migrations
                .ScanIn(scanAssemblies.ToArray()).For.Migrations())
            .ConfigureRunner(configureSqlProvider)
            // Enable logging to console in the FluentMigrator way
            .AddLogging(lb => { lb.AddProvider(new SqlScriptFluentMigratorLoggerProvider(_stringWriter)); });

        IServiceProvider serviceProvider = services.BuildServiceProvider(false);

        _previewRunner = serviceProvider.GetRequiredService<IMigrationRunner>();
    }
}