using System;

namespace FluiTec.AppFx.Data.Dapper.Repositories
{
    /// <summary>   Interface for repository command cache.</summary>
    public interface IRepositoryCommandCache
    {
        /// <summary>   Gets from cache.</summary>
        /// <param name="commandFunc">  The command function. </param>
        /// <param name="memberName">   Name of the member. </param>
        /// <returns>   The data that was read from the cache.</returns>
        string GetFromCache(Func<string> commandFunc, string memberName);
    }
}