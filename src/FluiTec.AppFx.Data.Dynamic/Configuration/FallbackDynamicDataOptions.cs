using System;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.Dynamic.Configuration;

/// <summary>
/// A fallback dynamic data options.
/// </summary>
public class FallbackDynamicDataOptions : IOptionsMonitor<IDynamicDataOptions>
{
    private readonly IOptionsMonitor<IDynamicDataOptions> _preferredOptions;
    private readonly IOptionsMonitor<IDynamicDataOptions> _fallbackOptions;

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="preferredOptions"> Options for controlling the preferred. </param>
    /// <param name="fallbackOptions">  Options for controlling the fallback. </param>
    public FallbackDynamicDataOptions(IOptionsMonitor<IDynamicDataOptions> preferredOptions,
        IOptionsMonitor<IDynamicDataOptions> fallbackOptions)
    {
        _preferredOptions = preferredOptions;
        _fallbackOptions = fallbackOptions;
    }

    /// <summary>
    /// Returns the current instances value by name.
    /// </summary>
    ///
    /// <param name="name"> The name to get. </param>
    ///
    /// <returns>
    /// The IDynamicDataOptions.
    /// </returns>
    public IDynamicDataOptions Get(string name)
    {
        return _preferredOptions.CurrentValue != null &&
            _preferredOptions.CurrentValue.Provider != DataProvider.Unconfigured
                ? _preferredOptions.Get(name)
                : _fallbackOptions.Get(name);
    }

    /// <summary>
    /// Registers a listener to be called whenever a named options changes.
    /// </summary>
    ///
    /// <param name="listener"> The action to be invoked when options has changed. </param>
    ///
    /// <returns>
    /// An <see cref="T:System.IDisposable" /> which should be disposed to stop listening for changes.
    /// </returns>
    public IDisposable OnChange(Action<IDynamicDataOptions, string> listener)
    {
        return _preferredOptions.CurrentValue != null &&
               _preferredOptions.CurrentValue.Provider != DataProvider.Unconfigured
            ? _preferredOptions.OnChange(listener)
            : _fallbackOptions.OnChange(listener);
    }

    /// <summary>
    /// Returns the current instances value.
    /// </summary>
    ///
    /// <value>
    /// The current value.
    /// </value>
    public IDynamicDataOptions CurrentValue =>
        _preferredOptions.CurrentValue != null &&
        _preferredOptions.CurrentValue.Provider != DataProvider.Unconfigured
            ? _preferredOptions.CurrentValue
            : _fallbackOptions.CurrentValue;
}