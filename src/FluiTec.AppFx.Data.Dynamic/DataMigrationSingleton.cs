using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Migration;

namespace FluiTec.AppFx.Data.Dynamic;

/// <summary>
///     A data migration singleton. This class cannot be inherited.
/// </summary>
public sealed class DataMigrationSingleton
{
    #region Fields

    /// <summary>	The lazy initilizer. </summary>
    private static readonly Lazy<DataMigrationSingleton> Lazy =
        new(() => new DataMigrationSingleton());

    #endregion

    #region Constructors

    /// <summary>
    ///     Constructor that prevents a default instance of this class from being created.
    /// </summary>
    private DataMigrationSingleton()
    {
        VersionMismatch += DefaultVersionMismatchAction;
        MigrationPossible += DefaultMigrationPossibleAction;
    }

    #endregion

    #region Events

    /// <summary>
    ///     Event queue for all listeners interested in VersionMismatch events.
    /// </summary>
    public event Action<IDataMigrator, IDataService> VersionMismatch;

    /// <summary>
    ///     Event queue for all listeners interested in MigrationPossible events.
    /// </summary>
    public event Action<IDataMigrator, IDataService> MigrationPossible;

    #endregion

    #region Properties

    /// <summary>
    ///     The instance.
    /// </summary>
    /// <value>
    ///     The instance.
    /// </value>
    public static DataMigrationSingleton Instance => Lazy.Value;

    /// <summary>
    ///     Gets the default version mismatch action.
    /// </summary>
    /// <value>
    ///     The default version mismatch action.
    /// </value>
    public Action<IDataMigrator, IDataService> DefaultVersionMismatchAction
        => (migrator, service)
            => throw new DataMigrationException(migrator.CurrentVersion, migrator.MaximumVersion,
                service.GetType());

    /// <summary>
    ///     Gets the default migration possible action.
    /// </summary>
    /// <value>
    ///     The default migration possible action.
    /// </value>
    public Action<IDataMigrator, IDataService> DefaultMigrationPossibleAction
        => (migrator, _)
            => migrator.Migrate();

    #endregion

    #region EventInvocators

    /// <summary>
    ///     Executes the 'version mismatch' action.
    /// </summary>
    /// <param name="migrator">     The migrator. </param>
    /// <param name="dataService">  The data service. </param>
    public void OnVersionMismatch(IDataMigrator migrator, IDataService dataService)
    {
        VersionMismatch?.Invoke(migrator, dataService);
    }

    /// <summary>
    ///     Executes the 'migration possible' action.
    /// </summary>
    /// <param name="migrator">     The migrator. </param>
    /// <param name="dataService">  The data service. </param>
    public void OnMigrationPossible(IDataMigrator migrator, IDataService dataService)
    {
        MigrationPossible?.Invoke(migrator, dataService);
    }

    #endregion
}