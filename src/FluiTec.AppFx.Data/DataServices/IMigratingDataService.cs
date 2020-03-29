namespace FluiTec.AppFx.Data.DataServices
{
    /// <summary>Interface for a migrating dataService.</summary>
    /// <seealso cref="FluiTec.AppFx.Data.DataServices.IDataService" />
    public interface IMigratingDataService : IDataService
    {
        /// <summary>Gets a value indicating whether this instance can migrate.</summary>
        /// <value>
        ///     <c>true</c> if this instance can migrate; otherwise, <c>false</c>.
        /// </value>
        bool CanMigrate { get; }

        /// <summary>Migrates the unterlying dataStore.</summary>
        void Migrate();
    }
}