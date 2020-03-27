using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.DataServices
{
    /// <summary>Basic, abstract implementation of an IMigrationDataService.</summary>
    /// <seealso cref="FluiTec.AppFx.Data.DataServices.DataService" />
    /// <seealso cref="FluiTec.AppFx.Data.DataServices.IMigratingDataService" />
    public abstract class MigratingDataService : DataService, IMigratingDataService
    {
        /// <summary>Initializes a new instance of the <see cref="MigratingDataService"/> class.</summary>
        /// <param name="canMigrate">if set to <c>true</c> [can migrate].</param>
        /// <param name="name">The name.</param>
        /// <param name="logger">The logger.</param>
        protected MigratingDataService(bool canMigrate, string name, ILogger<IMigratingDataService> logger) : base(name, logger)
        {
            CanMigrate = canMigrate;
        }

        /// <summary>Gets a value indicating whether this instance can migrate.</summary>
        /// <value>
        ///   <c>true</c> if this instance can migrate; otherwise, <c>false</c>.</value>
        public virtual bool CanMigrate { get; }

        /// <summary>Migrates the unterlying dataStore.</summary>
        public abstract void Migrate();
    }
}