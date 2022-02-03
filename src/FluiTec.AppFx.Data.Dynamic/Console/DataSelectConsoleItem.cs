using FluiTec.AppFx.Console.ConsoleItems;

namespace FluiTec.AppFx.Data.Dynamic.Console;

/// <summary>
///     A data select console item.
/// </summary>
public class DataSelectConsoleItem : SelectConsoleItem
{
    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <param name="name">     The name. </param>
    /// <param name="module">   The module. </param>
    protected DataSelectConsoleItem(string name, DataConsoleModule module) : base(name)
    {
        Module = module;
    }

    /// <summary>
    ///     Gets the module.
    /// </summary>
    /// <value>
    ///     The module.
    /// </value>
    public DataConsoleModule Module { get; }
}