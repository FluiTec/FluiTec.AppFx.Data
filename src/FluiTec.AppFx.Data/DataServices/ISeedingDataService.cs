namespace FluiTec.AppFx.Data.DataServices
{
    /// <summary>Interface for a seeding dataService.</summary>
    /// <seealso cref="FluiTec.AppFx.Data.DataServices.IDataService" />
    public interface ISeedingDataService : IDataService
    {
        /// <summary>Gets a value indicating whether this instance can seed.</summary>
        /// <value>
        ///   <c>true</c> if this instance can seed; otherwise, <c>false</c>.</value>
        bool CanSeed { get; }

        /// <summary>Seeds this instance.</summary>
        void Seed();
    }
}