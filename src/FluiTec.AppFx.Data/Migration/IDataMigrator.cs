using System.Collections.Generic;

namespace FluiTec.AppFx.Data.Migration;

/// <summary>   Interface for data-migrators. </summary>
public interface IDataMigrator
{
    /// <summary>   Gets the current version. </summary>
    /// <value> The current version. </value>
    // ReSharper disable once UnusedMemberInSuper.Global
    long CurrentVersion { get; }

    /// <summary>   Gets the maximum version. </summary>
    /// <value> The maximum version. </value>
    // ReSharper disable once UnusedMemberInSuper.Global
    long MaximumVersion { get; }

    /// <summary>
    ///     Gets the migrations in this collection.
    /// </summary>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process the migrations in this collection.
    /// </returns>
    IEnumerable<MigrationInfo> GetMigrations();

    /// <summary>   Migrates this.  </summary>
    void Migrate();

    /// <summary>   Migrates this. </summary>
    /// <param name="version">  The version. </param>
    // ReSharper disable once UnusedMember.Global
    void Migrate(long version);

    /// <summary>
    ///     Gets the migration instructions in text-format.
    /// </summary>
    /// ///
    /// <returns>Instructions to be executed by applying the latest migration.</returns>
    string GetMigrationInstructions();

    /// <summary>
    ///     Gets the migration instructions in text-format.
    /// </summary>
    /// <param name="version">  The version.</param>
    /// <returns>Instructions to be exectuted by applying this migration-version.</returns>
    string GetMigrationInstructions(long version);
}