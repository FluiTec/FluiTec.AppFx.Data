namespace FluiTec.AppFx.Data.Dynamic.Configuration
{
    /// <summary>   Values that represent data providers. </summary>
    public enum DataProvider
    {
        /// <summary>   An enum constant representing the mssql option. </summary>
        Mssql,

        /// <summary>   An enum constant representing the mysql option. </summary>
        Mysql,

        /// <summary>   An enum constant representing the pgsql option. </summary>
        Pgsql,

        /// <summary>   An enum constant representing the sqlite option. </summary>
        Sqlite,

        /// <summary>   An enum constant representing the lite Database option. </summary>
        LiteDb,

        /// <summary>   An enum constant representing the nmemory Database option. </summary>
        NMemory
    }
}