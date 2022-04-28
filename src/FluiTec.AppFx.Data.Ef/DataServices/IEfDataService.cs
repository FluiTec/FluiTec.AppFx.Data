using FluiTec.AppFx.Data.DataServices;

namespace FluiTec.AppFx.Data.Ef.DataServices;

/// <summary>
///     Interface for ef data service.
/// </summary>
public interface IEfDataService : IDataService
{
    /// <summary>
    ///     Gets the context.
    /// </summary>
    /// <returns>
    ///     The context.
    /// </returns>
    IDynamicDbContext GetContext();
}