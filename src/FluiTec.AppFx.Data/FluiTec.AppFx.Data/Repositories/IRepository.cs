using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Repositories
{
    /// <summary>Interface for a repository.</summary>
    public interface IRepository
    {
        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        ILogger<IRepository> Logger { get; }
    }
}