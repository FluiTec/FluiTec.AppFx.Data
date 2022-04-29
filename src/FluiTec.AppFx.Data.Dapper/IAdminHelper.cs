using System;

namespace FluiTec.AppFx.Data.Dapper;

/// <summary>
/// Interface for admin helper.
/// </summary>
public interface IAdminHelper
{
    /// <summary>
    /// Creates a database.
    /// </summary>
    ///
    /// <param name="adminConnectionString">    The admin connection string. </param>
    /// <param name="dbName">                   Name of the database. </param>
    static int CreateDatabase(string adminConnectionString, string dbName) => throw new NotImplementedException();

    /// <summary>
    /// Creates user and login.
    /// </summary>
    ///
    /// <param name="adminConnectionString">    The admin connection string. </param>
    /// <param name="dbName">                   Name of the database. </param>
    /// <param name="userName">                 Name of the user. </param>
    /// <param name="password">                 The password. </param>
    static int CreateUserAndLogin(string adminConnectionString, string dbName, string userName,
        string password) => throw new NotImplementedException();
}