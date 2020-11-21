using System;

namespace FluiTec.AppFx.Data.Dapper.DataServices
{
    /// <summary>   Interface for command cache.</summary>
    public interface ICommandCache
    {
        /// <summary>   Gets from cache.</summary>
        /// <param name="repositoryType">   Type of the repository. </param>
        /// <param name="memberName">       Name of the member. </param>
        /// <param name="commandFunc">      The command function. </param>
        /// <returns>   The data that was read from the cache.</returns>
        string GetFromCache(Type repositoryType, string memberName, Func<string> commandFunc);
    }
}