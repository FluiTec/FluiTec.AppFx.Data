using System;

namespace FluiTec.AppFx.Data.Dynamic
{
    /// <summary>   Exception for signalling data migration errors. </summary>
    public class DataMigrationException : Exception
    {
        /// <summary>   Gets the current version. </summary>
        /// <value> The current version. </value>
        public long CurrentVersion { get; }
        /// <summary>   Gets the maximum version. </summary>
        /// <value> The maximum version. </value>
        public long MaximumVersion { get; }

        /// <summary>   Gets the type of the data service. </summary>
        /// <value> The type of the data service. </value>
        public Type DataServiceType { get; }

        /// <summary>   Constructor. </summary>
        /// <param name="currentVersion">   The current version. </param>
        /// <param name="maximumVersion">   The maximum version. </param>
        /// <param name="dataServiceType">  Type of the data service. </param>
        public DataMigrationException(long currentVersion, long maximumVersion, Type dataServiceType) : base($"\"{dataServiceType.Name}\" needs version {maximumVersion} but the database only supplies version {currentVersion}. Please migrate manually or enable AutoMigrate")
        {
            CurrentVersion = currentVersion;
            MaximumVersion = maximumVersion;
            DataServiceType = dataServiceType;
        }
    }
}
