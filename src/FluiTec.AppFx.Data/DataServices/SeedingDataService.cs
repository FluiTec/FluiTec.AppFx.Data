using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.DataServices
{
    /// <summary>Basic, abstract implementation of an ISeedingDataService.</summary>
    /// <seealso cref="FluiTec.AppFx.Data.DataServices.MigratingDataService" />
    /// <seealso cref="FluiTec.AppFx.Data.DataServices.ISeedingDataService" />
    public abstract class SeedingDataService : MigratingDataService, ISeedingDataService
    {
        /// <summary>Initializes a new instance of the <see cref="SeedingDataService" /> class.</summary>
        /// <param name="canSeed">if set to <c>true</c> [can seed].</param>
        /// <param name="canMigrate">if set to <c>true</c> [can migrate].</param>
        /// <param name="name">The name.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="loggerFactory">The loggerFactory.</param>
        protected SeedingDataService(bool canSeed, bool canMigrate, string name, ILogger<IMigratingDataService> logger,
            ILoggerFactory loggerFactory)
            : base(canMigrate, name, logger, loggerFactory)
        {
            CanSeed = canSeed;
        }

        public bool CanSeed { get; }

        public abstract void Seed();
    }
}