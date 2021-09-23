using System;
using System.Runtime.CompilerServices;

namespace FluiTec.AppFx.Data.Dapper.Repositories
{
    /// <summary>   Interface for repository command cache.</summary>
    public interface IRepositoryCommandCache
    {
        /// <summary>
        /// Gets from cache.
        /// </summary>
        ///
        /// <param name="commandFunc">  The command function. </param>
        /// <param name="memberName">   (Optional) Name of the member. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>
        /// The data that was read from the cache.
        /// </returns>
        string GetFromCache(Func<string> commandFunc, [CallerMemberName] string memberName = null,
            params string[] parameters);
    }
}